using ShibaReader.Models;
using ShibaReader.Common;
using System.IO;
using System.Windows;
using System.Collections.Generic;
using System;
using ShibaReader.Utils;
using ShibaReader.Events;
using System.Collections.ObjectModel;
using System.Text;

namespace ShibaReader.Processors
{
    class JILProcessor : Processor
    {
        public string FileName { get; set; }
        private UpdateImportJobEvent jobEvent = null;
        public JILProcessor(string fileName)
        {
            this.FileName = fileName;
            jobEvent = UpdateImportJobEvent.getInstance();
        }

        public Dictionary<string, AutoSysJob> ProcessJILFile()
        {
            Dictionary<string, AutoSysJob> autoSysJobs = new Dictionary<string, AutoSysJob>();
            if (FileName == "") return autoSysJobs;

            try
            {
                int totalLineCount = FileUtils.GetLineCount(FileName);
                int currLineCount = 0;
                using (var reader = new StreamReader(FileName))
                {
                    AutoSysJob job = null;
                    string line = reader.ReadLine()?.Trim();
                    while (line != null)
                    {
                        // Job Name
                        if (line.StartsWith("/*"))
                        {
                            string[] tokens = line.Split(" ");
                            if (tokens.Length < 2) continue;

                            if (autoSysJobs.ContainsKey(tokens[2]))
                            {
                                job = autoSysJobs[tokens[2]];
                            }
                            else
                            {
                                job = new AutoSysJob(tokens[2]);
                                autoSysJobs.Add(tokens[2], job);
                            }
                        }
                        else if (job != null && line != "")
                        {
                            if (line.StartsWith("insert_job"))
                            {
                                string param = ExtractParameterValue(line, "insert_job");
                                param = ExtractParameterValue(param, "job_type");
                                string[] paramTokens = param.Split(" ");
                                bool firstParamFilled = false;
                                for (int i = 0; i < paramTokens.Length; i++)
                                {
                                    string paramToken = paramTokens[i].Trim();
                                    if (paramToken != "")
                                    {
                                        if (!firstParamFilled)
                                        {
                                            job.InsertJob = paramToken;
                                            firstParamFilled = true;
                                        }
                                        else
                                        {
                                            job.JobType = paramToken;
                                            break;
                                        }
                                    }
                                }
                            }
                            else if (line.StartsWith("command"))
                            {
                                string param = ExtractParameterValue(line, "command");
                                job.Command = param;
                            }
                            else if (line.StartsWith("machine"))
                            {
                                string param = ExtractParameterValue(line, "machine");
                                job.Machine = param;
                            }
                            else if (line.StartsWith("owner"))
                            {
                                string param = ExtractParameterValue(line, "owner");
                                job.Owner = param;
                            }
                            else if (line.StartsWith("permission"))
                            {
                                string param = ExtractParameterValue(line, "permission");
                                string[] paramTokens = param.Split(",");
                                for (int i = 0; i < paramTokens.Length; i++)
                                {
                                    string permission = paramTokens[i].Trim();
                                    switch (permission)
                                    {
                                        case "gx":
                                            job.Permissions.Add(Enums.Permission.GroupExecute);
                                            break;
                                        case "ge":
                                            job.Permissions.Add(Enums.Permission.GroupEdit);
                                            break;
                                        case "mx":
                                            job.Permissions.Add(Enums.Permission.AuthorizedExecute);
                                            break;
                                        case "me":
                                            job.Permissions.Add(Enums.Permission.AuthorizedEdit);
                                            break;
                                        case "wx":
                                            job.Permissions.Add(Enums.Permission.WorldExecute);
                                            break;
                                        case "we":
                                            job.Permissions.Add(Enums.Permission.WorldEdit);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            else if (line.StartsWith("date_conditions"))
                            {
                                string param = ExtractParameterValue(line, "date_conditions");
                                job.HasDateConditions = param == "0" ? false : true;
                            }
                            else if (line.StartsWith("days_of_week"))
                            {
                                string param = ExtractParameterValue(line, "days_of_week");
                                string[] paramTokens = param.Split(",");
                                for (int i = 0; i < paramTokens.Length; i++)
                                {
                                    string day = paramTokens[i].Trim();
                                    switch (day)
                                    {
                                        case "mo":
                                            job.DaysToRun.Add(Enums.DaysOfWeek.Monday);
                                            break;
                                        case "tu":
                                            job.DaysToRun.Add(Enums.DaysOfWeek.Tuesday);
                                            break;
                                        case "we":
                                            job.DaysToRun.Add(Enums.DaysOfWeek.Wednesday);
                                            break;
                                        case "th":
                                            job.DaysToRun.Add(Enums.DaysOfWeek.Thursday);
                                            break;
                                        case "fr":
                                            job.DaysToRun.Add(Enums.DaysOfWeek.Friday);
                                            break;
                                        case "sa":
                                            job.DaysToRun.Add(Enums.DaysOfWeek.Saturday);
                                            break;
                                        case "su":
                                            job.DaysToRun.Add(Enums.DaysOfWeek.Sunday);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            else if (line.StartsWith("run_calendar"))
                            {
                                string param = ExtractParameterValue(line, "run_calendar");
                                job.RunSchedule = param;
                            }
                            else if (line.StartsWith("exclude_calendar"))
                            {
                                string param = ExtractParameterValue(line, "exclude_calendar");
                                job.ExcludeSchedule = param;
                            }
                            else if (line.StartsWith("start_times"))
                            {
                                string param = ExtractParameterValue(line, "start_times");
                                job.StartTime = param;
                            }
                            else if (line.StartsWith("condition"))
                            {
                                string param = ExtractParameterValue(line, "condition");
                                string[] conditions = param.Split(new char[] { '&', '|' });
                                for (int i = 0; i < conditions.Length; i++)
                                {
                                    string[] condition = conditions[i].Split(new char[] { '(', ')' });
                                    string conditionTypeText = condition[0].Trim();
                                    Enums.JobStatus conditionType = Enums.JobStatus.None;
                                    switch (conditionTypeText.ToString())
                                    {
                                        case "Success":
                                        case "s":
                                            conditionType = Enums.JobStatus.Success;
                                            break;
                                        case "Done":
                                        case "d":
                                            conditionType = Enums.JobStatus.Done;
                                            break;
                                        case "Failure":
                                        case "f":
                                            conditionType = Enums.JobStatus.Failure;
                                            break;
                                        case "Terminated":
                                        case "t":
                                            conditionType = Enums.JobStatus.Terminated;
                                            break;
                                        case "Notrunning":
                                        case "n":
                                            conditionType = Enums.JobStatus.Notrunning;
                                            break;
                                        default:
                                            break;
                                    }
                                    
                                    string conditionJobText = condition[1].Trim();
                                    if (!autoSysJobs.ContainsKey(conditionJobText))
                                    {
                                        autoSysJobs.Add(conditionJobText, new AutoSysJob(conditionJobText));
                                    }
                                    job.RunCondition.Add(new Tuple<Enums.JobStatus, AutoSysJob>(conditionType, autoSysJobs[conditionJobText]));
                                }
                            }
                            else if (line.StartsWith("description"))
                            {
                                string param = ExtractParameterValue(line, "description");
                                job.Description = param;
                            }
                            else if (line.StartsWith("std_out_file"))
                            {
                                string param = ExtractParameterValue(line, "std_out_file");
                                job.JobLogFile = param;
                            }
                            else if (line.StartsWith("std_err_file"))
                            {
                                string param = ExtractParameterValue(line, "std_err_file");
                                job.JobErrorFile = param;
                            }
                            else if (line.StartsWith("alarm_if_fail"))
                            {
                                string param = ExtractParameterValue(line, "alarm_if_fail");
                                job.AlarmIfFail = param == "0" ? false : true;
                            }
                            else if (line.StartsWith("alarm_if_terminated"))
                            {
                                string param = ExtractParameterValue(line, "alarm_if_terminated");
                                job.AlarmIfTerminated = param == "0" ? false : true;
                            }
                            else if (line.StartsWith("application"))
                            {
                                string param = ExtractParameterValue(line, "application");
                                job.Application = param;
                            }
                            else if (line.StartsWith("send_notification"))
                            {
                                string param = ExtractParameterValue(line, "send_notification");
                                if (param == "y" || param == "1")
                                {
                                    job.SendNotificationOn = Enums.SendAlert.Yes;
                                }
                                else if (param == "n" || param == "0")
                                {
                                    job.SendNotificationOn = Enums.SendAlert.No;
                                }
                                else if (param == "F" || param == "f" || param == "2")
                                {
                                    job.SendNotificationOn = Enums.SendAlert.Failure;
                                }
                            }
                            else if (line.StartsWith("notification_template"))
                            {
                                string param = ExtractParameterValue(line, "notification_template");
                                job.NotificationTemplate = param;
                            }
                            else if (line.StartsWith("notification_emailaddress_on_failure"))
                            {
                                string param = ExtractParameterValue(line, "notification_emailaddress_on_failure");
                                string separator = param.Contains(";") ? ";" : ",";
                                string[] emailAddresses = param.Split(separator);
                                for (int i = 0; i < emailAddresses.Length; i++)
                                {
                                    string emailAddress = emailAddresses[i].Trim();
                                    if (emailAddress != "")
                                    {
                                        job.EmailAddressesOnFailure.Add(emailAddress);
                                    }
                                }
                            }
                            else if (line.StartsWith("notification_alarm_types"))
                            {
                                string param = ExtractParameterValue(line, "notification_alarm_types");
                                string separator = param.Contains(";") ? ";" : ",";
                                string[] alarmTypes = param.Split(separator);
                                for (int i = 0; i < alarmTypes.Length; i++)
                                {
                                    string alarmType = alarmTypes[i].Trim();
                                    if (alarmType != "")
                                    {
                                        job.NotificationAlarmTypes.Add((Enums.NotificationAlarmType)Enum.Parse(typeof(Enums.NotificationAlarmType), alarmType.ToUpper()));
                                    }
                                }
                            }
                            else if (line.StartsWith("notification_emailaddress_on_alarm"))
                            {
                                string param = ExtractParameterValue(line, "notification_emailaddress_on_alarm");
                                string separator = param.Contains(";") ? ";" : ",";
                                string[] emailAddresses = param.Split(separator);
                                for (int i = 0; i < emailAddresses.Length; i++)
                                {
                                    string emailAddress = emailAddresses[i].Trim();
                                    if (emailAddress != "")
                                    {
                                        job.EmailAddressesOnAlarm.Add(emailAddress);
                                    }
                                }
                            }
                        }
                        currLineCount++;
                        jobEvent.Progress = currLineCount / totalLineCount;
                        line = reader.ReadLine()?.Trim();
                    }
                    jobEvent.TaskComplete();
                }
            }
            catch (DirectoryNotFoundException)
            {
                
            }
            catch (FileNotFoundException)
            {
                
            }
            return autoSysJobs;
        }
    }
}
