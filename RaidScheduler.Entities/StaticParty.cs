using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.DTO
{
    public class StaticParty
    {
        public int StaticPartyID { get; set; }

        public int RaidID { get; set; }
        public Raid Raid {get; set;}

        private ICollection<StaticPartyDayAndTimeSchedule> _scheduledTimes;
        public virtual ICollection<StaticPartyDayAndTimeSchedule> ScheduledTimes
        {
            get
            {
                return _scheduledTimes ?? (_scheduledTimes = new Collection<StaticPartyDayAndTimeSchedule>());
            }

            set
            {
                _scheduledTimes = value;
            }
        }

        private ICollection<StaticMember> _staticMembers;
        public virtual ICollection<StaticMember> StaticMembers 
        {
            get
            {
                return _staticMembers ?? (_staticMembers = new Collection<StaticMember>());
            }
            set
            {
                _staticMembers = value;
            }
        }
    }
}
