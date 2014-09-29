using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using RaidScheduler.Domain.DomainModels.PlayerDomain;
using RaidScheduler.Domain.DomainModels.JobDomain;

namespace RaidScheduler.Domain.DomainModels.StaticPartyDomain
{
    public class StaticMember
    {

        public StaticMember(string playerId, JobTypes chosenJob)
        {
            PlayerId = playerId;
            ChosenJob = chosenJob;
        }

        protected StaticMember() { }
        
        public int StaticMemberId { get; protected set; }

        public string PlayerId { get; protected set; }
        public JobTypes ChosenJob { get; protected set; }

    }
}
