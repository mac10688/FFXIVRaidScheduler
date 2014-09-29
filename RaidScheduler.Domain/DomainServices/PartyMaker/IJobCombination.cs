using RaidScheduler.Domain.DomainModels;
using RaidScheduler.Domain.DomainModels.JobDomain;
using RaidScheduler.Domain.DomainModels.PlayerDomain;
using RaidScheduler.Domain.DomainModels.RaidDomain;
using RaidScheduler.Domain.DomainModels.StaticPartyDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.Services
{
    public interface IJobCombination
    {
        ICollection<StaticMember> FindPotentialJobCombination(ICollection<Player> members, Raid raid, ICollection<Job> allJobs);
    }
}
