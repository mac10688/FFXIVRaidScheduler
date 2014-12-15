using RaidScheduler.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.SqlClient;
using RaidScheduler.Domain.Queries.UserDefinedParties.Interfaces;
using RaidScheduler.Domain.Queries.UserDefinedParties.DTOS;
using RaidScheduler.Domain.DomainModels.JobDomain;

namespace RaidScheduler.Domain.Queries.UserDefinedParties
{
    public class PlayerSearch : IPlayerSearch
    {
        private readonly RaidSchedulerContext _context;
        public PlayerSearch(RaidSchedulerContext context)
        {
            _context = context;
        }

        public IEnumerable<PlayerSearchDTO> SearchPlayers(string server, string searchString)
        {
            var firstnameOrLastName = searchString;
            var result = _context.Player.Where(p =>( p.FirstName.Contains(searchString) || p.LastName.Contains(searchString)) && p.Server == server)
                .Select(p => new PlayerSearchDTO
                {
                    Id = p.PlayerId,
                    Name = p.FirstName + " " + p.LastName
                });
            return result;
        }


        public Player GetPlayer(string Id, string userId)
        {
            var timezoneToConvertTo = _context.Users.Find(userId).PreferredTimezone;
            var player = _context.Player.Where(p => p.PlayerId == Id).Single();
            var result = new Player();
            result.PlayerId = player.PlayerId;
            result.DisplayName = player.FirstName + " " + player.LastName;

            foreach(var jobType in Enum.GetValues(typeof(JobTypes)).Cast<JobTypes>())
            {
                var potentialJob = player.PotentialJobs.Where(pj => pj.JobId == jobType).Select(j => new DTOS.Job
                    {
                        Ilvl = j.ILvl,
                        PotentialJobId = j.PotentialJobId
                    }).SingleOrDefault();

                result.Jobs.Add(potentialJob);
            }

            foreach(var day in Enum.GetValues(typeof(NodaTime.IsoDayOfWeek))
                                .Cast<NodaTime.IsoDayOfWeek>()
                                .Where(d => d != NodaTime.IsoDayOfWeek.None))
            {
                var potentialTimesForDay = player.DaysAndTimesAvailable.Where(dt => dt.DayAndTime.ConvertToTimezone(timezoneToConvertTo).DayOfWeek == day)
                    .Select(d => new DTOS.TimeAvailable
                    {
                        StartTime = d.DayAndTime.ConvertToTimezone(timezoneToConvertTo).StartTimeToString(),
                        EndTime = d.DayAndTime.ConvertToTimezone(timezoneToConvertTo).EndTimeToString()
                    }).ToList();

                var dayToAdd = new DTOS.PotentialDay
                {
                    Day = day.ToString(),
                    TimesAvailable = potentialTimesForDay
                };
                result.AvailableTimes.Add(dayToAdd);
            }

            return result;
        }
    }
}
