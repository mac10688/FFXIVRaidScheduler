using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaidScheduler.Domain.DomainModels;

namespace RaidScheduler.Domain.Services
{
    public interface ISchedulingService
    {
        /// <summary>
        /// Given a collection of players, find a collection of contiguous play times.
        /// </summary>
        /// <param name="playerCollection"></param>
        /// <returns></returns>
        ICollection<DayAndTime> CommonScheduleAmongAllPlayers(ICollection<Player> playerCollection);
    }
}
