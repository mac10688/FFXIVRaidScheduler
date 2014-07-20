using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.DomainModels
{
    public class RaidCriteria
    {

        public RaidCriteria(int minLvl, int numberOfPlayersRequired)
        {
            MinILvl = minLvl;
            NumberOfPlayersRequired = numberOfPlayersRequired;
        }
        
        protected RaidCriteria() { }

        public int RaidCriteriaId { get; set; }
        public int? NumberOfStunners { get; set; }
        public int? NumberOfTanks { get; set; }
        public int? NumberOfHealers { get; set; }
        public int? NumberOfDps { get; set; }
        public int? NumberOfMeleeDps { get; set; }
        public int? NumberOfRangedDps { get; set; }
        public int? NumberOfMagicalDps{ get; set; }
        public int? NumberOfPhysicalDps { get; set; }
        public int? NumberOfSilencers { get; set; }
        public int MinILvl { get; set; }

        public int NumberOfPlayersRequired { get; set; }

    }


}
