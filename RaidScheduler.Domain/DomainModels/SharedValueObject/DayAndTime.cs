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
        
        public DayAndTime(IsoDayOfWeek dayOfWeek, long timeStart, long timeEnd)
        {
            DayOfWeek = dayOfWeek;

            TimeStart = timeStart;
            
            TimeEnd = timeEnd;
        }

        public DayAndTime(IsoDayOfWeek dayOfWeek, LocalDateTime timeStart, LocalDateTime timeEnd)
        {
            DayOfWeek = dayOfWeek;
            TimeStart = timeStart.TickOfDay;
            TimeEnd = timeEnd.TickOfDay;
        }

        protected DayAndTime() { }

        public IsoDayOfWeek DayOfWeek { get; protected set; }
        public long TimeStart { get; protected set; }
        public long TimeEnd { get; protected set; }
    }
}
