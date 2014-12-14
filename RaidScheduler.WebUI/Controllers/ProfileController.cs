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
using RaidScheduler.Domain.DomainModels.RaidDomain;
using RaidScheduler.Domain.DomainModels.JobDomain;
using RaidScheduler.Domain.DomainModels.UserDomain;
using RaidScheduler.Domain.DomainModels.PlayerDomain;
using RaidScheduler.Domain.DomainModels.StaticPartyDomain;
using RaidScheduler.Domain.DomainModels.SharedValueObject;
using RaidScheduler.Domain.Repositories.Interfaces;

namespace RaidScheduler.Controllers
{
    public class ProfileController : Controller
    {
        private UserManager<User> _userManager;
        private readonly IRepository<Player> _playerRepository;
        private readonly IRepository<StaticParty> _staticPartyRepository;
        private readonly IJobFactory _jobFactory;
        private readonly IRaidFactory _raidFactory;        
        private readonly IPartyService _partyCombination;
        

        public ProfileController(
            UserManager<User> userManager,
            IRepository<Player> playerRepository,
            IRaidFactory raidFactory,
            IJobFactory jobFactory,
            IRepository<StaticParty> staticPartyRepository,
            IPartyService partyCombination
            )
        {
            _userManager = userManager;
            _playerRepository = playerRepository;
            _raidFactory = raidFactory;
            _jobFactory = jobFactory;
            _partyCombination = partyCombination;
            _staticPartyRepository = staticPartyRepository;
        }

        /// <summary>
        /// Returns the view for a player to edit their profile.
        /// </summary>
        /// <returns></returns>
        public ActionResult PlayerEdit()
        {
            PlayerPreferencesModel model = new PlayerPreferencesModel();

            var user = _userManager.FindById(User.Identity.GetUserId());
            var player = _playerRepository.Get((p) => p.UserId == user.Id).SingleOrDefault();
            var timezoneSource = new BclDateTimeZoneSource();
            model.TimeZoneList = timezoneSource.GetIds().OrderBy(tz => tz).ToList();
            if(player != null)
            { 
                model.FirstName = player.FirstName;
                model.LastName = player.LastName;
                model.SelectedServer = player.Server;

                model.RaidsRequested = player.RaidsRequested.Select(rr => _raidFactory.CreateRaid(rr.RaidType).RaidName).ToList();

                var timezone = DateTimeZoneProviders.Bcl.GetZoneOrNull(user.PreferredTimezone);
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
                        PlayerPotentialJobID = pj.PotentialJobId,
                        PotentialJobID = (int)pj.JobId,
                        ILvl = pj.ILvl
                    }).ToList();
            }            

            model.PotentialJobsToChoose = _jobFactory.GetAllJobs().Select(j =>
                new JobModel
                {
                    JobID = j.JobId,
                    JobName = j.JobName
                }).ToList();

            model.RaidsAvailable = _raidFactory.GetAllRaids().Select(r => r.RaidName).ToList();
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
                var playerUser = _userManager.FindById(currentUserId);
                var player = _playerRepository.Get(p => p.UserId == playerUser.Id).SingleOrDefault();
                if(player == null)
                {
                    player = new Player(playerUser.Id, playerPreferences.FirstName, playerPreferences.LastName, playerPreferences.SelectedServer);
                }
                else
                {
                    player.FirstName = playerPreferences.FirstName;
                    player.LastName = playerPreferences.LastName;
                    player.Server = playerPreferences.SelectedServer;
                }                

                //var raidsRequested = player.RaidsRequested.ToList();

                //player.RaidsRequested.ToList().ForEach(r =>
                //{
                //    player.RaidsRequested.Remove(r);
                //    _raidRequestedRepository.Delete(r);                    
                //});

                var allRaidsPossible = _raidFactory.GetAllRaids().ToList();
                var raidsRequested = new List<RaidRequested>();
                foreach(var modelRaidRequested in playerPreferences.RaidsRequested)
                {
                    var raidType = allRaidsPossible.Where(r => modelRaidRequested == r.RaidName).Single().RaidType;
                    var raidRequested = new RaidRequested(raidType, false);
                    raidsRequested.Add(raidRequested);
                }
                player.SetRaidsRequested(raidsRequested);

                var allJobsPossible = _jobFactory.GetAllJobs();
                var allPotentialJobs = new List<PotentialJob>();
                foreach(var modelPotentialJob in playerPreferences.PlayerPotentialJobs)
                {
                    var jobType = allJobsPossible.Where(j => modelPotentialJob.PotentialJobID == j.JobId).Single().JobType;
                    var potentialJob = new PotentialJob(modelPotentialJob.ILvl, jobType);
                    allPotentialJobs.Add(potentialJob);
                }

                player.SetPotentialJobs(allPotentialJobs);

                var timeAvailable = new List<PlayerDayAndTimeAvailable>();
                foreach (var d in playerPreferences.DaysAndTimesAvailable)
                {
                    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);

                    LocalDateTime timeAvailableStart = LocalDateTime.FromDateTime(epoch.AddMilliseconds(d.TimeAvailableStart));
                    var timeStart = timeAvailableStart.TickOfDay;


                    LocalDateTime timeAvailableEnd = LocalDateTime.FromDateTime(epoch.AddMilliseconds(d.TimeAvailableEnd));
                    var timeEnd = timeAvailableEnd.TickOfDay;

                    var day = (IsoDayOfWeek)Enum.Parse(typeof(IsoDayOfWeek), d.Day, true);
                    var dayAndTime = new DayAndTime(day, timeStart, timeEnd, playerUser.PreferredTimezone);

                    var availableDayaAndTime = new PlayerDayAndTimeAvailable(dayAndTime);

                    timeAvailable.Add(availableDayaAndTime);
                }
                player.SetDaysAndTimesAvailable(timeAvailable);

                _playerRepository.Save(player);

                var oldParty = _staticPartyRepository.Get();
                oldParty.ToList().ForEach(p =>
                {
                    _staticPartyRepository.Delete(p);
                });

                var players = _playerRepository.Get().ToList();

                var staticParties = _partyCombination.CreateStaticPartiesFromPlayers(players);
                    
                foreach (var party in staticParties)
                {
                    _staticPartyRepository.Save(party);
                }

                return Json(new { Message = "success", PlayerID = player.PlayerId });
            }
            catch (Exception)
            {
                return Json(new { Message = "fail" });
            }
        }
	}
}