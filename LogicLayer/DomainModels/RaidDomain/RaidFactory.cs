using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.DomainModels.RaidDomain
{
    public class RaidFactory : IRaidFactory
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

        public IEnumerable<Raid> GetAllRaids()
        {
            foreach(var enumValue in Enum.GetValues(typeof(RaidType)))
            {
                yield return CreateRaid((RaidType)enumValue);
            }
        }

        private Raid CoilTurn1()
        {
            var criteria = new List<RaidCriterium>();
            criteria.Add(new RaidCriterium(4, DomainModels.JobDomain.JobAttributes.Dps));
            criteria.Add(new RaidCriterium(2, DomainModels.JobDomain.JobAttributes.Tank));
            criteria.Add(new RaidCriterium(2, DomainModels.JobDomain.JobAttributes.Healer));
            criteria.Add(new RaidCriterium(2, DomainModels.JobDomain.JobAttributes.Silencer));

            var raidCriteria = new RaidCriteria(70, 8, criteria);

            return new Raid(RaidType.CoilTurn1, "Coil: Turn 1", raidCriteria);
        }

        private Raid CoilTurn2()
        {
            var criteria = new List<RaidCriterium>();
            criteria.Add(new RaidCriterium(4, DomainModels.JobDomain.JobAttributes.Dps));
            criteria.Add(new RaidCriterium(1, DomainModels.JobDomain.JobAttributes.Tank));
            criteria.Add(new RaidCriterium(3, DomainModels.JobDomain.JobAttributes.Healer));
            criteria.Add(new RaidCriterium(2, DomainModels.JobDomain.JobAttributes.Silencer));
            var raidCriteria = new RaidCriteria(73, 8, criteria);

            return new Raid(RaidType.CoilTurn2, "Coil: Turn 2", raidCriteria);
        }

        private Raid CoilTurn3()
        {
            var criteria = new List<RaidCriterium>();
            criteria.Add(new RaidCriterium(4, DomainModels.JobDomain.JobAttributes.Dps));
            criteria.Add(new RaidCriterium(2, DomainModels.JobDomain.JobAttributes.Tank));
            criteria.Add(new RaidCriterium(2, DomainModels.JobDomain.JobAttributes.Healer));

            var raidCriteria = new RaidCriteria(73, 8, criteria);

            return new Raid(RaidType.CoilTurn3, "Coil: Turn 3", raidCriteria);

        }

        private Raid CoilTurn4()
        {
            var criteria = new List<RaidCriterium>();

            criteria.Add(new RaidCriterium(2, DomainModels.JobDomain.JobAttributes.Tank));
            criteria.Add(new RaidCriterium(2, DomainModels.JobDomain.JobAttributes.MagicalDps));
            criteria.Add(new RaidCriterium(2, DomainModels.JobDomain.JobAttributes.PhysicalDps));
            var raidCriteria = new RaidCriteria(77, 8, criteria);

            return new Raid(RaidType.CoilTurn4, "Coil: Turn 4", raidCriteria);
        }

        private Raid CoilTurn5()
        {
            var criteria = new List<RaidCriterium>();
            criteria.Add(new RaidCriterium(4, DomainModels.JobDomain.JobAttributes.Dps));
            criteria.Add(new RaidCriterium(2, DomainModels.JobDomain.JobAttributes.Tank));
            criteria.Add(new RaidCriterium(2, DomainModels.JobDomain.JobAttributes.Healer));
            var raidCriteria = new RaidCriteria(82, 8, criteria);

            return new Raid(RaidType.CoilTurn5, "Coil: Turn 5", raidCriteria);

        }

        private Raid GarudaExtreme()
        {
            var criteria = new List<RaidCriterium>();
            criteria.Add(new RaidCriterium(4, DomainModels.JobDomain.JobAttributes.Dps));
            criteria.Add(new RaidCriterium(2, DomainModels.JobDomain.JobAttributes.Tank));
            criteria.Add(new RaidCriterium(2, DomainModels.JobDomain.JobAttributes.Healer));
            var raidCriteria = new RaidCriteria(67, 8, criteria);

            return new Raid(RaidType.GarudaExtreme, "Garuda Extreme", raidCriteria);
            
        }

        private Raid TitanExtreme()
        {
            var criteria = new List<RaidCriterium>();
            criteria.Add(new RaidCriterium(4, DomainModels.JobDomain.JobAttributes.Dps));
            criteria.Add(new RaidCriterium(2, DomainModels.JobDomain.JobAttributes.Tank));
            criteria.Add(new RaidCriterium(2, DomainModels.JobDomain.JobAttributes.Healer));

            var raidCriteria = new RaidCriteria(67, 8, criteria);

            return new Raid(RaidType.TitanExtreme, "Titan Extreme", raidCriteria);
        }

        private Raid IfritExtreme()
        {
            var criteria = new List<RaidCriterium>();
            criteria.Add(new RaidCriterium(4, DomainModels.JobDomain.JobAttributes.Dps));
            criteria.Add(new RaidCriterium(2, DomainModels.JobDomain.JobAttributes.Tank));
            criteria.Add(new RaidCriterium(2, DomainModels.JobDomain.JobAttributes.Healer));

            var raidCriteria = new RaidCriteria(70, 8, criteria);

            return new Raid(RaidType.IfritExtreme, "Ifrit Extreme", raidCriteria);
        }
        
    }


}
