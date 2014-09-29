using RaidScheduler.Domain.DomainModels.SharedValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.DomainModels.StaticPartyDomain
{
    public class StaticPartyDayAndTimeSchedule
    {
        
        public StaticPartyDayAndTimeSchedule(DayAndTime dayAndTime)
        {
            DayAndTime = dayAndTime;
        }
        
        protected StaticPartyDayAndTimeSchedule() { }
        
        public int StaticPartyDayAndTimeScheduleID { get; protected set; }
        public DayAndTime DayAndTime { get; protected set; }
    }
}
