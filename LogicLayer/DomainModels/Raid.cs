using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.DomainModels
{
    public class Raid
    {

        public int RaidId { get; protected set; }
        public string RaidName { get; protected set; }

        protected Raid() { }

        public Raid(string raidName)
        {
            RaidName = raidName;
        }

        public Raid( string raidName, ICollection<RaidCriteria> raidCriteria)
        {
            RaidName = raidName;
            RaidCriteria = raidCriteria;
        }        

        private ICollection<RaidCriteria> _raidCriteria;
        public virtual ICollection<RaidCriteria> RaidCriteria 
        { 
            get
            {
                return _raidCriteria ?? (_raidCriteria = new Collection<RaidCriteria>());
            }
            protected set
            {
                _raidCriteria = value;
            }
        }

    }
}
