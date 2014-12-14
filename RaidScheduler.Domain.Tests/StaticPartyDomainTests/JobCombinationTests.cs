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

namespace RaidScheduler.Domain.Tests.StaticPartyDomainTests
{
    [TestClass]
    public class JobCombinationTests
    {
        public const string CERBERUS = "Cerberus";

        [TestMethod]
        public void TwoTanks_TwoHealers_FourDps_CTurnOne()
        {
            var tankPlayer1 = new Player(Guid.NewGuid().ToString(), "Tank1", "Tank1", CERBERUS );
            var potentialJob1 = new PotentialJob(90, JobTypes.Paladin);
            tankPlayer1.PotentialJobs.Add(potentialJob1);

            var tankPlayer2 = new Player(Guid.NewGuid().ToString(), "Tank2", "Tank2", CERBERUS);
            var potentialJob2 = new PotentialJob(90, JobTypes.Warrior);
            tankPlayer2.PotentialJobs.Add(potentialJob2);

            var healerPlayer1 = new Player(Guid.NewGuid().ToString(), "Healer1", "Healer1", CERBERUS);
            var potentialJob3 = new PotentialJob(90, JobTypes.WhiteMage);
            healerPlayer1.PotentialJobs.Add(potentialJob3);

            var healerPlayer2 = new Player(Guid.NewGuid().ToString(), "Healer2", "Healer2", CERBERUS);
            var potentialJob4 = new PotentialJob(90, JobTypes.Scholar);
            healerPlayer2.PotentialJobs.Add(potentialJob4);

            var dpsPlayer1 = new Player(Guid.NewGuid().ToString(), "Dps1", "Dps1", CERBERUS);
            var potentialJob5 = new PotentialJob(90, JobTypes.Dragoon);
            dpsPlayer1.PotentialJobs.Add(potentialJob5);

            var dpsPlayer2 = new Player(Guid.NewGuid().ToString(), "Dps2", "Dps2", CERBERUS);
            var potentialJob6 = new PotentialJob(90, JobTypes.Bard);
            dpsPlayer2.PotentialJobs.Add(potentialJob6);

            var dpsPlayer3 = new Player(Guid.NewGuid().ToString(), "Dps3", "Dps3", CERBERUS);
            var potentialJob7 = new PotentialJob(90, JobTypes.BlackMage);
            dpsPlayer3.PotentialJobs.Add(potentialJob7);

            var dpsPlayer4 = new Player(Guid.NewGuid().ToString(), "Dps4", "Dps4", CERBERUS);
            var potentialJob8 = new PotentialJob(90, JobTypes.Summoner);
            dpsPlayer4.PotentialJobs.Add(potentialJob8);            

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
            var tankPlayer2 = new Player(Guid.NewGuid().ToString(), "Tank2", "Tank2", CERBERUS);
            var potentialJob2 = new PotentialJob(90, JobTypes.Warrior);
            tankPlayer2.PotentialJobs.Add(potentialJob2);

            var healerPlayer1 = new Player(Guid.NewGuid().ToString(), "Healer1", "Healer1", CERBERUS);
            var potentialJob3 = new PotentialJob(90, JobTypes.WhiteMage);
            healerPlayer1.PotentialJobs.Add(potentialJob3);

            var healerPlayer2 = new Player(Guid.NewGuid().ToString(), "Healer2", "Healer2", CERBERUS);
            var potentialJob4 = new PotentialJob(90, JobTypes.Scholar);
            healerPlayer2.PotentialJobs.Add(potentialJob4);

            var dpsPlayer1 = new Player(Guid.NewGuid().ToString(), "Dps1", "Dps1", CERBERUS);
            var potentialJob5 = new PotentialJob(90, JobTypes.Dragoon);
            dpsPlayer1.PotentialJobs.Add(potentialJob5);

            var dpsPlayer2 = new Player(Guid.NewGuid().ToString(), "Dps2", "Dps2", CERBERUS);
            var potentialJob6 = new PotentialJob(90, JobTypes.Bard);
            dpsPlayer2.PotentialJobs.Add(potentialJob6);

            var dpsPlayer3 = new Player(Guid.NewGuid().ToString(), "Dps3", "Dps3", CERBERUS);
            var potentialJob7 = new PotentialJob(90, JobTypes.BlackMage);
            dpsPlayer3.PotentialJobs.Add(potentialJob7);

            var dpsPlayer4 = new Player(Guid.NewGuid().ToString(), "Dps4", "Dps4", CERBERUS);
            var potentialJob8 = new PotentialJob(90, JobTypes.Summoner);
            dpsPlayer4.PotentialJobs.Add(potentialJob8);

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
            var tankPlayer1 = new Player(Guid.NewGuid().ToString(), "Tank1", "Tank1", CERBERUS);
            var potentialJob1 = new PotentialJob(90, JobTypes.Paladin);
            tankPlayer1.PotentialJobs.Add(potentialJob1);

            var tankPlayer2 = new Player(Guid.NewGuid().ToString(), "Tank2", "Tank2", CERBERUS);
            var potentialJob2 = new PotentialJob(90, JobTypes.Warrior);
            tankPlayer2.PotentialJobs.Add(potentialJob2);

            var healerPlayer1 = new Player(Guid.NewGuid().ToString(), "Healer1", "Healer1", CERBERUS);
            var potentialJob3 = new PotentialJob(90, JobTypes.WhiteMage);
            healerPlayer1.PotentialJobs.Add(potentialJob3);

            var healerPlayer2 = new Player(Guid.NewGuid().ToString(), "Healer2", "Healer2", CERBERUS);
            var potentialJob4 = new PotentialJob(90, JobTypes.Scholar);
            healerPlayer2.PotentialJobs.Add(potentialJob4);

            var dpsPlayer1 = new Player(Guid.NewGuid().ToString(), "Dps1", "Dps1", CERBERUS);
            var potentialJob5 = new PotentialJob(90, JobTypes.Dragoon);
            dpsPlayer1.PotentialJobs.Add(potentialJob5);

            var dpsPlayer2 = new Player(Guid.NewGuid().ToString(), "Dps2", "Dps2", CERBERUS);
            var potentialJob6 = new PotentialJob(90, JobTypes.Bard);
            dpsPlayer2.PotentialJobs.Add(potentialJob6);

            var dpsPlayer3 = new Player(Guid.NewGuid().ToString(), "Dps3", "Dps3", CERBERUS);
            var potentialJob7 = new PotentialJob(90, JobTypes.BlackMage);
            dpsPlayer3.PotentialJobs.Add(potentialJob7);

            var dpsPlayer4 = new Player(Guid.NewGuid().ToString(), "Dps4", "Dps4", CERBERUS);
            var potentialJob8 = new PotentialJob(90, JobTypes.Summoner);
            dpsPlayer4.PotentialJobs.Add(potentialJob8);

            var playerWithNoJob = new Player(Guid.NewGuid().ToString(), "BlankPlayer", "BlankPlayer", CERBERUS);

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

        [TestMethod]
        public void One_Player_With_All_Jobs_CoilTurn1()
        {
            var player1 = new Player(Guid.NewGuid().ToString(), "player1", "player1", CERBERUS);
            player1.SetPotentialJobs(new List<PotentialJob>
                {
                    new PotentialJob(90, JobTypes.Warrior),
                    new PotentialJob(90, JobTypes.Paladin),
                    new PotentialJob(90, JobTypes.WhiteMage),
                    new PotentialJob(90, JobTypes.Scholar),
                    new PotentialJob(90, JobTypes.Bard),
                    new PotentialJob(90, JobTypes.BlackMage),
                    new PotentialJob(90, JobTypes.Dragoon),
                    new PotentialJob(90, JobTypes.Summoner),
                    new PotentialJob(90, JobTypes.Monk)
                });

            var raidFactory = new RaidFactory();
            var coil1 = raidFactory.CreateRaid(RaidType.CoilTurn1);

            var jobFactory = new JobFactory();
            var jobs = jobFactory.GetAllJobs().ToList();

            var jobCombination = new JobCombination();
            var staticMembers = jobCombination.FindPotentialJobCombination(new List<Player> { player1 }, coil1, jobs);

            staticMembers.Should().BeNull();
        }

        [TestMethod]
        public void Player_With_One_Needed_Job_One()
        {
            var player1 = new Player(Guid.NewGuid().ToString(), "player1", "player1", CERBERUS);
            player1.SetPotentialJobs(new List<PotentialJob>
                {
                    new PotentialJob(90,JobTypes.Warrior),
                    new PotentialJob(90,JobTypes.Paladin)
                });

            var player2 = new Player(Guid.NewGuid().ToString(), "player2", "player2", CERBERUS);
            player2.SetPotentialJobs(new List<PotentialJob>
                {
                    new PotentialJob(90,JobTypes.Warrior)
                });

            var player3 = new Player(Guid.NewGuid().ToString(), "player3", "player3", CERBERUS);
            player3.SetPotentialJobs(new List<PotentialJob>
                {
                    new PotentialJob(90, JobTypes.WhiteMage)
                });

            var player4 = new Player(Guid.NewGuid().ToString(), "player4", "player4", CERBERUS);
            player4.SetPotentialJobs(new List<PotentialJob>
                {
                    new PotentialJob(90, JobTypes.WhiteMage)
                });

            var player5 = new Player(Guid.NewGuid().ToString(), "player5", "player5", CERBERUS);
            player5.SetPotentialJobs(new List<PotentialJob>
                {
                    new PotentialJob(90, JobTypes.Bard)
                });

            var player6 = new Player(Guid.NewGuid().ToString(), "player6", "player6", CERBERUS);
            player6.SetPotentialJobs(new List<PotentialJob>
                {
                    new PotentialJob(90, JobTypes.BlackMage)
                });

            var player7 = new Player(Guid.NewGuid().ToString(), "player7", "player7", CERBERUS);
            player7.SetPotentialJobs(new List<PotentialJob>
                {
                    new PotentialJob(90, JobTypes.Summoner)
                });

            var player8 = new Player(Guid.NewGuid().ToString(), "player8", "player8", CERBERUS);
            player8.SetPotentialJobs(new List<PotentialJob>
                {
                    new PotentialJob(90, JobTypes.Summoner)
                });

            var raidFactory = new RaidFactory();
            var coil1 = raidFactory.CreateRaid(RaidType.CoilTurn1);

            var jobFactory = new JobFactory();
            var jobs = jobFactory.GetAllJobs().ToList();

            var playerCollection = new List<Player> { player1, player2, player3, player4, player5, player6, player7, player8 };

            var jobCombination = new JobCombination();
            var staticMembers = jobCombination.FindPotentialJobCombination(playerCollection, coil1, jobs);

            staticMembers.Should().NotBeNull();
            staticMembers.Should().Contain(p => p.PlayerId == player1.PlayerId && p.ChosenJob == JobTypes.Paladin);
        }

    }
}
