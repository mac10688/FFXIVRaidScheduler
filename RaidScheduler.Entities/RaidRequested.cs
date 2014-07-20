using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.DTO
{
    public class RaidRequested
    {
        public int RaidRequestedID { get; set; }
        public bool FoundRaid { get; set; }

        public int RaidID { get; set; }
        public virtual Raid Raid { get; set; }

        public int PlayerID { get; set; }
        public virtual Player Player { get; set; }
    }
}
