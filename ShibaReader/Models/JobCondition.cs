using ShibaReader.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShibaReader.Models
{
    class JobCondition
    {
        public AutoSysJob Job { get; set; }
        public Enums.JobStatus JobStatus { get; set; }

        public JobCondition() { }
        public JobCondition(AutoSysJob job, Enums.JobStatus jobStatus)
        {
            this.Job = job;
            this.JobStatus = jobStatus;
        }
    }
}
