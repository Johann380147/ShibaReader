using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ShibaReader.Common;

namespace ShibaReader.Models
{
    /* ----------------- PFTP_PZ_A_S-IRAS-ADDRINFO ----------------- */
    /*
     * 
     * insert_job: PFTP_PZ_A_S-IRAS-ADDRINFO   job_type: CMD 
     * command: /projn/pz/script/sftp_s_pz_iras_addrinfo.sh
     * machine: PFTP
     * owner: pz53@PFTP
     * permission: gx,wx
     * date_conditions: 0
     * days_of_week: su,mo,tu,we,th,fr,sa
     * run_calendar: PZ-7886_14th_28th
     * exclude_calendar: PZ_01_02_03_JUNE2019
     * start_times: "08:00"
     * condition: s(PFTP_PZ_W_DECRY-CPF-NONEDN-SIT)
     * description: "PZ"
     * std_out_file: "/opt/autosys/outfiles/$AUTO_JOB_NAME.out"
     * std_err_file: "/opt/autosys/errorfiles/$AUTO_JOB_NAME.err"
     * alarm_if_fail: 1
     * alarm_if_terminated: 1
     * application: PZ
     * send_notification: F
     * notification_template: "failed_template"
     * notification_emailaddress_on_failure: Prasanth_VIJAYANATHAN_NAIR@moe.gov.sg
     * notification_emailaddress_on_failure: Elvin_PRANANTA@moe.gov.sg
     * notification_emailaddress_on_failure: psea-support@wizvision.com
     * notification_alarm_types: JOBFAILURE
     * notification_emailaddress_on_alarm: 97731360@10.152.193.10;97421484@10.152.193.10;98764709@10.152.193.10;93406076@10.152.193.10;87547468@10.152.193.10;90927008@10.152.193.10;90469259@10.152.193.10;88120262@10.152.193.10
     *
     */
    public class AutoSysJob
    {
        public string JobName { get; set; }
        public string InsertJob { get; set; }
        public string JobType { get; set; }
        public string Command { get; set; }
        public string Machine { get; set; }
        public string Owner { get; set; }
        public List<Enums.Permission> Permissions { get; set; } = new List<Enums.Permission>();
        public bool HasDateConditions { get; set; }
        public List<Enums.DaysOfWeek> DaysToRun { get; set; } = new List<Enums.DaysOfWeek>();
        public string RunSchedule { get; set; }
        public string ExcludeSchedule { get; set; }
        public string StartTime { get; set; }
        public ObservableCollection<Tuple<Enums.JobStatus, AutoSysJob>> RunCondition { get; set; } = new();
        public string Description { get; set; }
        public string JobLogFile { get; set; }
        public string JobErrorFile { get; set; }
        public bool AlarmIfFail { get; set; }
        public bool AlarmIfTerminated { get; set; }
        public string Application { get; set; }
        public Enums.SendAlert SendNotificationOn { get; set; }
        public string NotificationTemplate { get; set; }
        public List<Enums.NotificationAlarmType> NotificationAlarmTypes { get; set; } = new List<Enums.NotificationAlarmType>();
        public List<string> EmailAddressesOnFailure { get; set; } = new List<string>();
        public List<string> EmailAddressesOnAlarm { get; set; } = new List<string>();

        private AutoSysJob() { }
        public AutoSysJob(string jobName)
        {
            this.JobName = jobName;
        }
    }
}
