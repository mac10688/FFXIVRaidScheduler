using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.DomainModels
{
    public class StaticPartyDayAndTimeSchedule
    {
        
        public StaticPartyDayAndTimeSchedule(DayAndTime dayAndTime, StaticParty party)
        {
            DayAndTime = dayAndTime;
            StaticParty = party;
        }
        
        protected StaticPartyDayAndTimeSchedule() { }
        
        public int StaticPartyDayAndTimeScheduleID { get; protected set; }
        public DayAndTime DayAndTime { get; protected set; }

        public int StaticPartyID { get; protected set; }
        public StaticParty StaticParty { get; protected set; }
    }
}
