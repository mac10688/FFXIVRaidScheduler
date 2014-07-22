using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaidScheduler.Domain.DomainModels;

namespace RaidScheduler.Domain.Services
{
    public interface IRaidService
    {
        /// <summary>
        /// Given a collection of players, find the common raids requested.
        /// </summary>
        /// <param name="playerCollection"></param>
        /// <returns></returns>
        ICollection<Raid> CommonRaidsRequested(ICollection<Player> playerCollection);
    }
}
