using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;

namespace RaidScheduler.Domain.DomainModels.SharedValueObject
{
    public class DayAndTime
    {

        private static readonly string UTC_TIMEZONE_ID = TimeZoneInfo.Utc.Id;
        
        public DayAndTime(IsoDayOfWeek dayOfWeek, long timeStart, long timeEnd, string timezone)
        {
            DayOfWeek = dayOfWeek;
            TimeStart = timeStart;            
            TimeEnd = timeEnd;
            Timezone = timezone;
        }

        public DayAndTime(IsoDayOfWeek dayOfWeek, LocalDateTime timeStart, LocalDateTime timeEnd, string timezone)
        {
            DayOfWeek = dayOfWeek;
            TimeStart = timeStart.TickOfDay;
            TimeEnd = timeEnd.TickOfDay;
            Timezone = timezone;
        }

        protected DayAndTime() { }

        public IsoDayOfWeek DayOfWeek { get; protected set; }
        public long TimeStart { get; protected set; }
        public long TimeEnd { get; protected set; }
        public string Timezone { get; set; }

        /// <summary>
        /// Given an IsoDayOfWeek find the previous day. Note* IsoDayOfWeek is circular
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public IsoDayOfWeek GoBackADay(IsoDayOfWeek day)
        {
            var newDay = (int)day - 1;
            newDay = (int)newDay == 1 ? 7 : newDay;
            var result = (IsoDayOfWeek)newDay;
            return result;
        }

        /// <summary>
        /// Given an IsoDayOfWeek find the next day. Note* IsoDayOfWeek is circular
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public IsoDayOfWeek GoForwardADay(IsoDayOfWeek day)
        {
            var newDay = (int)day + 1;
            newDay = (int)newDay == 8 ? 1 : newDay;
            var result = (IsoDayOfWeek)newDay;
            return result;
        }

        /// <summary>
        /// Given a dayAndTime object, apply the offset of ticks and return a new dayAndTime object with the applied offset.
        /// </summary>
        /// <param name="dayAndTime"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        private Tuple<IsoDayOfWeek, long, long> ApplyOffsetToToDayAndTime(long offset)
        {
            var day = DayOfWeek;
            var startTime = TimeStart + offset;
            if (startTime <= 0)
            {
                day = GoBackADay(DayOfWeek);
                startTime = NodaConstants.TicksPerStandardDay + startTime;
            }
            else if (startTime >= NodaConstants.TicksPerStandardDay)
            {
                day = GoForwardADay(DayOfWeek);
                startTime = startTime - NodaConstants.TicksPerStandardDay;
            }

            var endTime = TimeEnd + offset;
            if (endTime <= 0)
            {
                endTime = NodaConstants.TicksPerStandardDay + endTime;
            }
            else if (endTime >= NodaConstants.TicksPerStandardDay)
            {
                endTime = endTime - NodaConstants.TicksPerStandardDay;
            }

            var result = Tuple.Create(day, startTime, endTime);
            return result;
        }

        /// <summary>
        /// Given a dayAndTime object and bcl timezone, create a new DayAndTime object that is in UTC.
        /// </summary>
        /// <param name="dayAndTime"></param>
        /// <param name="timezone"></param>
        /// <returns></returns>
        public DayAndTime ConvertToUTC()
        {
            var tz = DateTimeZoneProviders.Bcl.GetZoneOrNull(Timezone);            
            var offset = tz.GetUtcOffset(SystemClock.Instance.Now);

            var adjustedTime = ApplyOffsetToToDayAndTime(-offset.Ticks);
            var result = new DayAndTime(adjustedTime.Item1, adjustedTime.Item2, adjustedTime.Item3, UTC_TIMEZONE_ID);
            return result;
        }

        /// <summary>
        /// Given a dayAndTime in utc object and bcl timezone, create a new DayAndTime object that is translated to the new timezone.
        /// </summary>
        /// <param name="dayAndTime"></param>
        /// <param name="timezone"></param>
        /// <returns></returns>
        public DayAndTime ConvertToTimezone(string timezone)
        {
            var tz = DateTimeZoneProviders.Bcl.GetZoneOrNull(timezone);
            var offset = tz.GetUtcOffset(SystemClock.Instance.Now);

            var utcTime = this.ConvertToUTC();

            var adjustedTime = utcTime.ApplyOffsetToToDayAndTime( offset.Ticks);
            var result = new DayAndTime(adjustedTime.Item1, adjustedTime.Item2, adjustedTime.Item3, timezone);
            return result;
        }

        /// <summary>
        /// Given a two dayAndTimeObjects, find their common time. Converts both timezones to utc for comparison.
        /// </summary>
        /// <param name="leftSide"></param>
        /// <param name="rightSide"></param>
        /// <returns>DayAndTime object</returns>
        public DayAndTime DayAndTimeOverlap(DayAndTime rightSide)
        {
            DayAndTime result = null;
            var utcRightSide = rightSide.ConvertToUTC();
            var utcSelf = this.ConvertToUTC();
            if (utcSelf.DayOfWeek == utcRightSide.DayOfWeek)
            {
                var leftStartTime = utcSelf.TimeStart;
                var leftEndTime = utcSelf.TimeEnd >= utcSelf.TimeStart ? utcSelf.TimeEnd : utcSelf.TimeEnd + NodaConstants.TicksPerStandardDay;

                var rightStartTime = utcRightSide.TimeStart;
                var rightEndTime = utcRightSide.TimeEnd >= utcRightSide.TimeStart ? utcRightSide.TimeEnd : utcRightSide.TimeEnd + NodaConstants.TicksPerStandardDay;

                if ((rightEndTime > leftStartTime && rightStartTime < leftEndTime))//|| (leftEndTime > rightStartTime && leftStartTime < rightEndTime)
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

                    var timeEnd = endTime < NodaConstants.TicksPerStandardDay ? endTime : endTime - NodaConstants.TicksPerStandardDay;

                    result = new DayAndTime(DayOfWeek, startTime, timeEnd, UTC_TIMEZONE_ID);

                }
            }
            return result;
        }

        public int StartTimeInMilliseconds
        {
            get
            {
                var startTime = LocalTime.FromTicksSinceMidnight(TimeStart);
                return (int)(startTime.TickOfDay / NodaConstants.TicksPerMillisecond);
            }
            
        }

        public int EndTimeInMilliseconds
        {
            get
            {
                var endTime = LocalTime.FromTicksSinceMidnight(TimeEnd);
                return (int)(endTime.TickOfDay / NodaConstants.TicksPerMillisecond);
            }
        }

        public string StartTimeToString()
        {
            var startTime = LocalTime.FromTicksSinceMidnight(TimeStart);
            return startTime.ToString();
        }

        public string EndTimeToString()
        {
            var endTime = LocalTime.FromTicksSinceMidnight(TimeEnd);
            return endTime.ToString();
        }

    }
}
