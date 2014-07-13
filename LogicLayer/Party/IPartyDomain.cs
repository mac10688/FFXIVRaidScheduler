using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaidScheduler.Entities;

namespace RaidScheduler.Domain
{
    public interface IPartyDomain
    {
        /// <summary>
        /// Creates a static party from the collection of players given to it.
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        ICollection<StaticParty> CreateStaticPartiesFromPlayers(ICollection<Player> players);
    }
}
