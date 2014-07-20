using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;

namespace RaidScheduler.Domain.DomainModels
{
    public class DayAndTime
    {
        
        public DayAndTime(IsoDayOfWeek dayOfWeek, long timeStart, long timeEnd)
        {
            DayOfWeek = DayOfWeek;
            TimeStart = timeStart;
            TimeEnd = timeEnd;
        }

        protected DayAndTime() { }

        public IsoDayOfWeek DayOfWeek { get; protected set; }
        public long TimeStart { get; protected set; }
        public long TimeEnd { get; protected set; }
        public long TimeDurationLimit { get; protected set; }
        public bool IsTentative { get; protected set; }
    }
}
