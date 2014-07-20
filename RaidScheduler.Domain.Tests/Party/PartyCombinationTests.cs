using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using RaidScheduler.Domain;
using RaidScheduler.Domain.DomainModels;
using RaidScheduler.Domain.Repositories;
using System.Collections.Generic;

using FluentAssertions;
using RaidScheduler.Domain.Helper;

namespace RaidScheduler.Domain.Tests.Party
{
    [TestClass]
    public class PartyCombinationTests
    {
        [TestMethod]
        public void WhatDoesStaticPartyNeed_NeedTwoSilencers()
        {
            var jobRepo = new Mock<IRepository<Job>>();

            var jobFactory = new JobFactory();

            jobRepo.Setup(j => j.Get(null)).Returns(new List<Job>
                {
                    jobFactory.CreateJob(JobType.Paladin),
                    jobFactory.CreateJob(JobType.Warrior),
                    jobFactory.CreateJob(JobType.WhiteMage),
                    jobFactory.CreateJob(JobType.Scholar),
                    jobFactory.CreateJob(JobType.Summoner),
                    jobFactory.CreateJob(JobType.Dragoon),
                    jobFactory.CreateJob(JobType.Monk)
                });

            var raidRepo = new Mock<IRepository<Raid>>();
            var raidLogic = new Mock<IRaidDomain>();
            var schedule = new Mock<ISchedulingDomain>();

            var partyCombo = new PartyCombination(jobRepo.Object, raidRepo.Object, raidLogic.Object, schedule.Object);

            var raidFactory = new RaidFactory();

            var raid = raidFactory.CreateRaid(RaidType.CoilTurn1);

            var currentStaticMembers = new List<StaticMember>();
                
            var job1 = jobFactory.CreateJob(JobType.Warrior);
            var chosenPotentialJob1 = new PotentialJob(80, 9, job1);
            var staticMember1 = new StaticMember(null,null,chosenPotentialJob1);
            currentStaticMembers.Add(staticMember1);

            var job2 = jobFactory.CreateJob(JobType.WhiteMage);
            var chosenPotentialJob2 = new PotentialJob(80, 9, job2);
            var staticMember2 = new StaticMember(null,null,chosenPotentialJob2);
            currentStaticMembers.Add(staticMember2);

            var job3 = jobFactory.CreateJob(JobType.Scholar);
            var chosenPotentialJob3 = new PotentialJob(80, 9, job3);
            var staticMember3 = new StaticMember(null, null, chosenPotentialJob3);
            currentStaticMembers.Add(staticMember3);

            var job4 = jobFactory.CreateJob(JobType.BlackMage);
            var chosenPotentialJob4 = new PotentialJob(80, 9, job4);
            var staticMember4 = new StaticMember(null, null, chosenPotentialJob4);
            currentStaticMembers.Add(staticMember4);

            var job5 = jobFactory.CreateJob(JobType.Dragoon);
            var chosenPotentialJob5 = new PotentialJob(80, 9, job5);
            var staticMember5 = new StaticMember(null, null, chosenPotentialJob5);
            currentStaticMembers.Add(staticMember5);
                
            var job6 = jobFactory.CreateJob(JobType.Monk);
            var chosenPotentialJob6 = new PotentialJob(80, 9, job6);
            var staticMember6 = new StaticMember(null, null, chosenPotentialJob6);
            currentStaticMembers.Add(staticMember6);

            var result = partyCombo.WhatDoesStaticPartyNeed(raid, currentStaticMembers);

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }
    }
}
