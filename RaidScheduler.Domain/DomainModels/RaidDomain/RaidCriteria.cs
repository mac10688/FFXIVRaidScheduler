using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.DomainModels.RaidDomain
{
    public class RaidCriteria
    {

        public RaidCriteria(int minLvl, int numberOfPlayersRequired, ICollection<RaidCriterium> criteria)
        {
            MinILvl = minLvl;
            NumberOfPlayersRequired = numberOfPlayersRequired;
            Criteria = criteria;
        }
        
        protected RaidCriteria() { }

        public int RaidCriteriaId { get; protected set; }

        public int MinILvl { get; protected set; }
        public int NumberOfPlayersRequired { get; protected set; }
        public ICollection<RaidCriterium> Criteria { get; protected set; }

    }

    

}
