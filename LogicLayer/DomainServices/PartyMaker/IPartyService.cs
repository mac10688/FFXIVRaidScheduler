using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaidScheduler.Domain.DomainModels.StaticPartyDomain;
using RaidScheduler.Domain.DomainModels.PlayerDomain;

namespace RaidScheduler.Domain.Services
{
    public interface IPartyService
    {
        /// <summary>
        /// Creates a static party from the collection of players given to it.
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        ICollection<StaticParty> CreateStaticPartiesFromPlayers(ICollection<Player> players);
    }
}
