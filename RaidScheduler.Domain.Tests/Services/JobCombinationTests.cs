using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaidScheduler.Domain.Services;
using RaidScheduler.Domain.DomainModels;
using RaidScheduler.Domain.DomainModels.JobDomain;
using RaidScheduler.Domain.DomainModels.RaidDomain;
using System.Collections.Generic;

using FluentAssertions;
using RaidScheduler.Domain.DomainModels.PlayerDomain;

namespace RaidScheduler.Domain.Tests.Services
{
    [TestClass]
    public class JobCombinationTests
    {

        private Player tankPlayer1;
        private Player tankPlayer2;
        private Player healerPlayer1;
        private Player healerPlayer2;
        private Player dpsPlayer1;
        private Player dpsPlayer2;
        private Player dpsPlayer3;
        private Player dpsPlayer4;
        private Player playerWithNoJob;

        [TestInitialize]
        public void TestSetup()
        {
            var jobFactory = new JobFactory();

            tankPlayer1 = new Player(Guid.NewGuid().ToString(), "Tank1", "Tank1", "SomeTimezone");
            var potentialJob1 = new PotentialJob(90, 10, JobTypes.Paladin);
            tankPlayer1.PotentialJobs.Add(potentialJob1);

            tankPlayer2 = new Player(Guid.NewGuid().ToString(), "Tank2", "Tank2", "SomeTimezone");
            var potentialJob2 = new PotentialJob(90, 10, JobTypes.Warrior);
            tankPlayer2.PotentialJobs.Add(potentialJob2);

            healerPlayer1 = new Player(Guid.NewGuid().ToString(), "Healer1", "Healer1", "SomeTimezone");
            var potentialJob3 = new PotentialJob(90, 10, JobTypes.WhiteMage);
            healerPlayer1.PotentialJobs.Add(potentialJob3);

            healerPlayer2 = new Player(Guid.NewGuid().ToString(), "Healer2", "Healer2", "SomeTimezone");
            var potentialJob4 = new PotentialJob(90, 10, JobTypes.Scholar);
            healerPlayer2.PotentialJobs.Add(potentialJob4);

            dpsPlayer1 = new Player(Guid.NewGuid().ToString(), "Dps1", "Dps1", "SomeTimezone");
            var potentialJob5 = new PotentialJob(90, 10, JobTypes.Dragoon);
            dpsPlayer1.PotentialJobs.Add(potentialJob5);

            dpsPlayer2 = new Player(Guid.NewGuid().ToString(), "Dps2", "Dps2", "SomeTimezone");
            var potentialJob6 = new PotentialJob(90, 10, JobTypes.Bard);
            dpsPlayer2.PotentialJobs.Add(potentialJob6);

            dpsPlayer3 = new Player(Guid.NewGuid().ToString(), "Dps3", "Dps3", "SomeTimezone");
            var potentialJob7 = new PotentialJob(90, 10, JobTypes.BlackMage);
            dpsPlayer3.PotentialJobs.Add(potentialJob7);

            dpsPlayer4 = new Player(Guid.NewGuid().ToString(), "Dps4", "Dps4", "SomeTimezone");
            var potentialJob8 = new PotentialJob(90, 10, JobTypes.Summoner);
            dpsPlayer4.PotentialJobs.Add(potentialJob8);

            playerWithNoJob = new Player(Guid.NewGuid().ToString(), "BlankPlayer", "BlankPlayer", "SomeTimezone");

        }

        [TestMethod]
        public void TwoTanks_TwoHealers_FourDps_CTurnOne()
        {
            var playerCollection = new List<Player>{
                tankPlayer1, tankPlayer2, 
                healerPlayer1, healerPlayer2,
                dpsPlayer1, dpsPlayer2, dpsPlayer3, dpsPlayer4
            };

            var raidFactory = new RaidFactory();
            var coil1 = raidFactory.CreateRaid(RaidType.CoilTurn1);
            
            var jobFactory = new JobFactory();
            var jobs = jobFactory.GetAllJobs().ToList();

            var jobCombination = new JobCombination();
            var staticMembers = jobCombination.FindPotentialJobCombination(playerCollection, coil1, jobs);

            staticMembers.Should().NotBeNull();
            staticMembers.Should().ContainSingle(sm => sm.PlayerId == tankPlayer1.PlayerId);
        }
        
        [TestMethod]
        public void OneTank_TwoHealers_FourDps_CTurnOne()
        {
            var playerCollection = new List<Player>{
                tankPlayer2, 
                healerPlayer1, healerPlayer2,
                dpsPlayer1, dpsPlayer2, dpsPlayer3, dpsPlayer4
            };

            var raidFactory = new RaidFactory();
            var coil1 = raidFactory.CreateRaid(RaidType.CoilTurn1);

            var jobFactory = new JobFactory();            
            var allJobs = jobFactory.GetAllJobs().ToList();

            var jobCombination = new JobCombination();
            var staticMembers = jobCombination.FindPotentialJobCombination(playerCollection, coil1, allJobs);

            staticMembers.Should().BeNull();
        }

        [TestMethod]
        public void TwoTanks_TwoHealers_FourDps_OneJobless_CTurnOne()
        {
            var playerCollection = new List<Player>{
                tankPlayer1, tankPlayer2, 
                healerPlayer1, healerPlayer2,
                dpsPlayer1, dpsPlayer2, dpsPlayer3, dpsPlayer4, 
                playerWithNoJob
            };

            var raidFactory = new RaidFactory();
            var coil1 = raidFactory.CreateRaid(RaidType.CoilTurn1);

            var jobFactory = new JobFactory();
            var jobs = jobFactory.GetAllJobs().ToList();

            var jobCombination = new JobCombination();
            var staticMembers = jobCombination.FindPotentialJobCombination(playerCollection, coil1, jobs);

            staticMembers.Should().NotBeNull();
            staticMembers.Should().ContainSingle(sm => sm.PlayerId == tankPlayer1.PlayerId);
        }


    }
}
