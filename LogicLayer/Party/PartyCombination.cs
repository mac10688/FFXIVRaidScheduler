using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NodaTime;
using RaidScheduler.Domain;
using RaidScheduler.Domain.Repositories;
using RaidScheduler.Domain.DomainModels;

using MoreLinq;

using Microsoft.AspNet;

namespace RaidScheduler.Domain
{
    public class PartyCombination : IPartyDomain
    {
        private readonly IRepository<Job> jobRepository;
        private readonly IRepository<Raid> raidRepository;
        private readonly IRaidDomain raidLogic;
        private readonly ISchedulingDomain schedulingLogic;

        public PartyCombination(IRepository<Job> jobRepository, IRepository<Raid> raidRepository, IRaidDomain raidLogic, ISchedulingDomain schedulingLogic)
        {
            this.jobRepository = jobRepository;
            this.raidRepository = raidRepository;
            this.raidLogic = raidLogic;
            this.schedulingLogic = schedulingLogic;
        }

        /// <summary>
        /// Creates a static party from the collection of players given.
        /// </summary>
        /// <param name="playerCollection"></param>
        /// <returns></returns>
        public ICollection<StaticParty> CreateStaticPartiesFromPlayers(ICollection<Player> playerCollection)
        {
            var result = new List<StaticParty>();
            
            var allRaids = raidRepository.Get();
            foreach (var raid in allRaids)
            {
                var raidPlayerCollection = playerCollection.Where(p => p.RaidsRequested.Where(r => r.Raid.RaidId == raid.RaidId).Any()).ToList();
                var stillPossibilities = false;
                if (raidPlayerCollection.Any())
                {
                    var currentParty = new StaticParty(raid, null, null);
                    result.Add(currentParty);
                    do
                    {
                        stillPossibilities = false;
                        foreach (var player in raidPlayerCollection.ToList())
                        {
                            var players = currentParty.StaticMembers.Select(p => p.Player).Concat(player).ToList();
                            var potentialStaticSchedules = schedulingLogic.CommonScheduleAmongAllPlayers(players);
                            if (potentialStaticSchedules.Any())
                            {
                                var staticMembers = FindPotentialPartyCombination(raid, currentParty, player);
                                //Ok, the player has the schedule and has the job, let's add him to the static party.
                                if (staticMembers.Select(m => m.Player).Contains(player))
                                {
                                    currentParty.SetStaticMembers(staticMembers);
                                    var times = potentialStaticSchedules.Select(p => new StaticPartyDayAndTimeSchedule(p, null)).ToList();
                                    currentParty.SetScheduledTimes(times);

                                    raidPlayerCollection.Remove(player);
                                    stillPossibilities = true;

                                }
                                if (currentParty.StaticMembers.Count() >= raid.RaidCriteria.First().NumberOfPlayersRequired)
                                {
                                    currentParty = new StaticParty(raid,null,null);
                                    result.Add(currentParty);
                                }
                            }
                        }

                    } while (stillPossibilities);
                }
            }
            return result;
        }

        /// <summary>
        /// Finds a collection of potential players that have common schedules, and necessary jobs to complete the raid.
        /// </summary>
        /// <param name="raidsRequested"></param>
        /// <param name="party"></param>
        /// <param name="potentialPlayer"></param>
        /// <returns></returns>
        public ICollection<StaticMember> FindPotentialPartyCombination(Raid raidsRequested, StaticParty party, Player potentialPlayer)
        {
            var jobsNeeded = WhatDoesStaticPartyNeed(raidsRequested, party.StaticMembers);
            //Is there a natural fit for the potential static member?
            foreach (var pJob in potentialPlayer.PotentialJobs)
            {
                //This player already fits, no need to rearrange the party
                if (jobsNeeded.Any(j => j.JobId == pJob.Job.JobId))
                {
                    var members = party.StaticMembers.ToList();
                    members.Add(new StaticMember(potentialPlayer, null, pJob));
                    return members;
                }
            }

            var players = party.StaticMembers.Select(p => p.Player).ToList();
            players.Add(potentialPlayer);

            //Let's see if we can rearrange the current party to fit the new potential player
            var newMembers = FindAPartyCombination(raidsRequested, players.Count, new List<StaticMember>(), players);

            return newMembers;
        }

        /// <summary>
        /// Finds a collection of potential players that have common schedules, and necessary jobs to complete the raid.
        /// This function is meant for recursion.
        /// </summary>
        /// <param name="raid"></param>
        /// <param name="limit"></param>
        /// <param name="allocatedMembers"></param>
        /// <param name="unallocatedmembers"></param>
        /// <returns></returns>
        public ICollection<StaticMember> FindAPartyCombination(Raid raid, int limit, ICollection<StaticMember> allocatedMembers, ICollection<Player> unallocatedmembers)
        {
            var jobsNeeded = WhatDoesStaticPartyNeed(raid, allocatedMembers);
            var result = new List<StaticMember>();
            foreach (var player in unallocatedmembers)
            {
                foreach (var job in player.PotentialJobs)
                {
                    if (jobsNeeded.Any(j => j.JobId == job.Job.JobId))
                    {
                        var newStaticMemberList = allocatedMembers.ToList();
                        newStaticMemberList.Add(new StaticMember(player, null, job));

                        if (newStaticMemberList.Count() >= limit)
                        {
                            return newStaticMemberList;
                        }

                        var newPlayerSelectionList = unallocatedmembers.ToList();
                        newPlayerSelectionList.Remove(player);

                        result = FindAPartyCombination(raid, limit, newStaticMemberList, newPlayerSelectionList).ToList();
                        if (result.Count() >= raid.RaidCriteria.First().NumberOfPlayersRequired)
                        {
                            return result;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a collection of jobs possibly needed for the raid.
        /// </summary>
        /// <param name="raidsRequested"></param>
        /// <param name="currentStaticMembers"></param>
        /// <returns></returns>
        public ICollection<Job> WhatDoesStaticPartyNeed(ICollection<Raid> raidsRequested, ICollection<StaticMember> currentStaticMembers)
        {
            var jobsNeededOverall = new List<Job>();
            foreach (var raid in raidsRequested)
            {
                var jobsNeeded = WhatDoesStaticPartyNeed(raid, currentStaticMembers);
                jobsNeededOverall.AddRange(jobsNeeded);
            }

            jobsNeededOverall = jobsNeededOverall.DistinctBy(j => j.JobId).ToList();
            return jobsNeededOverall;
        }

        /// <summary>
        /// Returns a collection of jobs possibly needed for the raid.
        /// </summary>
        /// <param name="raid"></param>
        /// <param name="currentStaticMembers"></param>
        /// <returns></returns>
        public ICollection<Job> WhatDoesStaticPartyNeed(Raid raid, ICollection<StaticMember> currentStaticMembers)
        {
            var result = new List<Job>();

            

            var chosenJobs = currentStaticMembers.Select(s => s.ChosenPotentialJob);

            var allJobs = jobRepository.Get();

            var criteria = raid.RaidCriteria.Single();

            if (criteria.NumberOfTanks.HasValue)
            {
                var numberOfTanks = chosenJobs.Where(j => j.Job.IsTank).Count();
                if (numberOfTanks < criteria.NumberOfTanks)
                {
                    var jobs = allJobs.Where(j => j.IsTank);
                    result.AddRange(jobs);
                }
            }
            if (criteria.NumberOfDps.HasValue)
            {
                var numberOfDps = chosenJobs.Where(j => j.Job.IsDps).Count();
                if (numberOfDps < criteria.NumberOfDps)
                {
                    var jobs = allJobs.Where(j => j.IsDps);
                    result.AddRange(jobs);
                }
            }
            if (criteria.NumberOfHealers.HasValue)
            {
                var numberOfHealers = chosenJobs.Where(j => j.Job.IsHealer).Count();
                if (numberOfHealers < criteria.NumberOfHealers)
                {
                    var jobs = allJobs.Where(j => j.IsHealer);
                    result.AddRange(jobs);
                }
            }
            if (criteria.NumberOfMagicalDps.HasValue)
            {
                var numberOfMagicalDps = chosenJobs.Where(j => j.Job.IsMagicalDps).Count();
                var totalNumberOfDpsAlreadyAdded = result.Where(r => r.IsDps).Count();
                var isDpsCountOk = true;
                if(criteria.NumberOfDps.HasValue)
                {
                    isDpsCountOk = result.Where(j => j.IsDps).Count() < criteria.NumberOfDps;
                }
                if (numberOfMagicalDps < criteria.NumberOfMagicalDps && isDpsCountOk)
                {
                    var jobs = allJobs.Where(j => j.IsMagicalDps);
                    result.AddRange(jobs);
                }
            }
            if (criteria.NumberOfMeleeDps.HasValue)
            {
                var numberOfMeleeDps = chosenJobs.Where(j => j.Job.IsMeleeDps).Count();
                var isDpsCountOk = true;
                if (criteria.NumberOfDps.HasValue)
                {
                    isDpsCountOk = result.Where(j => j.IsDps).Count() < criteria.NumberOfDps;
                }
                if (numberOfMeleeDps < criteria.NumberOfMeleeDps && isDpsCountOk)
                {
                    var jobs = allJobs.Where(j => j.IsMeleeDps);
                    result.AddRange(jobs);
                }
            }
            if (criteria.NumberOfPhysicalDps.HasValue)
            {
                var numberOfPhysicalDps = chosenJobs.Where(j => j.Job.IsPhysicalDps).Count();
                var isDpsCountOk = true;
                if (criteria.NumberOfDps.HasValue)
                {
                    isDpsCountOk = result.Where(j => j.IsDps).Count() < criteria.NumberOfDps;
                }
                if (numberOfPhysicalDps < criteria.NumberOfPhysicalDps && isDpsCountOk)
                {
                    var jobs = allJobs.Where(j => j.IsPhysicalDps);
                    result.AddRange(jobs);
                }
            }
            if (criteria.NumberOfRangedDps.HasValue)
            {
                var numberOfRangedDps = chosenJobs.Where(j => j.Job.IsRangedDps).Count();
                var isDpsCountOk = true;
                if (criteria.NumberOfDps.HasValue)
                {
                    isDpsCountOk = result.Where(j => j.IsDps).Count() < criteria.NumberOfDps;
                }
                if (numberOfRangedDps < criteria.NumberOfRangedDps && isDpsCountOk)
                {
                    var jobs = allJobs.Where(j => j.IsRangedDps);
                    result.AddRange(jobs);
                }
            }
            if (criteria.NumberOfSilencers.HasValue)
            {
                var numberOfSilencers = chosenJobs.Where(j => j.Job.CanSilence).Count();

                var isDpsCountOk = true;
                if(criteria.NumberOfDps.HasValue)
                {
                    isDpsCountOk = result.Where(j => j.IsDps).Count() < criteria.NumberOfDps;
                }

                var isTankCountOk = true;
                if(criteria.NumberOfTanks.HasValue)
                {
                    isTankCountOk = result.Where(j => j.IsTank).Count() < criteria.NumberOfTanks;
                }
                
                var isHealerCountOk = true;
                if(criteria.NumberOfHealers.HasValue)
                {
                    isHealerCountOk = result.Where(j => j.IsHealer).Count() < criteria.NumberOfHealers;
                }

                if (numberOfSilencers < criteria.NumberOfSilencers)
                {
                    var jobs = allJobs.Where(j => j.CanSilence && (j.IsTank == isTankCountOk || j.IsHealer == isHealerCountOk || j.IsDps == isDpsCountOk));
                    result.AddRange(jobs);
                }
            }
            if (criteria.NumberOfStunners.HasValue)
            {
                var numberOfStunners = chosenJobs.Where(j => j.Job.CanStun).Count();

                var isDpsCountOk = true;
                if (criteria.NumberOfDps.HasValue)
                {
                    isDpsCountOk = result.Where(j => j.IsDps).Count() < criteria.NumberOfDps;
                }

                var isTankCountOk = true;
                if (criteria.NumberOfTanks.HasValue)
                {
                    isTankCountOk = result.Where(j => j.IsTank).Count() < criteria.NumberOfTanks;
                }

                var isHealerCountOk = true;
                if (criteria.NumberOfHealers.HasValue)
                {
                    isHealerCountOk = result.Where(j => j.IsHealer).Count() < criteria.NumberOfHealers;
                }

                if (numberOfStunners < criteria.NumberOfStunners)
                {
                    var jobs = allJobs.Where(j => j.CanStun && (j.IsTank == isTankCountOk || j.IsHealer == isHealerCountOk || j.IsDps == isDpsCountOk));
                    result.AddRange(jobs);
                }
            }
            

            result = result.DistinctBy(j => j.JobId).ToList();

            return result;
        }
        
    }
}
