using RaidScheduler.Domain.Queries.UserDefinedParties.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.Queries.UserDefinedParties.Interfaces
{
    public interface IPlayerSearch
    {
        IEnumerable<PlayerSearchDTO> SearchPlayers(string server, string searchString);
        Player GetPlayer(string Id, string timezone);
    }
}
