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
using RaidScheduler.Domain.DomainModels.RaidDomain;
using RaidScheduler.Domain.DomainModels.JobDomain;
using RaidScheduler.Domain.DomainModels.UserDomain;
using RaidScheduler.Domain.DomainModels.PlayerDomain;
using RaidScheduler.Domain.DomainModels.StaticPartyDomain;

namespace RaidScheduler.Controllers
{
    [Authorize]
    public class PartyController : Controller
    {
        private readonly IRepository<Player> _playerRepository;
        private readonly IRaidFactory _raidFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IRepository<StaticParty> _staticPartyRepository;
        private readonly UserManager<User> _userManager;
        //private readonly PartyCombination partyCombination;
        

        public PartyController(
            UserManager<User> userManager,
            IRepository<Player> playerRepository,
            IRaidFactory raidFactory,
            IJobFactory jobFactory,
            IRepository<StaticParty> staticPartyRepository
            )
        {
            this._playerRepository = playerRepository;
            this._raidFactory = raidFactory;
            this._jobFactory = jobFactory;
            this._userManager = userManager;
            this._staticPartyRepository = staticPartyRepository;
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
                var playerCollection = _playerRepository.Get();
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
            var user = _userManager.FindById(userID);
            var player = _playerRepository.Get(p => p.UserId == user.Id).SingleOrDefault();
            if(player != null)
            { 
                var timezoneString = player.TimeZone;
                var timezone = NodaTime.DateTimeZoneProviders.Bcl.GetZoneOrNull(timezoneString);
                var offset = timezone.GetUtcOffset(SystemClock.Instance.Now);
                var staticParties = _staticPartyRepository.Get();
                foreach (var party in staticParties)
                {
                    var partyModel = new PartyModel
                        {
                            PartyCombination = party.StaticMembers.Select(p => new DisplayPlayerModel
                            {
                                PlayerFirstName = _playerRepository.Find(p.PlayerId).FirstName,
                                PlayerLastName = _playerRepository.Find(p.PlayerId).LastName,
                                ChosenJob = _jobFactory.CreateJob(p.ChosenJob).JobName
                            }).ToList(),
                            RaidName = _raidFactory.CreateRaid(party.RaidType).RaidName
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