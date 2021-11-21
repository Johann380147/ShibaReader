using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShibaReader.Models
{
    class ComplexJobCondition
    {
        public ComplexJobCondition NestedCondition { get; set; }
        public List<JobCondition> Conditions { get; set; }
        public List<string> Operators { get; set; }
    }
}
