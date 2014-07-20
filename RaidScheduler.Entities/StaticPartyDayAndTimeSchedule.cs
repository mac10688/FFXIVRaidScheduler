using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.DTO
{
    public class StaticPartyDayAndTimeSchedule
    {
        public int StaticPartyDayAndTimeScheduleID { get; set; }
        public DayAndTime DayAndTime { get; set; }

        public int StaticPartyID { get; set; }
        public StaticParty StaticParty { get; set; }
    }
}
