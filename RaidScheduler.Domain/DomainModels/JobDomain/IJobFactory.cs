using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.DomainModels.JobDomain
{
    public interface IJobFactory
    {
        Job CreateJob(JobTypes job);
        IEnumerable<Job> GetAllJobs();
    }
}
