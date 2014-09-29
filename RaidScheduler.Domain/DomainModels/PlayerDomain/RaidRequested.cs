using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidScheduler.Domain.DomainModels.RaidDomain;

namespace RaidScheduler.Domain.DomainModels.PlayerDomain
{
    public class RaidRequested
    {

        public RaidRequested(RaidType raid, bool foundRaid)
        {
            RaidType = raid;
            FoundRaid = foundRaid;
        }

        protected RaidRequested() { }

        public int RaidRequestedId { get; protected set; }
        public bool FoundRaid { get; set; }

        public RaidType RaidType { get; protected set; }

    }
}
