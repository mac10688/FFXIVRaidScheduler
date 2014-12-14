using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NodaTime;
using System.ComponentModel.DataAnnotations;

namespace RaidScheduler.WebUI.Models
{
    public class PlayerPreferencesModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string SelectedServer { get; set; }

        private IList<string> availableServers = new List<string>()
        {
            "Aegis",
            "Atomos",
            "Carbuncle",
            "Garuda",
            "Gungnir",
            "Kujata",
            "Ramuh",
            "Tonberry",
            "Typhon",
            "Unicorn",
            "Alexander",
            "Bahamut",
            "Durandel",
            "Fenrir",
            "Ifrit",
            "Ridill",
            "Tiamat",
            "Ultima",
            "Valefor",
            "Yojimbo",
            "Zeromus",
            "Anima",
            "Asura",
            "Belias",
            "Chocobo",
            "Hades",
            "Ixion",
            "Mandragora",
            "Masamune",
            "Pandaemonium",
            "Shinryu",
            "Titan",
            "Adamantoise",
            "Balmug",
            "Cactuar",
            "Coeurl",
            "Faerie",
            "Gilgamesh",
            "Goblin",
            "Jenova",
            "Mateus",
            "Midgardsormr",
            "Sargatanas",
            "Siren",
            "Zalera",
            "Behemoth",
            "Brynhildr",
            "Diabolos",
            "Excalibur",
            "Exodus",
            "Famfrit",
            "Hyperion",
            "Lamia",
            "Leviathan",
            "Malboro",
            "Ultros",
            "Cerberus",
            "Lich",
            "Moogle",
            "Odin",
            "Phoenix",
            "Ragnarok",
            "Shiva",
            "Zodiark"
        };
        public IList<string> AvailableServers { get { return availableServers; } set { availableServers = value; } }

        private IList<string> timeZoneList = new List<string>();
        public IList<string> TimeZoneList { get; set; }

        private IList<PlayerPotentialJobModel> playerPotentialJobs = new List<PlayerPotentialJobModel>();
        public IList<PlayerPotentialJobModel> PlayerPotentialJobs { get { return playerPotentialJobs; } set { playerPotentialJobs = value; } }

        private IList<JobModel> potentialJobsToChoose = new List<JobModel>();
        public IList<JobModel> PotentialJobsToChoose { get { return potentialJobsToChoose; } set { potentialJobsToChoose = value; } }

        private IList<DayAndTimeAvailableModel> daysAndTimesAvailable = new List<DayAndTimeAvailableModel>();
        public IList<DayAndTimeAvailableModel> DaysAndTimesAvailable { get { return daysAndTimesAvailable; } set { daysAndTimesAvailable = value; } }

        private IList<string> raidsRequested = new List<string>();
        public IList<string> RaidsRequested { get { return raidsRequested; } set { raidsRequested = value; } }

        private IList<string> raidsAvailable = new List<string>();
        public IList<string> RaidsAvailable { get { return raidsAvailable; } set { raidsAvailable = value; } }

        private IList<string> daysToChoose = new List<string>();
        public IList<string> DaysToChoose { get { return daysToChoose; } set { daysToChoose = value; } }

    }

    public class PlayerPotentialJobModel
    {
        public int PlayerPotentialJobID { get; set; }
        [Required]
        public int PotentialJobID { get; set; }
        [Required]
        public int ILvl { get; set; }

    }

    public class PlayerRaidRequestedModel
    {
        public int RaidID { get; set; }
    }

    public class DayAndTimeAvailableModel
    {
        public int DayAndTimeAvailableModelID { get; set; }
        [Required]
        public string Day { get; set; }
        [Required]
        public long TimeAvailableStart { get; set; }
        [Required]
        public long TimeAvailableEnd { get; set; }
    }

    public class RaidModel
    {
        public int RaidID { get; set; }
        public string RaidName { get; set; }
    }

    public class JobModel
    {
        public int JobID { get; set; }
        public string JobName { get; set; }
    }

}