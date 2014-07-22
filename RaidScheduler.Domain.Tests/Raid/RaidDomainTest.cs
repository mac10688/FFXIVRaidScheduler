using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using RaidScheduler.Domain.Services;
using RaidScheduler.Domain.DomainModels;
using System.Collections.Generic;
using System.Linq;

namespace RaidScheduler.Domain.Tests.RaidDomainTest
{
    [TestClass]
    public class RaidDomainTest
    {
        [TestMethod]
        public void CommonRaidsRequested_BetweenTwoPlayers_Positive()
        {
            var domain = new RaidService();

            var raid1 = new Raid("Coil Turn 1");
            var raidRequested1 = new RaidRequested(null, raid1, false);
            var player1 = new Player(Guid.NewGuid().ToString(), "test user1", "test user1", "central timezone");
            player1.AddToRaidRequested(raidRequested1);

            var raid2 = new Raid("Coil Turn 2");
            var raid3 = new Raid("Coil Turn 3");
            var raidRequested2 = new RaidRequested(null, raid2, false);
            var raidRequested3 = new RaidRequested(null, raid3, false);

            var player2 = new Player(Guid.NewGuid().ToString(), "test user 2", "test user 2", "central timezone");
            player2.AddToRaidRequested(raidRequested2);
            player2.AddToRaidRequested(raidRequested3);

            var players = new List<Player>
            {
                player1,
                player2
            };

            var result = domain.CommonRaidsRequested(players);

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.ToList()[0].RaidId == 1);
        }

        [TestMethod]
        public void CommonRaidsRequested_BetweenTwoPlayers_Negative()
        {
            var domain = new RaidService();

            var raid1 = new Raid("Coil Turn 1");
            var raid2 = new Raid("Coil Trun 2");
            var raidRequested1 = new RaidRequested(null, raid1, false);
            var raidRequested2 = new RaidRequested(null, raid2, false);
            var player1 = new Player(Guid.NewGuid().ToString(), "test user1", "test user1", "central timezone");
            player1.AddToRaidRequested(raidRequested1);
            player1.AddToRaidRequested(raidRequested2);

            var raid3 = new Raid("Coil Turn 3");
            var raid4 = new Raid("Coil Turn 4");
            var raidRequested3 = new RaidRequested(null, raid3, false);
            var raidRequested4 = new RaidRequested(null, raid4, false);

            var player2 = new Player(Guid.NewGuid().ToString(), "test user 2", "test user 2", "central timezone");
            player2.AddToRaidRequested(raidRequested3);
            player2.AddToRaidRequested(raidRequested4);

            var players = new List<Player>
            {
                player1,
                player2
            };

            var result = domain.CommonRaidsRequested(players);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void CommonRaidsRequested_PassInNullCollection()
        {
            var domain = new RaidService();
            var result = domain.CommonRaidsRequested(null);
            Assert.IsNotNull(result);
        }

    }
}
