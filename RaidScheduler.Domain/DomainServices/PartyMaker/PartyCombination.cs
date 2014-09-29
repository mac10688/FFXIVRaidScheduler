using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NodaTime;
using RaidScheduler.Domain;
using RaidScheduler.Domain.Repositories;
using RaidScheduler.Domain.DomainModels;
using RaidScheduler.Domain.DomainModels.JobDomain;
using RaidScheduler.Domain.DomainModels.RaidDomain;

using RaidScheduler.Domain.DomainModels.Combinations;

using MoreLinq;

using Microsoft.AspNet;
using RaidScheduler.Domain.DomainModels.StaticPartyDomain;
using RaidScheduler.Domain.DomainModels.PlayerDomain;

namespace RaidScheduler.Domain.Services
{
    public class PartyCombinationService : IPartyService
    {
        private readonly ISchedulingDomainService _schedulingLogic;
        private readonly IJobCombination _jobCombinationLogic;
        private readonly IRaidFactory _raidFactory;
        private readonly IJobFactory _jobFactory;

        public PartyCombinationService(ISchedulingDomainService schedulingLogic, IJobCombination jobCombinationLogic, IRaidFactory raidFactory, IJobFactory jobFactory)
        {
            _schedulingLogic = schedulingLogic;
            _jobCombinationLogic = jobCombinationLogic;
            _raidFactory = raidFactory;
            _jobFactory = jobFactory;
        }

        /// <summary>
        /// Creates a static party from the collection of players given.
        /// </summary>
        /// <param name="playerCollection"></param>
        /// <returns></returns>
        public ICollection<StaticParty> CreateStaticPartiesFromPlayers(ICollection<Player> playerCollection)
        {
            var result = new List<StaticParty>();
            var distinctRaidTypes = playerCollection.SelectMany(p => p.RaidsRequested).Select(p => p.RaidType).DistinctBy(rr => rr);
            var allJobs = _jobFactory.GetAllJobs().ToList();
            foreach (var raidType in distinctRaidTypes)
            {
            FindPartiesForTheRaid:
                var raidPlayerCollection = playerCollection.Where(p => p != null && p.RaidsRequested.Where(r => r.RaidType == raidType && !r.FoundRaid).Any()).ToList();
                var raid = _raidFactory.CreateRaid(raidType);
                var numberOfPlayers = raid.RaidCriteria.NumberOfPlayersRequired;
                var combination = new Combinations<Player>(raidPlayerCollection, numberOfPlayers);
                foreach (var party in combination)
                {
                    var potentialSchedules = _schedulingLogic.CommonScheduleAmongAllPlayers(party);
                    if (potentialSchedules.Any())
                    {
                        var staticPartyMembers = _jobCombinationLogic.FindPotentialJobCombination(party, raid, allJobs);
                        if (staticPartyMembers != null)
                        {
                            var staticParty = new StaticParty(raidType, potentialSchedules, staticPartyMembers);
                            result.Add(staticParty);
                            foreach (var mem in party)
                            {
                                var raidsRequested = mem.RaidsRequested.Where(rr => rr.RaidType == raidType);
                                foreach (var raidRequested in raidsRequested)
                                {
                                    raidRequested.FoundRaid = true;
                                }
                            }
                            goto FindPartiesForTheRaid;
                        }
                    }
                }
            }
            return result;
        }

    }
}