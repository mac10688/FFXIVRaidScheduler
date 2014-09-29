using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaidScheduler.WebUI.Models
{
    public class PlayerCombinationsModel
    {
        
        private IList<PartyModel> parties;
        public IList<PartyModel> Parties
        {
            get
            {
                if (parties == null)
                    parties = new List<PartyModel>();
                return parties;
            }
            set
            {
                parties = value;
            }
        }
    }

    public class PartyModel
    {
        public string RaidName { get; set; }

        private IList<string> scheduledTimes;
        public IList<string> ScheduledTimes
        {
            get
            {
                if (scheduledTimes == null)
                    scheduledTimes = new List<string>();
                return scheduledTimes;
            }
            set
            {
                scheduledTimes = value;
            }
        }

        private IList<DisplayPlayerModel> partyCombination;
        public IList<DisplayPlayerModel> PartyCombination
        {
            get
            {
                if (partyCombination == null)
                    partyCombination = new List<DisplayPlayerModel>();
                return partyCombination;
            }
            set
            {
                partyCombination = value;
            }
        }
    }

    public class DisplayPlayerModel
    {
        public string PlayerFirstName { get; set; }
        public string PlayerLastName { get; set; }
        public string ChosenJob {get; set;} 
    }

}