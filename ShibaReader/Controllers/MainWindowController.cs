using ShibaReader.Models;
using ShibaReader.Processors;
using ShibaReader.Utils;
using System.Collections.Generic;
using System.Linq;

namespace ShibaReader.Controllers
{
    class MainWindowController
    {
        private Dictionary<string, AutoSysJob> autoSysJobs = null;
        private Dictionary<string, List<string>> calendarDates = new Dictionary<string, List<string>>();
        private List<string> matchedJobs = new List<string>();
        private int currentIndex = 0;
        private int increment = 1;
        public void importJILFile()
        {
            string fileName = FileUtils.openFileChooser();
            if (fileName == null) return;

            JILProcessor jilProc = new JILProcessor(fileName);
            autoSysJobs = jilProc.processJILFile();
        }

        public void importCALFile()
        {
            string fileName = FileUtils.openFileChooser();
            if (fileName == null) return;

            CALProcessor calProc = new CALProcessor(fileName);
            calendarDates = calProc.processCALFile();
        }

        public AutoSysJob getCurrJob()
        {
            if (matchedJobs.Count == 0) return null;

            var job = autoSysJobs[matchedJobs[currentIndex]];

            if (matchedJobs.Count > 1)
            {
                if (currentIndex + increment >= matchedJobs.Count)
                {
                    increment = -1;
                }
                else if (currentIndex + increment < 0)
                {
                    increment = 1;
                }
                currentIndex += increment;
            }
            return job;
        }

        public AutoSysJob searchJob(string jobName)
        {
            if (autoSysJobs == null) return null;

            currentIndex = 0;
            if (autoSysJobs.ContainsKey(jobName))
            {
                matchedJobs.Clear();
                matchedJobs.Add(jobName);
            }
            else
            {
                matchedJobs = autoSysJobs.Keys.Where(k => k.ToLower().Contains(jobName.ToLower())).ToList();
            }
            return getCurrJob();
        }
    }
}
