using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaidScheduler.Domain.DomainModels.PlayerDomain;
using RaidScheduler.Domain.DomainModels.JobDomain;
using RaidScheduler.Domain.DomainModels.StaticPartyDomain;

namespace RaidScheduler.Domain.DomainModels
{
    public class PotentialJob
    {

        public PotentialJob(int iLvl, JobTypes job)
        {
            ILvl = iLvl;

            JobId = job;

        }

        protected PotentialJob() { }
        
        public int PotentialJobId { get; protected set; }

        public int ILvl { get; protected set; }

        public JobTypes JobId { get; protected set; }

    }
}
