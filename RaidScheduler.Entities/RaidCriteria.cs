using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Entities
{
    public class RaidCriteria
    {
        public int RaidCriteriaID { get; set; }
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

        public int RaidID { get; set; }
        public virtual Raid Raid { get; set; }

    }
}
