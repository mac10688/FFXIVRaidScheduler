using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;


namespace RaidScheduler.DTO
{
    public class Player
    {
        public int PlayerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TimeZone { get; set; }


        public string UserId { get; set; }
        public User User { get; set; }

        private ICollection<PotentialJob> _potentialJobs;
        public virtual ICollection<PotentialJob> PotentialJobs
        {
            get
            {
                return _potentialJobs ?? (_potentialJobs = new Collection<PotentialJob>());
            }
            set
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
            set
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
            set
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
            set
            {
                _staticMember = value;
            }
        }

    }
}
