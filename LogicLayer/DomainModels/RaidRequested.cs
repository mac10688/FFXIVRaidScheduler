using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.DomainModels
{
    public class RaidRequested
    {

        public RaidRequested(Player player, Raid raid, bool foundRaid)
        {
            Player = player;
            PlayerId = player.PlayerId;

            Raid = raid;
            RaidId = raid.RaidId;

            FoundRaid = foundRaid;
        }

        protected RaidRequested() { }

        public int RaidRequestedId { get; set; }
        public bool FoundRaid { get; set; }

        public int RaidId { get; set; }
        public virtual Raid Raid { get; set; }

        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
    }
}
