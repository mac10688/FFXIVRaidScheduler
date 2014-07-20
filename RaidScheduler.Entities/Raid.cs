using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.DTO
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

    }
}
