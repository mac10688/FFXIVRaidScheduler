using RaidScheduler.Domain.DomainModels.SharedValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.DomainModels.PlayerDomain
{
    public class PlayerDayAndTimeAvailable
    {
        
        public PlayerDayAndTimeAvailable(DayAndTime dayAndTime)
        {
            DayAndTime = dayAndTime;
        }

        protected PlayerDayAndTimeAvailable() { }

        public int PlayerDayAndTimeAvailableId {get; set;}
        public DayAndTime DayAndTime { get; protected set; }

    }
}
