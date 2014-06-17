using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Entities
{
    public class Raid
    {
        public int RaidID { get; set; }
        public string RaidName { get; set; }

        private ICollection<RaidCriteria> _raidCriteria;
        public virtual ICollection<RaidCriteria> RaidCriteria 
        { 
            get
            {
                return _raidCriteria ?? (_raidCriteria = new Collection<RaidCriteria>());
            }
            set
            {
                _raidCriteria = value;
            }
        }

        private ICollection<RaidRequested> _raidRequested;
        public virtual ICollection<RaidRequested> RaidRequested
        {
            get
            {
                return _raidRequested ?? (_raidRequested = new Collection<RaidRequested>());
            }
            set
            {
                _raidRequested = value;
            }
        }

        private ICollection<StaticParty> _staticParty;
        public virtual ICollection<StaticParty> StaticParty
        {
            get
            {
                return _staticParty ?? (_staticParty = new Collection<StaticParty>());
            }
            set
            {
                _staticParty = value;
            }
        }

    }
}
