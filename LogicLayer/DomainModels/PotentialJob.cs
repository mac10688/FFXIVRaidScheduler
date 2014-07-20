using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.DomainModels
{
    public class PotentialJob
    {

        public PotentialJob(int iLvl, int comfortLevel, Job job)
        {
            ILvl = iLvl;
            ComfortLevel = comfortLevel;

            Job = job;
            JobId = job.JobId;

        }

        protected PotentialJob() { }
        
        public int PotentialJobId { get; protected set; }

        public int ILvl { get; protected set; }
        public int ComfortLevel { get; protected set; }

        public int JobId { get; protected set; }
        public virtual Job Job { get; protected set; }

        private ICollection<StaticMember> _staticMember;
        public virtual ICollection<StaticMember> StaticMember
        {
            get
            {
                return _staticMember ?? (_staticMember = new Collection<StaticMember>());
            }
            protected set
            {
                _staticMember = value;
            }
        }

    }
}
