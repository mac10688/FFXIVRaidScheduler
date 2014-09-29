using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaidScheduler.WebUI.Models
{
    public class PlayerChoiceModel
    {
        private IList<PlayerModel> playerModels = new List<PlayerModel>();
        public IList<PlayerModel> PlayerModels { get { return playerModels; } set { playerModels = value; } }
    }

    public class PlayerModel
    {
        public string PlayerID { get; set; }
        public string PlayerFirstName { get; set; }
        public string PlayerLastName { get; set; }
    }
}