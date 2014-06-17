using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaidScheduler.Entities;

namespace RaidScheduler.Domain
{
    public interface IRaidDomain
    {
        ICollection<Raid> CommonRaidsRequested(ICollection<Player> playerCollection);
    }
}
