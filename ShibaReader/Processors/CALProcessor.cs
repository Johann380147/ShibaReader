using System.Collections.Generic;

namespace ShibaReader.Processors
{
    class CALProcessor
    {
        public string FileName { get; set; }

        public CALProcessor(string fileName)
        {
            this.FileName = fileName;
        }

        public Dictionary<string, List<string>> processCALFile()
        {
            Dictionary<string, List<string>> calendar = new Dictionary<string, List<string>>();



            return calendar;
        }
    }
}
