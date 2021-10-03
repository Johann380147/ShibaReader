using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShibaReader.Models
{
    public class Calendar
    {
        public string Name { get; set; }
        public List<string> DateTimes { get; set; } = new();

        public Calendar() { }
        public Calendar(string name)
        {
            this.Name = name;
        }
    }
}
