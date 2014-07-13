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
        /// <summary>
        /// Given a collection of players, find the common raids requested.
        /// </summary>
        /// <param name="playerCollection"></param>
        /// <returns></returns>
        ICollection<Raid> CommonRaidsRequested(ICollection<Player> playerCollection);
    }
}
