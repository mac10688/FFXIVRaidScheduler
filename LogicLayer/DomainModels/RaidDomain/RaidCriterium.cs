using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.DomainModels.RaidDomain
{
    public class RaidCriterium
    {
        public RaidCriterium(int number, JobDomain.JobAttributes attribute)
        {
            Number = number;
            Attribute = attribute;
        }

        public int Number { get; protected set; }
        public JobDomain.JobAttributes Attribute { get; protected set; }

    }
}
