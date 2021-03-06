﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using RaidScheduler.Domain.DomainModels.RaidDomain;
using RaidScheduler.Domain.DomainModels.SharedValueObject;


namespace RaidScheduler.Domain.DomainModels.PlayerDomain
{
    public class Player
    {
        public string PlayerId { get; protected set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string UserId { get; protected set; }
        public string Server { get; set; }

        public Player(string userId, string firstName, string lastName, string server)
        {
            PlayerId = Guid.NewGuid().ToString();
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Server = server;
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

        public void AddToRaidRequested(RaidType raid)
        {
            var raidRequested = new RaidRequested(raid, false);
            RaidsRequested.Add(raidRequested);
        }

        public void AddToDayAndTimeAvailable(DayAndTime dayAndTime)
        {
            var dayAndTimeAvailable = new PlayerDayAndTimeAvailable(dayAndTime);
            DaysAndTimesAvailable.Add(dayAndTimeAvailable);
        }

        public void SetRaidsRequested(ICollection<RaidRequested> raidsRequested)
        {
            foreach( var rr in raidsRequested)
            {
                var count = raidsRequested.Where(r => r.RaidType == rr.RaidType).Count();
                if(count > 1)
                {
                    throw new Exception("Cannot have duplicating raids requested.");
                }
            }

            foreach (var rr in RaidsRequested.ToList())
            {
                RaidsRequested.Remove(rr);
            }

            foreach(var rr in raidsRequested)
            {
                RaidsRequested.Add(rr);
            }
        }

        public void SetPotentialJobs(ICollection<PotentialJob> allPotentialJobs)
        {
            foreach(var pj in allPotentialJobs)
            {
                var count = allPotentialJobs.Where(j => j.JobId == pj.JobId).Count();
                if(count > 1)
                {
                    throw new Exception("Cannnot have duplicating potential jobs.");
                }
            }

            foreach (var jr in PotentialJobs.ToList())
            {
                PotentialJobs.Remove(jr);
            }

            foreach(var jr in allPotentialJobs)
            {
                PotentialJobs.Add(jr);
            }
        }

        public void SetDaysAndTimesAvailable(ICollection<PlayerDayAndTimeAvailable> timeAvailable)
        {
            foreach (var ta in DaysAndTimesAvailable.ToList())
            {
                DaysAndTimesAvailable.Remove(ta);
            }

            foreach(var ta in timeAvailable)
            {
                DaysAndTimesAvailable.Add(ta);
            }
        }

    }
}
