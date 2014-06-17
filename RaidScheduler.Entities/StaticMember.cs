using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace RaidScheduler.Entities
{
    public class StaticMember
    {
        public int StaticMemberID { get; set; }

        public int PlayerID { get; set; }
        public virtual Player Player { get; set; }

        public int ChosenPotentialJobID { get; set; }
        public virtual PotentialJob ChosenPotentialJob { get; set; }

        public int StaticPartyID { get; set; }
        public virtual StaticParty StaticParty { get; set; }

    }
}
