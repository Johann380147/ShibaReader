using ShibaReader.Models;
using ShibaReader.Processors;
using ShibaReader.Utils;
using ShibaReader.Views;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ShibaReader.Controllers
{
    class MainWindowController
    {
        CalendarView calendarView = new CalendarView();
        private Dictionary<string, AutoSysJob> autoSysJobs = null;
        private Dictionary<string, Calendar> calendarDates = new Dictionary<string, Calendar>();
        private List<string> matchedJobs = new List<string>();
        private int currentIndex = 0;
        public int CurrentIndex 
        { 
            get => currentIndex;
            set
            {
                currentIndex = value;
                IsFirstElement = currentIndex - 1 < 0 ? true : false;
                IsLastElement = currentIndex + 1 >= matchedJobs.Count ? true : false;
            }
        }
        public bool IsFirstElement { get; set; } = true;
        public bool IsLastElement { get; set; } = true;
        public int MatchedJobsCount { get; set; }

                
        public MainWindowController()
        {
            string jilFile = (string)Application.Current.Properties[ApplicationProperties.JilFileProperty];
            string calFile = (string)Application.Current.Properties[ApplicationProperties.CalFileProperty];
            JILProcessor jilProc = new JILProcessor(jilFile);
            autoSysJobs = jilProc.ProcessJILFile();
            CALProcessor calProc = new CALProcessor(calFile);
            calendarDates = calProc.ProcessCALFile();
        }

        public int ImportJILFile()
        {
            string filter = "Jil files (*.jil)|*.jil|All files (*.*)|*.*";
            string fileName = FileUtils.OpenFileChooser(filter);
            if (fileName == null) return 0;

            JILProcessor jilProc = new JILProcessor(fileName);
            autoSysJobs = jilProc.ProcessJILFile();
            Reset();
            return autoSysJobs.Count;
        }

        public int ImportCALFile()
        {
            string filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            string fileName = FileUtils.OpenFileChooser(filter);
            if (fileName == null) return 0;

            CALProcessor calProc = new CALProcessor(fileName);
            calendarDates = calProc.ProcessCALFile();
            return calendarDates.Count;
        }

        public AutoSysJob GetCurrJob()
        {
            if (matchedJobs.Count == 0) return null;

            return autoSysJobs[matchedJobs[CurrentIndex]];
        }

        public AutoSysJob GetPrevJob()
        {
            if (matchedJobs.Count == 0) return null;

            if (CurrentIndex - 1 >= 0)
            {
                return autoSysJobs[matchedJobs[--CurrentIndex]];
            }
            return null;
        }

        public AutoSysJob GetNextJob()
        {
            if (matchedJobs.Count == 0) return null;

            if (CurrentIndex + 1 < matchedJobs.Count)
            {
                return autoSysJobs[matchedJobs[++CurrentIndex]];
            }
            return null;
        }

        public AutoSysJob SearchJob(string jobName)
        {
            if (autoSysJobs == null) return null;

            if (autoSysJobs.ContainsKey(jobName))
            {
                matchedJobs.Clear();
                matchedJobs.Add(jobName);
            }
            else
            {
                matchedJobs = autoSysJobs.Keys.Where(k => k.ToLower().Contains(jobName.ToLower())).ToList();
            }
            CurrentIndex = 0;
            MatchedJobsCount = matchedJobs.Count;
            return GetCurrJob();
        }

        public void GetCalendar(string name)
        {
            if (calendarDates.ContainsKey(name))
            {
                calendarView.SetCalendar(calendarDates[name]);
                calendarView.Show();
            }
        }

        public int GetJobCount()
        {
            return autoSysJobs.Count;
        }

        public int GetCalendarCount()
        {
            return calendarDates.Count();
        }

        private void Reset()
        {
            matchedJobs.Clear();
            currentIndex = 0;
            IsFirstElement = IsLastElement = true;
            MatchedJobsCount = 0;
        }

        public void Dispose()
        {
            calendarView.Exit();
            calendarView = null;
        }
    }
}
