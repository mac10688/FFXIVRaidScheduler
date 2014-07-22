using Microsoft.AspNet.Identity;
using NodaTime;
using NodaTime.TimeZones;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;


using RaidScheduler.WebUI.Models;
using RaidScheduler.Domain.DomainModels;
using RaidScheduler.Domain.Repositories;
using RaidScheduler.Domain;
using RaidScheduler.Domain.Services;

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
        private readonly IPartyService partyCombination;
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
            IPartyService partyCombination
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

        /// <summary>
        /// Returns the view for a player to edit their profile.
        /// </summary>
        /// <returns></returns>
        public ActionResult PlayerEdit()
        {
            PlayerPreferencesModel model = new PlayerPreferencesModel();

            var user = userManager.FindById(User.Identity.GetUserId());
            var player = playerRepository.Get((p) => p.UserId == user.Id).SingleOrDefault();
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
                        }).ToList();

                    model.PlayerPotentialJobs = player.PotentialJobs.Select(pj =>
                        new PlayerPotentialJobModel
                        {
                            PotentialJobID = pj.Job.JobId,
                            ILvl = pj.ILvl,
                            ComfortLevel = pj.ComfortLevel
                        }).ToList();
                }
            }            

            model.PotentialJobsToChoose = jobRepository.Get().Select(j =>
                new JobModel
                {
                    JobID = j.JobId,
                    JobName = j.JobName
                }).ToList();

            model.RaidsAvailable = raidRepository.Get().Select(r => r.RaidName).ToList();
            model.DaysToChoose = Enum.GetNames(typeof(IsoDayOfWeek)).Where(n => n != "None").ToList();

            return View(model);
        }

        /// <summary>
        /// Given a player preference model, this will save. If it fails it will return Message: "fail".
        /// if it succeeds, it will return Message: success
        /// </summary>
        /// <param name="playerPreferences"></param>
        /// <returns></returns>
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
                var player = playerRepository.Get(p => p.UserId == playerUser.Id).SingleOrDefault();
                if(player == null)
                {
                    player = new Player(playerUser.Id);
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
                    new RaidRequested(
                        player,
                        raidRepository.Get(r => r.RaidName == rr).Single(), 
                        false))
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
                    new PotentialJob(ppj.ILvl, ppj.ComfortLevel, jobRepository.Find(ppj.PotentialJobID)))
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
                    

                    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);

                    LocalDateTime timeAvailableStart = LocalDateTime.FromDateTime(epoch.AddMilliseconds(d.TimeAvailableStart));
                    var timeStart = timeAvailableStart.TickOfDay;


                    LocalDateTime timeAvailableEnd = LocalDateTime.FromDateTime(epoch.AddMilliseconds(d.TimeAvailableEnd));
                    var timeEnd = timeAvailableEnd.TickOfDay;

                    var day = (IsoDayOfWeek)Enum.Parse(typeof(IsoDayOfWeek), d.Day, true);
                    var dayAndTime = new DayAndTime(day, timeStart, timeEnd);

                    var availableDayaAndTime = new PlayerDayAndTimeAvailable(dayAndTime);

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

                return Json(new { Message = "success", PlayerID = player.PlayerId });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "fail" });
            }
        }
	}
}