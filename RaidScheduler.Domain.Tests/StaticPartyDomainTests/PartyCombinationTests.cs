using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using RaidScheduler.Domain.Services;
using RaidScheduler.Domain.DomainModels;
using RaidScheduler.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using NodaTime;

using RaidScheduler.Domain.DomainModels.JobDomain;
using RaidScheduler.Domain.DomainModels.RaidDomain;
using RaidScheduler.Domain.DomainModels.PlayerDomain;
using RaidScheduler.Domain.DomainModels.SharedValueObject;
using RaidScheduler.Domain.DomainModels.StaticPartyDomain;

namespace RaidScheduler.Domain.Tests.StaticPartyDomainTests
{
    [TestClass]
    public class PartyCombinationTests
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

        private const string CentralStandardTime = "Central Standard Time";
        private const string EasternStandardTime = "Eastern Standard Time";

        [TestInitialize]
        public void TestSetup()
        {
            var jobFactory = new JobFactory();
            var raidFactory = new RaidFactory();

            var startTime = new LocalDateTime(2014, 9, 16, 11, 0, 0);
            var endTime = new LocalDateTime(2014, 9, 16, 13, 0, 0);

            var dayAndTime = new DayAndTime(IsoDayOfWeek.Monday, startTime, endTime);

            tankPlayer1 = new Player(Guid.NewGuid().ToString(), "Tank1", "Tank1", CentralStandardTime);
            var potentialJob1 = new PotentialJob(90, 10, JobTypes.Paladin);
            tankPlayer1.PotentialJobs.Add(potentialJob1);
            tankPlayer1.AddToRaidRequested(RaidType.CoilTurn1);
            tankPlayer1.AddToDayAndTimeAvailable(dayAndTime);

            tankPlayer2 = new Player(Guid.NewGuid().ToString(), "Tank2", "Tank2", CentralStandardTime);
            var potentialJob2 = new PotentialJob(90, 10, JobTypes.Warrior);
            tankPlayer2.PotentialJobs.Add(potentialJob2);
            tankPlayer2.AddToRaidRequested(RaidType.CoilTurn1);
            tankPlayer2.AddToDayAndTimeAvailable(dayAndTime);

            healerPlayer1 = new Player(Guid.NewGuid().ToString(), "Healer1", "Healer1", CentralStandardTime);
            var potentialJob3 = new PotentialJob(90, 10, JobTypes.WhiteMage);
            healerPlayer1.PotentialJobs.Add(potentialJob3);
            healerPlayer1.AddToRaidRequested(RaidType.CoilTurn1);
            healerPlayer1.AddToDayAndTimeAvailable(dayAndTime);

            healerPlayer2 = new Player(Guid.NewGuid().ToString(), "Healer2", "Healer2", CentralStandardTime);
            var potentialJob4 = new PotentialJob(90, 10, JobTypes.Scholar);
            healerPlayer2.PotentialJobs.Add(potentialJob4);
            healerPlayer2.AddToRaidRequested(RaidType.CoilTurn1);
            healerPlayer2.AddToDayAndTimeAvailable(dayAndTime);

            dpsPlayer1 = new Player(Guid.NewGuid().ToString(), "Dps1", "Dps1", CentralStandardTime);
            var potentialJob5 = new PotentialJob(90, 10, JobTypes.Dragoon);
            dpsPlayer1.PotentialJobs.Add(potentialJob5);
            dpsPlayer1.AddToRaidRequested(RaidType.CoilTurn1);
            dpsPlayer1.AddToDayAndTimeAvailable(dayAndTime);

            dpsPlayer2 = new Player(Guid.NewGuid().ToString(), "Dps2", "Dps2", CentralStandardTime);
            var potentialJob6 = new PotentialJob(90, 10, JobTypes.Bard);
            dpsPlayer2.PotentialJobs.Add(potentialJob6);
            dpsPlayer2.AddToRaidRequested(RaidType.CoilTurn1);
            dpsPlayer2.AddToDayAndTimeAvailable(dayAndTime);

            dpsPlayer3 = new Player(Guid.NewGuid().ToString(), "Dps3", "Dps3", CentralStandardTime);
            var potentialJob7 = new PotentialJob(90, 10, JobTypes.BlackMage);
            dpsPlayer3.PotentialJobs.Add(potentialJob7);
            dpsPlayer3.AddToRaidRequested(RaidType.CoilTurn1);
            dpsPlayer3.AddToDayAndTimeAvailable(dayAndTime);

            dpsPlayer4 = new Player(Guid.NewGuid().ToString(), "Dps4", "Dps4", CentralStandardTime);
            var potentialJob8 = new PotentialJob(90, 10, JobTypes.Summoner);
            dpsPlayer4.PotentialJobs.Add(potentialJob8);
            dpsPlayer4.AddToRaidRequested(RaidType.CoilTurn1);
            dpsPlayer4.AddToDayAndTimeAvailable(dayAndTime);

            playerWithNoJob = new Player(Guid.NewGuid().ToString(), "BlankPlayer", "BlankPlayer", CentralStandardTime);

        }

        [TestMethod]
        public void CreateParty_TwoTank_TwoHealer_FourDps_TwoSilencers_CoilTurnOne_AllSameTime_Positive()
        {
            var jobCombinationLogic = new Mock<IJobCombination>();
            jobCombinationLogic.Setup(j => 
                j.FindPotentialJobCombination(
                It.Is<List<Player>>(l => 
                    l.Any(p => p == tankPlayer1) &&
                    l.Any(p => p == tankPlayer2) &&
                    l.Any(p => p == healerPlayer1) &&
                    l.Any(p => p == healerPlayer2) &&
                    l.Any(p => p == dpsPlayer1) &&
                    l.Any(p => p == dpsPlayer2) &&
                    l.Any(p => p == dpsPlayer3) &&
                    l.Any(p => p == dpsPlayer4)
                )                
                , It.Is<Raid>(r => r.RaidType == RaidType.CoilTurn1)
                , It.IsAny<ICollection<Job>>()
                ))
                .Returns(() => new List<StaticMember>()
                {
                    new StaticMember(tankPlayer1.PlayerId, tankPlayer1.PotentialJobs.First().JobId),
                    new StaticMember(tankPlayer2.PlayerId, tankPlayer2.PotentialJobs.First().JobId),
                    new StaticMember(healerPlayer1.PlayerId, healerPlayer1.PotentialJobs.First().JobId),
                    new StaticMember(healerPlayer2.PlayerId, healerPlayer2.PotentialJobs.First().JobId),
                    new StaticMember(dpsPlayer1.PlayerId, dpsPlayer1.PotentialJobs.First().JobId),
                    new StaticMember(dpsPlayer2.PlayerId, dpsPlayer2.PotentialJobs.First().JobId),
                    new StaticMember(dpsPlayer3.PlayerId, dpsPlayer3.PotentialJobs.First().JobId),
                    new StaticMember(dpsPlayer4.PlayerId, dpsPlayer4.PotentialJobs.First().JobId)
                });


            var schedule = new Mock<ISchedulingDomainService>();
            schedule.Setup(s => s.CommonScheduleAmongAllPlayers(It.IsAny<List<Player>>())).Returns(() => new List<DayAndTime>
                {
                    new DayAndTime(IsoDayOfWeek.Monday, new LocalDateTime(2014, 9, 16, 16, 0, 0),new LocalDateTime(2014, 9, 16, 18, 0, 0))
                });

            var coilTurn1 = new Raid(RaidType.CoilTurn1, "Coil Turn 1",
                new RaidCriteria(80, 8, new List<RaidCriterium>
                    {
                        new RaidCriterium(2, JobAttributes.Tank),
                        new RaidCriterium(2, JobAttributes.Healer),
                        new RaidCriterium(4, JobAttributes.Dps),
                        new RaidCriterium(2, JobAttributes.Silencer)
                    })
                );

            var raidFactory = new Mock<IRaidFactory>();
            raidFactory.Setup(rf => rf.CreateRaid(It.Is<RaidType>(r => r == RaidType.CoilTurn1))).Returns(() => coilTurn1);


            var jobFacotry = new Mock<IJobFactory>();

            var paladinJob = new Job(JobTypes.Paladin, "Paladin", JobAttributes.Tank, JobAttributes.Stunner, JobAttributes.Silencer);
            jobFacotry.Setup(jf => jf.CreateJob(It.Is<JobTypes>(j => j == JobTypes.Paladin))).Returns(() => paladinJob);

            var warriorJob = new Job(JobTypes.Warrior, "Warrior", JobAttributes.Tank, JobAttributes.Stunner);
            jobFacotry.Setup(jf => jf.CreateJob(It.Is<JobTypes>(j => j == JobTypes.Warrior))).Returns(() => warriorJob);

            var whmJob = new Job(JobTypes.WhiteMage, "White Mage", JobAttributes.Healer);
            jobFacotry.Setup(jf => jf.CreateJob(It.Is<JobTypes>(j => j == JobTypes.WhiteMage))).Returns(() => whmJob);

            var schJob = new Job(JobTypes.WhiteMage, "Scholar", JobAttributes.Healer);
            jobFacotry.Setup(jf => jf.CreateJob(It.Is<JobTypes>(j => j == JobTypes.Scholar))).Returns(() => schJob);

            var smnJob = new Job(JobTypes.Summoner, "Summoner", JobAttributes.Dps, JobAttributes.RangedDps, JobAttributes.MagicalDps);
            jobFacotry.Setup(jf => jf.CreateJob(It.Is<JobTypes>(j => j == JobTypes.Summoner))).Returns(() => smnJob);

            var drgJob = new Job(JobTypes.Dragoon, "Dragoon", JobAttributes.Dps, JobAttributes.MeleeDps, JobAttributes.PhysicalDps);
            jobFacotry.Setup(jf => jf.CreateJob(It.Is<JobTypes>(j => j == JobTypes.Dragoon))).Returns(() => drgJob);

            var blmJob = new Job(JobTypes.BlackMage, "Black Mage", JobAttributes.Dps, JobAttributes.RangedDps, JobAttributes.MagicalDps);
            jobFacotry.Setup(jf => jf.CreateJob(It.Is<JobTypes>(j => j == JobTypes.BlackMage))).Returns(() => blmJob);

            var brdJob = new Job(JobTypes.Bard, "Bard", JobAttributes.Dps, JobAttributes.RangedDps, JobAttributes.PhysicalDps, JobAttributes.Silencer);
            jobFacotry.Setup(jf => jf.CreateJob(It.Is<JobTypes>(j => j == JobTypes.Bard))).Returns(() => brdJob);

            var partyCombo = new PartyCombinationService(schedule.Object, jobCombinationLogic.Object, raidFactory.Object, jobFacotry.Object);

            var playerCollection = new List<Player>()
            {
                tankPlayer1,
                tankPlayer2,
                healerPlayer1,
                healerPlayer2,
                dpsPlayer1,
                dpsPlayer2,
                dpsPlayer3,
                dpsPlayer4
            };

            var result = partyCombo.CreateStaticPartiesFromPlayers(playerCollection);
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            
            var staticParty = result.Single();
            staticParty.Should().NotBeNull();
            staticParty.ScheduledTimes.Should().HaveCount(1);

            var scheduledTime = staticParty.ScheduledTimes.Single();
            scheduledTime.DayAndTime.Should().NotBeNull();
            scheduledTime.DayAndTime.DayOfWeek.Should().Be(IsoDayOfWeek.Monday);
            scheduledTime.DayAndTime.TimeStart.Should().Be( new LocalDateTime(2014, 9, 16, 16, 0, 0).TickOfDay );
            scheduledTime.DayAndTime.TimeEnd.Should().Be(new LocalDateTime(2014, 9, 16, 18, 0, 0).TickOfDay);
            
            var listOfMembers = staticParty.StaticMembers;
            listOfMembers.Should().NotBeNull();
            listOfMembers.Should().Contain(p => p.PlayerId == tankPlayer1.PlayerId);
            listOfMembers.Should().Contain(p => p.PlayerId == tankPlayer2.PlayerId);
            listOfMembers.Should().Contain(p => p.PlayerId == healerPlayer1.PlayerId);
            listOfMembers.Should().Contain(p => p.PlayerId == healerPlayer2.PlayerId);
            listOfMembers.Should().Contain(p => p.PlayerId == dpsPlayer1.PlayerId);
            listOfMembers.Should().Contain(p => p.PlayerId == dpsPlayer2.PlayerId);
            listOfMembers.Should().Contain(p => p.PlayerId == dpsPlayer3.PlayerId);
            listOfMembers.Should().Contain(p => p.PlayerId == dpsPlayer4.PlayerId);

        }
    
    }
}
