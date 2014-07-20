using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaidScheduler.Domain.DomainModels;

namespace RaidScheduler.Domain.Helper
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

            var raidCriteria = new List<RaidCriteria>
                    {
                        new RaidCriteria(70, 8)
                        {
                            NumberOfDps = 4,
                            NumberOfTanks = 2,
                            NumberOfHealers = 2,
                            NumberOfSilencers = 2
                        }
                    };

            return new Raid("Coil: Turn 1", raidCriteria);
        }

        private Raid CoilTurn2()
        {
            var raidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria(73, 8)
                        {
                            NumberOfDps = 4,
                            NumberOfTanks = 1,
                            NumberOfHealers = 3,
                            NumberOfSilencers = 2
                        }
                    };
            
            return new Raid("Coil: Turn 2", raidCriteria);
        }

        private Raid CoilTurn3()
        {

            var raidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria(73, 8)
                        {
                            NumberOfDps = 4,
                            NumberOfTanks = 2,
                            NumberOfHealers = 2
                        }
                    };
            
            return new Raid("Coil: Turn 3", raidCriteria);

        }

        private Raid CoilTurn4()
        {

            var raidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria(77, 8)
                        {
                            NumberOfTanks = 2,
                            NumberOfHealers = 2,
                            NumberOfMagicalDps = 2,
                            NumberOfPhysicalDps = 2
                        }
                    };
            return new Raid("Coil: Turn 4", raidCriteria);
        }

        private Raid CoilTurn5()
        {

            var raidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria(82, 8)
                        {
                            NumberOfDps = 4,
                            NumberOfTanks = 2,
                            NumberOfHealers = 2
                        }
                    };
            return new Raid("Coil: Turn 5", raidCriteria);

        }

        private Raid GarudaExtreme()
        {

            var raidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria(67, 8)
                        {
                            NumberOfDps = 4,
                            NumberOfTanks = 2,
                            NumberOfHealers = 2,
                        }
                    };
            return new Raid("Garuda Extreme",raidCriteria);
            
        }

        private Raid TitanExtreme()
        {

            var raidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria(67, 8)
                        {
                            NumberOfDps = 4,
                            NumberOfTanks = 2,
                            NumberOfHealers = 2
                        }
                    };
            return new Raid("Titan Extreme", raidCriteria);
        }

        private Raid IfritExtreme()
        {
            var raidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria(70, 8)
                        {
                            NumberOfDps = 4,
                            NumberOfTanks = 2,
                            NumberOfHealers = 2,
                        }
                    };
            return new Raid("Ifrit Extreme", raidCriteria);
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
