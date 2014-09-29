using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaidScheduler.Domain.DomainModels.JobDomain;
using RaidScheduler.Domain.DomainModels.RaidDomain;

using MoreLinq;
using RaidScheduler.Domain.DomainModels.SharedValueObject;

namespace RaidScheduler.Domain.DomainModels.StaticPartyDomain
{
    public class StaticParty
    {

        public StaticParty(RaidType raidType, ICollection<StaticPartyDayAndTimeSchedule> scheduledTimes, ICollection<StaticMember> staticMembers)
        {
            StaticPartyId = Guid.NewGuid().ToString();
            RaidType = raidType;

            ScheduledTimes = scheduledTimes;
            StaticMembers = staticMembers;

        }

        public StaticParty(RaidType raidType, ICollection<DayAndTime> raidTimes, ICollection<StaticMember> staticMembers)
        {
            StaticPartyId = Guid.NewGuid().ToString();
            RaidType = raidType;

            ScheduledTimes = raidTimes.Select(t => new StaticPartyDayAndTimeSchedule(t)).ToList();
            StaticMembers = staticMembers;

        }

        protected StaticParty() { }
        
        public string StaticPartyId { get; protected set; }

        public RaidType RaidType { get; protected set; }

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
