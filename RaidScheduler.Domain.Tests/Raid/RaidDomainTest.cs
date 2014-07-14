using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using RaidScheduler.Domain;
using RaidScheduler.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RaidScheduler.Domain.Tests.Raid
{
    [TestClass]
    public class RaidDomainTest
    {
        [TestMethod]
        public void CommonRaidsRequested_BetweenTwoPlayers_Positive()
        {
            var domain = new RaidDomain();

            var player1 = new Player
            {
                RaidsRequested = new List<RaidRequested>()
                {
                    new RaidRequested
                    {
                        Raid = new RaidScheduler.Entities.Raid
                        {
                            RaidID = 1
                        }
                    },
                    new RaidRequested
                    {
                        Raid = new RaidScheduler.Entities.Raid
                        {
                            RaidID = 2
                        }
                    }
                }
            };

            var player2 = new Player
            {
                RaidsRequested = new List<RaidRequested>()
                {
                    new RaidRequested
                    {
                        Raid = new RaidScheduler.Entities.Raid
                        {
                            RaidID = 1
                        }
                    },
                    new RaidRequested
                    {
                        Raid = new RaidScheduler.Entities.Raid
                        {
                            RaidID = 3
                        }
                    }
                }
            };

            var players = new List<Player>
            {
                player1,
                player2
            };

            var result = domain.CommonRaidsRequested(players);

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.ToList()[0].RaidID == 1);
        }

        [TestMethod]
        public void CommonRaidsRequested_BetweenTwoPlayers_Negative()
        {
            var domain = new RaidDomain();

            var player1 = new Player
            {
                RaidsRequested = new List<RaidRequested>()
                {
                    new RaidRequested
                    {
                        Raid = new RaidScheduler.Entities.Raid
                        {
                            RaidID = 1
                        }
                    },
                    new RaidRequested
                    {
                        Raid = new RaidScheduler.Entities.Raid
                        {
                            RaidID = 2
                        }
                    }
                }
            };

            var player2 = new Player
            {
                RaidsRequested = new List<RaidRequested>()
                {
                    new RaidRequested
                    {
                        Raid = new RaidScheduler.Entities.Raid
                        {
                            RaidID = 4
                        }
                    },
                    new RaidRequested
                    {
                        Raid = new RaidScheduler.Entities.Raid
                        {
                            RaidID = 3
                        }
                    }
                }
            };

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
            var domain = new RaidDomain();
            var result = domain.CommonRaidsRequested(null);
            Assert.IsNotNull(result);
        }

    }
}
