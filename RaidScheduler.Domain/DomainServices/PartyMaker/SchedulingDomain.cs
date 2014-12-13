using NodaTime;
using RaidScheduler.Domain.DomainModels;
using RaidScheduler.Domain.DomainModels.PlayerDomain;
using RaidScheduler.Domain.DomainModels.SharedValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.Services
{
    public class SchedulingDomain : ISchedulingDomainService
    {
        /// <summary>
        /// Given a collection of players, find a collection of contiguous play times.
        /// </summary>
        /// <param name="playerCollection"></param>
        /// <returns></returns>
        public ICollection<DayAndTime> CommonScheduleAmongAllPlayers(ICollection<Player> playerCollection)
        {
            var currentCollection = new List<DayAndTime>();
            var hasPassedFirstPlayer = false;
            foreach (var player in playerCollection)
            {
                if (hasPassedFirstPlayer)
                {
                    var newRunningSchedule = new List<DayAndTime>();
                    foreach (var dayAndTime in player.DaysAndTimesAvailable)
                    {
                        foreach (var running in currentCollection)
                        {
                            var utcCommonDateAndTime = running.DayAndTimeOverlap( dayAndTime.DayAndTime);
                            if (utcCommonDateAndTime != null)
                            {
                                newRunningSchedule.Add(utcCommonDateAndTime);
                            }
                        }
                    }
                    currentCollection = newRunningSchedule;
                }
                else
                {
                    currentCollection = player.DaysAndTimesAvailable.Select(s => s.DayAndTime.ConvertToUTC()).ToList();
                }
                hasPassedFirstPlayer = true;
            }
            return currentCollection;
        }




    }
}
