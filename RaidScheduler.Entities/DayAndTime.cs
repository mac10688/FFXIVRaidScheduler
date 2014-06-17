using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime;

namespace RaidScheduler.Entities
{
    public class DayAndTime
    {
        //public int DayAndTimeID { get; set; }
        public IsoDayOfWeek DayOfWeek { get; set; }        
        public long TimeStart { get; set; }        
        public long TimeEnd { get; set; }
        public long TimeDurationLimit { get; set; }
        public bool IsTentative { get; set; }
    }
}
