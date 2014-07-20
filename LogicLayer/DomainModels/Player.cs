using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;


namespace RaidScheduler.Domain.DomainModels
{
    public class Player
    {
        public int PlayerId { get; protected set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TimeZone { get; set; }

        public string UserId { get; protected set; }

        public Player(string userId)
        {
            UserId = userId;
        }

        public Player(string userId, string firstName, string lastName, string timeZone)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            TimeZone = timeZone;
        }

        protected Player() { }

        private ICollection<PotentialJob> _potentialJobs;
        public virtual ICollection<PotentialJob> PotentialJobs
        {
            get
            {
                return _potentialJobs ?? (_potentialJobs = new Collection<PotentialJob>());
            }
            protected set
            {
                _potentialJobs = value;
            }
        }

        private ICollection<PlayerDayAndTimeAvailable> _daysAndTimesAvailable;
        public virtual ICollection<PlayerDayAndTimeAvailable> DaysAndTimesAvailable
        {
            get
            {
                return _daysAndTimesAvailable ?? (_daysAndTimesAvailable = new Collection<PlayerDayAndTimeAvailable>());
            }
            protected set
            {
                _daysAndTimesAvailable = value;
            }
        }

        private ICollection<RaidRequested> _raidsRequested;
        public virtual ICollection<RaidRequested> RaidsRequested
        {
            get
            {
                return _raidsRequested ?? (_raidsRequested = new Collection<RaidRequested>());
            }
            protected set
            {
                _raidsRequested = value;
            }
        }

        private ICollection<StaticMember> _staticMember;
        public virtual ICollection<StaticMember> StaticMember
        {
            get
            {
                return _staticMember ?? (_staticMember = new Collection<StaticMember>());
            }
            protected set
            {
                _staticMember = value;
            }
        }

        public void AddToRaidRequested(RaidRequested raidRequested)
        {
            RaidsRequested.Add(raidRequested);
        }

    }
}
