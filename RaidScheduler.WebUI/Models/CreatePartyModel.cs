using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaidScheduler.WebUI.Models
{
    public class CreatePartyModel
    {
        private IList<string> availableServers = new List<string>();
        public IList<string> AvailableServers { get { return availableServers; } set { availableServers = value; } }
    }
}