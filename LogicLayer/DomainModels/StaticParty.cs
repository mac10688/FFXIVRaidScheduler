using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.DomainModels
{
    public class StaticParty
    {

        public StaticParty(Raid raid, ICollection<StaticPartyDayAndTimeSchedule> scheduledTimes, ICollection<StaticMember> staticMembers)
        {
            Raid = raid;
            RaidId = raid.RaidId;

            ScheduledTimes = scheduledTimes;
            StaticMembers = staticMembers;

        }

        protected StaticParty() { }
        
        public int StaticPartyId { get; protected set; }

        public int RaidId { get; protected set; }
        public Raid Raid { get; protected set; }

        private ICollection<StaticPartyDayAndTimeSchedule> _scheduledTimes;
        public virtual ICollection<StaticPartyDayAndTimeSchedule> ScheduledTimes
        {
            get
            {
                return _scheduledTimes ?? (_scheduledTimes = new Collection<StaticPartyDayAndTimeSchedule>());
            }

            protected set
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
            protected set
            {
                _staticMembers = value;
            }
        }

        public void SetStaticMembers(ICollection<StaticMember> staticMembers)
        {
            StaticMembers = staticMembers;
        }

        public void SetScheduledTimes(ICollection<StaticPartyDayAndTimeSchedule> scheduledTimes)
        {
            ScheduledTimes = scheduledTimes;
        }

    }
}
