using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaidScheduler.Domain.Queries.UserDefinedParties.DTOS
{
    public class Player
    {
        public string PlayerId { get; set; }
        public string DisplayName { get; set; }

        private List<Job> jobs;
        public List<Job> Jobs 
        { 
            get
            {
                return jobs ?? (jobs = new List<Job>());
            }
            set
            {
                jobs = value;
            }
        }

        private List<PotentialDay> availableTimes;
        public List<PotentialDay> AvailableTimes 
        {
            get
            {
                return availableTimes ?? (availableTimes = new List<PotentialDay>());
            }
            set
            {
                availableTimes = value;
            }
        }
    }

    public class Job
    {
        public int PotentialJobId { get; set; }
        public int Ilvl {get; set;}
    }

    public class PotentialDay
    {
        public string Day { get; set; }

        private List<TimeAvailable> timesAvailable;
        public List<TimeAvailable> TimesAvailable {
            get
            {
                return timesAvailable ?? (timesAvailable = new List<TimeAvailable>());
            }
            set
            {
                timesAvailable = value;
            }
        }
    }

    public class TimeAvailable
    {
        public string StartTime {get; set;}
        public string EndTime {get; set;}
    }

}