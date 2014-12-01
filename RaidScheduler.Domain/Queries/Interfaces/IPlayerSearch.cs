using RaidScheduler.Domain.Queries.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.Queries.Interfaces
{
    public interface IPlayerSearch
    {
        IEnumerable<PlayerSearchDTO> SearchPlayers(string searchString);
    }
}
