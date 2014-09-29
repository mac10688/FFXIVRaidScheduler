using RaidScheduler.Domain.DomainModels.JobDomain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.DomainModels.RaidDomain
{
    public class Raid
    {
        public int RaidId { get; protected set; }
        public string RaidName { get; protected set; }
        public RaidCriteria RaidCriteria { get; protected set; }
        public RaidType RaidType { get; protected set; }

        protected Raid() { }

        public Raid(string raidName)
        {
            RaidName = raidName;
        }

        public Raid( RaidType raidType, string raidName, RaidCriteria raidCriteria)
        {
            RaidId = (int)raidType;
            RaidType = raidType;
            RaidName = raidName;
            RaidCriteria = raidCriteria;
        }

        /// <summary>
        /// Flattens the attributes needed for a raid
        /// </summary>
        /// <param name="raid"></param>
        /// <returns></returns>
        public ICollection<JobAttributes> FlattenAttributesNeededForRaid()
        {
            var allAttributesNeeded = new List<JobAttributes>();

            foreach(var crit in RaidCriteria.Criteria)
            {
                for (int i = 0; i < crit.Number; i++)
                {
                    allAttributesNeeded.Add(crit.Attribute);
                }
            }

            return allAttributesNeeded;
        }

    }
}
