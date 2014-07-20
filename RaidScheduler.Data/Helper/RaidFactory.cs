using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaidScheduler.DTO;

namespace RaidScheduler.Data.Helper
{
    public class RaidFactory
    {

        public Raid CreateRaid(RaidType raid)
        {
            switch(raid)
            {
                case RaidType.CoilTurn1:
                    {
                        return CoilTurn1();
                    }
                case RaidType.CoilTurn2:
                    {
                        return CoilTurn2();
                    }
                case RaidType.CoilTurn3:
                    {
                        return CoilTurn3();
                    }
                case RaidType.CoilTurn4:
                    {
                        return CoilTurn4();
                    }
                case RaidType.CoilTurn5:
                    {
                        return CoilTurn5();
                    }
                case RaidType.GarudaExtreme:
                    {
                        return GarudaExtreme();
                    }
                case RaidType.TitanExtreme:
                    {
                        return TitanExtreme();
                    }
                case RaidType.IfritExtreme:
                    {
                        return IfritExtreme();
                    }
                default:
                    return null;
            }
                
        }

        private Raid CoilTurn1()
        {
            return new Raid
            {
                RaidID = 1,
                RaidName = "Coil: Turn 1",
                RaidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria
                        {
                            NumberOfDps = 4,
                            NumberOfTanks = 2,
                            NumberOfHealers = 2,
                            NumberOfSilencers = 2,
                            MinILvl = 70,
                            NumberOfPlayersRequired = 8
                        }
                    }
            };
        }

        private Raid CoilTurn2()
        {
            return new Raid
            {
                RaidID = 2,
                RaidName = "Coil: Turn 2",
                RaidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria
                        {
                            NumberOfDps = 4,
                            NumberOfTanks = 1,
                            NumberOfHealers = 3,
                            NumberOfSilencers = 2,
                            MinILvl = 73,
                            NumberOfPlayersRequired = 8
                        }
                    }
            };
        }

        private Raid CoilTurn3()
        {
            return new Raid
            {
                RaidID = 3,
                RaidName = "Coil: Turn 3",
                RaidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria
                        {
                            NumberOfDps = 4,
                            NumberOfTanks = 2,
                            NumberOfHealers = 2,
                            MinILvl = 73,
                            NumberOfPlayersRequired = 8
                        }
                    }
            };
        }

        private Raid CoilTurn4()
        {
            return new Raid
            {
                RaidID = 4,
                RaidName = "Coil: Turn 4",
                RaidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria
                        {
                            NumberOfTanks = 2,
                            NumberOfHealers = 2,
                            NumberOfMagicalDps = 2,
                            NumberOfPhysicalDps = 2,
                            MinILvl = 77,
                            NumberOfPlayersRequired = 8
                        }
                    }
            };
        }

        private Raid CoilTurn5()
        {
            return new Raid
            {
                RaidID = 5,
                RaidName = "Coil: Turn 5",
                RaidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria
                        {
                            NumberOfDps = 4,
                            NumberOfTanks = 2,
                            NumberOfHealers = 2,
                            MinILvl = 82,
                            NumberOfPlayersRequired = 8
                        }
                    }
            };
        }

        private Raid GarudaExtreme()
        {
            return new Raid
            {
                RaidID = 5,
                RaidName = "Coil: Turn 5",
                RaidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria
                        {
                            NumberOfDps = 4,
                            NumberOfTanks = 2,
                            NumberOfHealers = 2,
                            MinILvl = 82,
                            NumberOfPlayersRequired = 8
                        }
                    }
            };
        }

        private Raid TitanExtreme()
        {
            return new Raid
            {
                RaidID = 7,
                RaidName = "Titan Extreme",
                RaidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria
                        {
                            NumberOfDps = 4,
                            NumberOfTanks = 2,
                            NumberOfHealers = 2,
                            MinILvl = 67,
                            NumberOfPlayersRequired = 8
                        }
                    }
            };
        }

        private Raid IfritExtreme()
        {
            return new Raid
            {
                RaidID = 8,
                RaidName = "Ifrit Extreme",
                RaidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria
                        {
                            NumberOfDps = 4,
                            NumberOfTanks = 2,
                            NumberOfHealers = 2,
                            MinILvl = 70,
                            NumberOfPlayersRequired = 8
                        }
                    }
            };
        }

    }

    public enum RaidType
    {
        CoilTurn1,
        CoilTurn2,
        CoilTurn3,
        CoilTurn4,
        CoilTurn5,
        GarudaExtreme,
        TitanExtreme,
        IfritExtreme
    }
}
