using Microsoft.AspNet.Identity;
using NodaTime;
using NodaTime.TimeZones;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

using RaidScheduler.Data;
using RaidScheduler.Entities;
using RaidScheduler.Models;
using RaidScheduler.Domain;

namespace RaidScheduler.Controllers
{
    public class ProfileController : Controller
    {
        private UserManager<User> userManager;
        private readonly IRepository<Player> playerRepository;
        private readonly IRepository<Raid> raidRepository;
        private readonly IRepository<Job> jobRepository;
        private readonly IRepository<RaidRequested> raidRequestedRepository;
        private readonly IRepository<PotentialJob> potentialJobRepository;
        private readonly IRepository<PlayerDayAndTimeAvailable> playerDayAndTimeAvailableRepository;
        private readonly IPartyDomain partyCombination;
        private readonly IRepository<StaticParty> staticPartyRepository;

        public ProfileController(
            UserManager<User> userManager,
            IRepository<Player> playerRepository,
            IRepository<Raid> raidRepository,
            IRepository<Job> jobRepository,
            IRepository<RaidRequested> raidRequestedRepository,
            IRepository<PotentialJob> potentialJobRepository,
            IRepository<PlayerDayAndTimeAvailable> playerDayAndTimeAvailableRepository,
            IRepository<StaticParty> staticPartyRepository,
            IPartyDomain partyCombination
            )
        {
            this.userManager = userManager;
            this.playerRepository = playerRepository;
            this.raidRepository = raidRepository;
            this.jobRepository = jobRepository;
            this.raidRequestedRepository = raidRequestedRepository;
            this.potentialJobRepository = potentialJobRepository;
            this.playerDayAndTimeAvailableRepository = playerDayAndTimeAvailableRepository;
            this.partyCombination = partyCombination;
            this.staticPartyRepository = staticPartyRepository;
        }

        public ActionResult PlayerEdit()
        {
            PlayerPreferencesModel model = new PlayerPreferencesModel();

            var user = userManager.FindById(User.Identity.GetUserId());
            var player = playerRepository.Get((p) => p.User.Id == user.Id).SingleOrDefault();
            BclDateTimeZoneSource timezoneSource = new BclDateTimeZoneSource();
            model.TimeZoneList = timezoneSource.GetIds().OrderBy(tz => tz).ToList();
            if(player != null)
            { 
                model.FirstName = player.FirstName;
                model.LastName = player.LastName;
                model.SelectedTimeZone = player.TimeZone;

                model.RaidsRequested = player.RaidsRequested.Select(rr => rr.Raid.RaidName).ToList();

                if (player.TimeZone != null)
                {
                    var timezone = DateTimeZoneProviders.Bcl.GetZoneOrNull(player.TimeZone);
                    var offset = timezone.GetUtcOffset(SystemClock.Instance.Now);

                    model.DaysAndTimesAvailable = player.DaysAndTimesAvailable.Select(rr =>
                        new DayAndTimeAvailableModel
                        {
                            Day = rr.DayAndTime.DayOfWeek.ToString(),
                            TimeAvailableStart = (rr.DayAndTime.TimeStart / NodaConstants.TicksPerMillisecond),
                            TimeAvailableEnd = (rr.DayAndTime.TimeEnd / NodaConstants.TicksPerMillisecond),
                            TimeDurationLimit = rr.DayAndTime.TimeDurationLimit,
                            IsTentative = rr.DayAndTime.IsTentative
                        }).ToList();

                    model.PlayerPotentialJobs = player.PotentialJobs.Select(pj =>
                        new PlayerPotentialJobModel
                        {
                            PotentialJobID = pj.Job.JobID,
                            ILvl = pj.ILvl,
                            ComfortLevel = pj.ComfortLevel
                        }).ToList();
                }
            }            

            model.PotentialJobsToChoose = jobRepository.Get().Select(j =>
                new JobModel
                {
                    JobID = j.JobID,
                    JobName = j.JobName
                }).ToList();

            model.RaidsAvailable = raidRepository.Get().Select(r => r.RaidName).ToList();
            model.DaysToChoose = Enum.GetNames(typeof(IsoDayOfWeek)).Where(n => n != "None").ToList();

            return View(model);
        }

        public JsonResult SavePlayer(PlayerPreferencesModel playerPreferences)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Message = "fail" });
                }

                var currentUserId = User.Identity.GetUserId();
                var playerUser = userManager.FindById(currentUserId);
                var player = playerRepository.Get(p => p.User.Id == playerUser.Id).SingleOrDefault();
                if(player == null)
                {
                    player = new Player();
                    player.User = playerUser;
                }

                player.FirstName = playerPreferences.FirstName;
                player.LastName = playerPreferences.LastName;
                player.TimeZone = playerPreferences.SelectedTimeZone;

                var raidsRequested = player.RaidsRequested.ToList();

                player.RaidsRequested.ToList().ForEach(r =>
                {
                    player.RaidsRequested.Remove(r);
                    raidRequestedRepository.Delete(r);
                    
                });

                playerPreferences.RaidsRequested.Select(rr =>
                    new RaidRequested
                    {
                        PlayerID = player.PlayerID,
                        Player = player,
                        Raid = raidRepository.Get(r => r.RaidName == rr).Single(),
                        RaidID = raidRepository.Get(r => r.RaidName == rr).Single().RaidID,
                        FoundRaid = false
                    })
                    .ToList()
                    .ForEach(r =>
                    {
                        player.RaidsRequested.Add(r);
                    });

                player.PotentialJobs.ToList().ForEach(p =>
                    {
                        player.PotentialJobs.Remove(p);
                        potentialJobRepository.Delete(p);
                    });

                playerPreferences.PlayerPotentialJobs.Select(ppj =>
                    new PotentialJob
                    {
                        ComfortLevel = ppj.ComfortLevel,
                        ILvl = ppj.ILvl,
                        Job = jobRepository.Find(ppj.PotentialJobID)
                    })
                    .ToList()
                    .ForEach(p =>
                    {
                        player.PotentialJobs.Add(p);

                    });

                player.DaysAndTimesAvailable.ToList().ForEach(d =>
                    {
                        player.DaysAndTimesAvailable.Remove(d);
                        playerDayAndTimeAvailableRepository.Delete(d);
                    });
                foreach (var d in playerPreferences.DaysAndTimesAvailable)
                {
                    var dayAndTime = new DayAndTime
                    {
                        DayOfWeek = (IsoDayOfWeek)Enum.Parse(typeof(IsoDayOfWeek), d.Day, true),
                        IsTentative = d.IsTentative,
                        TimeDurationLimit = d.TimeDurationLimit
                    };

                    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);

                    LocalDateTime timeAvailableStart = LocalDateTime.FromDateTime(epoch.AddMilliseconds(d.TimeAvailableStart));
                    dayAndTime.TimeStart = timeAvailableStart.TickOfDay;


                    LocalDateTime timeAvailableEnd = LocalDateTime.FromDateTime(epoch.AddMilliseconds(d.TimeAvailableEnd));
                    dayAndTime.TimeEnd = timeAvailableEnd.TickOfDay;

                    var availableDayaAndTime = new PlayerDayAndTimeAvailable
                    {
                        DayAndTime = dayAndTime
                    };

                    player.DaysAndTimesAvailable.Add(availableDayaAndTime);
                }

                playerRepository.Save(player);

                //Thread thread = new Thread(() =>
                //{
                var oldParty = staticPartyRepository.Get();
                oldParty.ToList().ForEach(p =>
                {
                    staticPartyRepository.Delete(p);
                });

                var players = playerRepository.Get().ToList();

                var staticParties = partyCombination.CreateStaticPartiesFromPlayers(players);
                    
                foreach (var party in staticParties)
                {
                    staticPartyRepository.Save(party);
                }
                //});
                //thread.IsBackground = true;
                //thread.Start();

                return Json(new { Message = "success", PlayerID = player.PlayerID });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "fail" });
            }
        }
	}
}