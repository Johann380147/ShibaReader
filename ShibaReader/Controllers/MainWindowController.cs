using ShibaReader.Models;
using ShibaReader.Processors;
using ShibaReader.Utils;
using System.Collections.Generic;

namespace ShibaReader.Controllers
{
    class MainWindowController
    {
        private Dictionary<string, AutoSysJob> autoSysJobs = null;
        public void importJILFile()
        {
            string fileName = FileUtils.openFileChooser();
            if (fileName == null) return;

            JILProcessor jilProc = new JILProcessor(fileName);
            autoSysJobs = jilProc.processJILFile();
        }
    }
}
