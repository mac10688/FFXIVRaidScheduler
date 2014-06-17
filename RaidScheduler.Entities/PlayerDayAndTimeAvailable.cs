using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Entities
{
    public class PlayerDayAndTimeAvailable
    {
        public int PlayerDayAndTimeAvailableID {get; set;}
        public DayAndTime DayAndTime { get; set; }

        public int PlayerID { get; set; }
        public Player Player { get; set; }
    }
}
