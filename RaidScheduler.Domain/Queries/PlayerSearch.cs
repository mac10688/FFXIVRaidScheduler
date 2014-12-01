using RaidScheduler.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.SqlClient;
using RaidScheduler.Domain.Queries.Interfaces;
using RaidScheduler.Domain.Queries.DTOS;

namespace RaidScheduler.Domain.Queries
{
    public class PlayerSearch : IPlayerSearch
    {
        private readonly RaidSchedulerContext _context;
        public PlayerSearch(RaidSchedulerContext context)
        {
            _context = context;
        }

        public IEnumerable<PlayerSearchDTO> SearchPlayers(string searchString)
        {
            var firstnameOrLastName = searchString;
            var result = _context.Player.Where(p => p.FirstName.Contains(searchString) || p.LastName.Contains(searchString))
                .Select(p => new PlayerSearchDTO
                {
                    Id = p.PlayerId,
                    Name = p.FirstName + " " + p.LastName
                });
            return result;
        }
    }
}
