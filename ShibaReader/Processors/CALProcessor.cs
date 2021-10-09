using ShibaReader.Models;
using ShibaReader.Utils;
using System.Collections.Generic;
using System.IO;

namespace ShibaReader.Processors
{
    class CALProcessor : Processor
    {
        public string FileName { get; set; }

        public CALProcessor(string fileName)
        {
            this.FileName = fileName;
        }

        public Dictionary<string, Calendar> ProcessCALFile()
        {
            Dictionary<string, Calendar> calendars = new Dictionary<string, Calendar>();

            try
            {
                using (var reader = new StreamReader(FileName))
                {
                    Calendar calendar = null;
                    string line = reader.ReadLine()?.Trim();
                    while (line != null)
                    {
                        if (line.StartsWith("calendar"))
                        {
                            string param = this.ExtractParameterValue(line, "calendar");
                            calendar = new(param);
                            calendars.Add(param, calendar);
                        }
                        else if (line != "" && !line.StartsWith("description") && calendar != null)
                        {
                            /*
                            string[] tokens = line.Split(" ");
                            string[] date = tokens[0].Split("/");
                            string[] time = tokens[1].Split(":");

                            int day = StringUtils.ToInteger(date[1]), month = StringUtils.ToInteger(date[0]), year = StringUtils.ToInteger(date[2]);
                            int hour = StringUtils.ToInteger(time[0]), min = StringUtils.ToInteger(time[1]), sec = StringUtils.ToInteger(time[2]);
                            */
                            calendar.DateTimes.Add(line);
                        }
                        line = reader.ReadLine()?.Trim();
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {

            }
            catch (FileNotFoundException)
            {

            }
            return calendars;
        }
    }
}
