using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.DomainModels.JobDomain
{
    public class Job
    {

        public Job(JobTypes jobType, string jobName, params JobAttributes[] attributes)
        {
            JobId = (int)jobType;
            JobType = jobType;
            JobName = jobName;
            Attributes = attributes;
        }

        protected Job() { }

        public int JobId { get; protected set; }
        public JobTypes JobType { get; protected set; }
        public string JobName { get; protected set; }
        public ICollection<JobAttributes> Attributes { get; protected set; }

    }
}
