using NodaTime;
using RaidScheduler.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain
{
    public class SchedulingDomain : ISchedulingDomain
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
                            var utcDayAndTime = ConvertToUTC(dayAndTime.DayAndTime, player.TimeZone);
                            var utcCommonDateAndTime = DayAndTimeOverlap(running, utcDayAndTime);
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
                    currentCollection = player.DaysAndTimesAvailable.Select(s => ConvertToUTC(s.DayAndTime, player.TimeZone)).ToList();
                }
                hasPassedFirstPlayer = true;
            }

            return currentCollection;
        }

        /// <summary>
        /// Given a dayAndTime object and bcl timezone, create a new DayAndTime object that is in UTC.
        /// </summary>
        /// <param name="dayAndTime"></param>
        /// <param name="timezone"></param>
        /// <returns></returns>
        private DayAndTime ConvertToUTC(DayAndTime dayAndTime, string timezone)
        {
            var tz = DateTimeZoneProviders.Bcl.GetZoneOrNull(timezone);
            var offset = tz.GetUtcOffset(SystemClock.Instance.Now);

            var result = ApplyOffsetToToDayAndTime(dayAndTime, -offset.Ticks);
            return result;
        }

        /// <summary>
        /// Given a dayAndTime in utc object and bcl timezone, create a new DayAndTime object that is translated to the new timezone.
        /// </summary>
        /// <param name="dayAndTime"></param>
        /// <param name="timezone"></param>
        /// <returns></returns>
        private DayAndTime ConvertUtcToTimezone(DayAndTime dayAndTime, string timezone)
        {
            var tz = DateTimeZoneProviders.Bcl.GetZoneOrNull(timezone);
            var offset = tz.GetUtcOffset(SystemClock.Instance.Now);

            var result = ApplyOffsetToToDayAndTime(dayAndTime, offset.Ticks);
            return result;
        }

        /// <summary>
        /// Given a dayAndTime object, apply the offset of ticks and return a new dayAndTime object with the applied offset.
        /// </summary>
        /// <param name="dayAndTime"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        private DayAndTime ApplyOffsetToToDayAndTime(DayAndTime dayAndTime, long offset)
        {
            var day = dayAndTime.DayOfWeek;
            var startTime = dayAndTime.TimeStart + offset;
            if (startTime < 0)
            {
                day = GoBackADay(dayAndTime.DayOfWeek);
                startTime = NodaConstants.TicksPerStandardDay + startTime;
            }
            else if (startTime > NodaConstants.TicksPerStandardDay)
            {
                day = GoForwardADay(dayAndTime.DayOfWeek);
                startTime = startTime - NodaConstants.TicksPerStandardDay;
            }

            var endTime = dayAndTime.TimeEnd + offset;
            if (endTime < 0)
            {
                endTime = NodaConstants.TicksPerStandardDay + endTime;
            }
            else if (endTime > NodaConstants.TicksPerStandardDay)
            {
                endTime = endTime - NodaConstants.TicksPerStandardDay;
            }

            var result = new DayAndTime();
            result.DayOfWeek = day;
            result.TimeStart = startTime;
            result.TimeEnd = endTime;
            return result;
        }

        /// <summary>
        /// Given an IsoDayOfWeek find the previous day. Note* IsoDayOfWeek is circular
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        private IsoDayOfWeek GoBackADay(IsoDayOfWeek day)
        {
            var newDay = (int)day - 1;
            newDay = (int)newDay == 0 ? 7 : newDay;
            var result = (IsoDayOfWeek)newDay;
            return result;
        }

        /// <summary>
        /// Given an IsoDayOfWeek find the next day. Note* IsoDayOfWeek is circular
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        private IsoDayOfWeek GoForwardADay(IsoDayOfWeek day)
        {
            var newDay = (int)day + 1;
            newDay = (int)newDay == 8 ? 1 : newDay;
            var result = (IsoDayOfWeek)newDay;
            return result;
        }

        /// <summary>
        /// Given a two dayAndTimeObjects, find their common time. Return new DayAndTime object.
        /// </summary>
        /// <param name="leftSide"></param>
        /// <param name="rightSide"></param>
        /// <returns>DayAndTime object</returns>
        private DayAndTime DayAndTimeOverlap(DayAndTime leftSide, DayAndTime rightSide)
        {
            DayAndTime result = null;
            if (leftSide.DayOfWeek == rightSide.DayOfWeek)
            {
                var leftStartTime = leftSide.TimeStart;
                var leftEndTime = leftSide.TimeEnd >= leftSide.TimeStart ? leftSide.TimeEnd : leftSide.TimeEnd + NodaConstants.TicksPerStandardDay;

                var rightStartTime = rightSide.TimeStart;
                var rightEndTime = rightSide.TimeEnd >= rightSide.TimeStart ? rightSide.TimeEnd : rightSide.TimeEnd + NodaConstants.TicksPerStandardDay;

                if (rightEndTime > leftStartTime && rightStartTime < leftEndTime)
                {
                    //Oh we got some overlap
                    long startTime;
                    if (rightStartTime > leftStartTime)
                    {
                        startTime = rightStartTime;
                    }
                    else
                    {
                        startTime = leftStartTime;
                    }

                    long endTime;
                    if (rightEndTime < leftEndTime)
                    {
                        endTime = rightEndTime;
                    }
                    else
                    {
                        endTime = leftEndTime;
                    }

                    result = new DayAndTime();
                    result.DayOfWeek = leftSide.DayOfWeek;
                    result.TimeStart = startTime;
                    result.TimeEnd = endTime <= NodaConstants.TicksPerStandardDay ? endTime : endTime - NodaConstants.TicksPerStandardDay;
                    result.IsTentative = rightSide.IsTentative;
                }
            }
            return result;
        }

    }
}
