using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.DTO
{
    public class PotentialJob
    {
        public int PotentialJobID { get; set; }
        
        public int ILvl { get; set; }
        public int ComfortLevel { get; set; }

        public int PlayerID { get; set; }
        public virtual Player Player { get; set; }

        public int JobID { get; set; }
        public virtual Job Job { get; set; }

        private ICollection<StaticMember> _staticMember;
        public virtual ICollection<StaticMember> StaticMember
        {
            get
            {
                return _staticMember ?? (_staticMember = new Collection<StaticMember>());
            }
            set
            {
                _staticMember = value;
            }
        }

    }
}
