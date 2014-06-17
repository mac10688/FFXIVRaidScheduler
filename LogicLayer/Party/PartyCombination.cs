using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NodaTime;
using RaidScheduler.Entities;
using RaidScheduler.Data;

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

        public ICollection<StaticParty> CreateStaticPartiesFromPlayers(ICollection<Player> playerCollection)
        {
            var result = new List<StaticParty>();
            
            var allRaids = raidRepository.Get();
            foreach (var raid in allRaids)
            {
                var raidPlayerCollection = playerCollection.Where(p => p.RaidsRequested.Where(r => r.Raid.RaidID == raid.RaidID).Any()).ToList();
                var stillPossibilities = false;
                if (raidPlayerCollection.Any())
                {
                    var currentParty = new StaticParty();
                    currentParty.Raid = raid;
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
                                    currentParty.StaticMembers = staticMembers;
                                    currentParty.ScheduledTimes = potentialStaticSchedules.Select(p => new StaticPartyDayAndTimeSchedule { DayAndTime = p}).ToList();

                                    raidPlayerCollection.Remove(player);
                                    stillPossibilities = true;

                                }
                                if (currentParty.StaticMembers.Count() >= raid.RaidCriteria.First().NumberOfPlayersRequired)
                                {
                                    currentParty = new StaticParty();
                                    currentParty.Raid = raid;
                                    result.Add(currentParty);
                                }
                            }
                        }

                    } while (stillPossibilities);
                }
            }
            return result;
        }

        public ICollection<StaticMember> FindPotentialPartyCombination(Raid raidsRequested, StaticParty party, Player potentialPlayer)
        {
            var jobsNeeded = WhatDoesStaticPartyNeed(raidsRequested, party.StaticMembers);
            //Is there a natural fit for the potential static member?
            foreach (var pJob in potentialPlayer.PotentialJobs)
            {
                //This player already fits, no need to rearrange the party
                if (jobsNeeded.Any(j => j.JobID == pJob.Job.JobID))
                {
                    var members = party.StaticMembers.ToList();
                    members.Add(new StaticMember { Player = potentialPlayer, ChosenPotentialJob = pJob });
                    return members;
                }
            }

            var players = party.StaticMembers.Select(p => p.Player).ToList();
            players.Add(potentialPlayer);

            //Let's see if we can rearrange the current party to fit the new potential player
            var newMembers = FindAPartyCombination(raidsRequested, players.Count, new List<StaticMember>(), players);

            return newMembers;
        }

        public ICollection<StaticMember> FindAPartyCombination(Raid raid, int limit, ICollection<StaticMember> allocatedMembers, ICollection<Player> unallocatedmembers)
        {
            var jobsNeeded = WhatDoesStaticPartyNeed(raid, allocatedMembers);
            var result = new List<StaticMember>();
            foreach (var player in unallocatedmembers)
            {
                foreach (var job in player.PotentialJobs)
                {
                    if (jobsNeeded.Any(j => j.JobID == job.Job.JobID))
                    {
                        var newStaticMemberList = allocatedMembers.ToList();
                        newStaticMemberList.Add(new StaticMember { ChosenPotentialJob = job, Player = player });

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

        //Create method that returns all possible static parties based on member setup
        public ICollection<Job> WhatDoesStaticPartyNeed(ICollection<Raid> raidsRequested, ICollection<StaticMember> currentStaticMembers)
        {
            var jobsNeededOverall = new List<Job>();
            foreach (var raid in raidsRequested)
            {
                var jobsNeeded = WhatDoesStaticPartyNeed(raid, currentStaticMembers);
                jobsNeededOverall.AddRange(jobsNeeded);
            }

            jobsNeededOverall = jobsNeededOverall.DistinctBy(j => j.JobID).ToList();
            return jobsNeededOverall;
        }

        public ICollection<Job> WhatDoesStaticPartyNeed(Raid raid, ICollection<StaticMember> currentStaticMembers)
        {
            var result = new List<Job>();

            var chosenJobs = currentStaticMembers.Select(s => s.ChosenPotentialJob);

            var allJobs = jobRepository.Get();

            var criteria = raid.RaidCriteria.Single();

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
                if (numberOfMagicalDps < criteria.NumberOfMagicalDps)
                {
                    var jobs = allJobs.Where(j => j.IsMagicalDps);
                    result.AddRange(jobs);
                }
            }
            if (criteria.NumberOfMeleeDps.HasValue)
            {
                var numberOfMeleeDps = chosenJobs.Where(j => j.Job.IsMeleeDps).Count();
                if (numberOfMeleeDps < criteria.NumberOfMeleeDps)
                {
                    var jobs = allJobs.Where(j => j.IsMeleeDps);
                    result.AddRange(jobs);
                }
            }
            if (criteria.NumberOfPhysicalDps.HasValue)
            {
                var numberOfPhysicalDps = chosenJobs.Where(j => j.Job.IsPhysicalDps).Count();
                if (numberOfPhysicalDps < criteria.NumberOfPhysicalDps)
                {
                    var jobs = allJobs.Where(j => j.IsPhysicalDps);
                    result.AddRange(jobs);
                }
            }
            if (criteria.NumberOfRangedDps.HasValue)
            {
                var numberOfRangedDps = chosenJobs.Where(j => j.Job.IsRangedDps).Count();
                if (numberOfRangedDps < criteria.NumberOfRangedDps)
                {
                    var jobs = allJobs.Where(j => j.IsRangedDps);
                    result.AddRange(jobs);
                }
            }
            if (criteria.NumberOfSilencers.HasValue)
            {
                var numberOfSilencers = chosenJobs.Where(j => j.Job.CanSilence).Count();
                if (numberOfSilencers < criteria.NumberOfSilencers)
                {
                    var jobs = allJobs.Where(j => j.CanSilence);
                    result.AddRange(jobs);
                }
            }
            if (criteria.NumberOfStunners.HasValue)
            {
                var numberOfStunners = chosenJobs.Where(j => j.Job.CanStun).Count();
                if (numberOfStunners < criteria.NumberOfStunners)
                {
                    var jobs = allJobs.Where(j => j.CanStun);
                    result.AddRange(jobs);
                }
            }
            if (criteria.NumberOfTanks.HasValue)
            {
                var numberOfTanks = chosenJobs.Where(j => j.Job.IsTank).Count();
                if (numberOfTanks < criteria.NumberOfTanks)
                {
                    var jobs = allJobs.Where(j => j.IsTank);
                    result.AddRange(jobs);
                }
            }

            result = result.DistinctBy(j => j.JobID).ToList();

            return result;
        }
        
    }
}
