using System.Collections.Generic;

namespace ShibaReader.Models
{
    public class History
    {

        public History Previous { get; set; }
        public History Next { get; set; }
        public List<string> MatchedJobs { get; set; }
        public string SearchText { get; set; }
        public int CurrentIndex { get; set; }

        public History(List<string> matchedJobs, string searchText, int currentIndex)
        {
            this.MatchedJobs = matchedJobs;
            this.SearchText = searchText;
            this.CurrentIndex = currentIndex;
        }
    }
}
