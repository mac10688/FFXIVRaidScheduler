using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RaidScheduler.Domain;
using RaidScheduler.WebUI.Models;
using RaidScheduler.Domain.DomainModels;
using RaidScheduler.Domain.Repositories;

using NodaTime;
using NodaTime.TimeZones;
using System.Configuration;
using Microsoft.AspNet.Identity;

namespace RaidScheduler.Controllers
{
    [Authorize]
    public class PartyController : Controller
    {
        private readonly IRepository<Player> playerRepository;
        private readonly IRepository<Raid> raidRepository;
        private readonly IRepository<Job> jobRepository;
        private readonly IRepository<PlayerDayAndTimeAvailable> dayAndTimeAvailableRepository;
        private readonly IRepository<StaticParty> staticPartyRepository;
        private readonly IRepository<PotentialJob> potentialJobRepository;
        private readonly UserManager<User> userManager;
        //private readonly PartyCombination partyCombination;
        

        public PartyController(
            UserManager<User> userManager,
            IRepository<Player> playerRepository,
            IRepository<Raid> raidRepository,
            IRepository<Job> jobRepository,
            IRepository<PlayerDayAndTimeAvailable> dayAndTimeAvailableRepository,
            IRepository<StaticParty> staticPartyRepository,
            IRepository<PotentialJob> potentialJobRepository
            )
        {
            this.playerRepository = playerRepository;
            this.raidRepository = raidRepository;
            this.jobRepository = jobRepository;
            this.dayAndTimeAvailableRepository = dayAndTimeAvailableRepository;
            this.userManager = userManager;
            this.staticPartyRepository = staticPartyRepository;
            this.potentialJobRepository = potentialJobRepository;
        }

        /// <summary>
        /// This will get the View to choose the player to inspect.
        /// </summary>
        /// <returns></returns>
        public ActionResult PlayerChoice()
        {
            
            PlayerChoiceModel modelCollection = new PlayerChoiceModel();
            try
            {
                var playerCollection = playerRepository.Get();
                foreach (var player in playerCollection)
                {
                    PlayerModel playerModel = new PlayerModel();
                    //playerModel.PlayerID = player.PlayerID;
                    playerModel.PlayerFirstName = player.FirstName;
                    playerModel.PlayerLastName = player.LastName;
                    modelCollection.PlayerModels.Add(playerModel);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View(modelCollection);
        }

        /// <summary>
        /// Returns view for computed party results
        /// </summary>
        /// <returns></returns>
        public ActionResult PlayerCombinations()
        {
            PlayerCombinationsModel model = new PlayerCombinationsModel();

            var userID = User.Identity.GetUserId();
            var user = userManager.FindById(userID);
            var player = playerRepository.Get(p => p.UserId == user.Id).SingleOrDefault();
            if(player != null)
            { 
                var timezoneString = player.TimeZone;
                var timezone = NodaTime.DateTimeZoneProviders.Bcl.GetZoneOrNull(timezoneString);
                var offset = timezone.GetUtcOffset(SystemClock.Instance.Now);
                var staticParties = staticPartyRepository.Get();
                foreach (var party in staticParties)
                {
                    var partyModel = new PartyModel
                        {
                            PartyCombination = party.StaticMembers.Select(p => new DisplayPlayerModel
                            {
                                PlayerFirstName = p.Player.FirstName,
                                PlayerLastName = p.Player.LastName,
                                ChosenJob = potentialJobRepository.Find(p.ChosenPotentialJobId).Job.JobName
                            }).ToList(),
                            RaidName = raidRepository.Find(party.RaidId).RaidName
                        };


                    foreach (var schedule in party.ScheduledTimes)
                    {
                        var startTime = LocalTime.FromTicksSinceMidnight(schedule.DayAndTime.TimeStart).PlusTicks(offset.Ticks);
                        var endTime = LocalTime.FromTicksSinceMidnight(schedule.DayAndTime.TimeEnd).PlusTicks(offset.Ticks);

                        var result = schedule.DayAndTime.DayOfWeek + " " + startTime + " " + endTime;
                        partyModel.ScheduledTimes.Add(result);
                    }             
                
                    model.Parties.Add(partyModel);

                }
            }

            return View(model);
        } 

        /// <summary>
        /// Returns a view for creating a static party
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateParty()
        {
            return View();
        }

    }
}