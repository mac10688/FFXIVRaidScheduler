using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.DomainModels
{
    public class PlayerDayAndTimeAvailable
    {
        
        public PlayerDayAndTimeAvailable(DayAndTime dayAndTime)
        {
            DayAndTime = dayAndTime;
        }

        protected PlayerDayAndTimeAvailable() { }

        public int PlayerDayAndTimeAvailableId {get; protected set;}
        public DayAndTime DayAndTime { get; protected set; }

        public int PlayerID { get; protected set; }
        public Player Player { get; protected set; }
    }
}
