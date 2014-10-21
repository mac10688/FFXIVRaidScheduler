using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using RaidScheduler.Domain.DomainModels.JobDomain;
using FluentAssertions;


namespace RaidScheduler.Domain.Tests.JobDomainTests
{
    [TestClass]
    public class JobFactoryTests
    {
        
        [TestMethod]
        public void CreateWarriorTest()
        {
            var factory = new JobFactory();

            var warrior = factory.CreateJob(JobTypes.Warrior);

            warrior.JobName.Should().Be("Warrior");
            warrior.JobId.Should().Be((int)JobTypes.Warrior);
            warrior.JobType.Should().Be(JobTypes.Warrior);
            warrior.Attributes.Count.Should().Be(2);

        }

        [TestMethod]
        public void CreatePaladinTest()
        {
            var factory = new JobFactory();

            var paladin = factory.CreateJob(JobTypes.Paladin);

            paladin.JobName.Should().Be("Paladin");
            paladin.JobId.Should().Be((int)JobTypes.Paladin);
            paladin.JobType.Should().Be(JobTypes.Paladin);
            paladin.Attributes.Count.Should().Be(3);

        }

        [TestMethod]
        public void CreateMonkTest()
        {
            var factory = new JobFactory();

            var monk = factory.CreateJob(JobTypes.Monk);

            monk.JobName.Should().Be("Monk");
            monk.JobId.Should().Be((int)JobTypes.Monk);
            monk.JobType.Should().Be(JobTypes.Monk);
            monk.Attributes.Count.Should().Be(4);
        }

        [TestMethod]
        public void CreateDragoonTest()
        {
            var factory = new JobFactory();

            var dragoon = factory.CreateJob(JobTypes.Dragoon);

            dragoon.JobName.Should().Be("Dragoon");
            dragoon.JobId.Should().Be((int)JobTypes.Dragoon);
            dragoon.JobType.Should().Be(JobTypes.Dragoon);
            dragoon.Attributes.Count.Should().Be(3);
        }

        [TestMethod]
        public void CreateBardTest()
        {
            var factory = new JobFactory();

            var bard = factory.CreateJob(JobTypes.Bard);

            bard.JobName.Should().Be("Bard");
            bard.JobId.Should().Be((int)JobTypes.Bard);
            bard.JobType.Should().Be(JobTypes.Bard);
            bard.Attributes.Count.Should().Be(4);
        }

        [TestMethod]
        public void CreateWhiteMageTest()
        {
            var factory = new JobFactory();

            var whiteMage = factory.CreateJob(JobTypes.WhiteMage);

            whiteMage.JobName.Should().Be("White Mage");
            whiteMage.JobId.Should().Be((int)JobTypes.WhiteMage);
            whiteMage.JobType.Should().Be(JobTypes.WhiteMage);
            whiteMage.Attributes.Count.Should().Be(1);
        }

        [TestMethod]
        public void CreateBlackMageTest()
        {
            var factory = new JobFactory();

            var blackMage = factory.CreateJob(JobTypes.BlackMage);

            blackMage.JobName.Should().Be("Black Mage");
            blackMage.JobId.Should().Be((int)JobTypes.BlackMage);
            blackMage.JobType.Should().Be(JobTypes.BlackMage);
            blackMage.Attributes.Count.Should().Be(3);
        }

        [TestMethod]
        public void CreateSummonerTest()
        {
            var factory = new JobFactory();

            var warrior = factory.CreateJob(JobTypes.Summoner);

            warrior.JobName.Should().Be("Summoner");
            warrior.JobId.Should().Be((int)JobTypes.Summoner);
            warrior.JobType.Should().Be(JobTypes.Summoner);
            warrior.Attributes.Count.Should().Be(3);
        }

        [TestMethod]
        public void CreateScholarTest()
        {
            var factory = new JobFactory();

            var warrior = factory.CreateJob(JobTypes.Scholar);

            warrior.JobName.Should().Be("Scholar");
            warrior.JobId.Should().Be((int)JobTypes.Scholar);
            warrior.JobType.Should().Be(JobTypes.Scholar);
            warrior.Attributes.Count.Should().Be(1);
        }

    }
}
