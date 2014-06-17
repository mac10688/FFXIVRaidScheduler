using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaidScheduler.Entities;

namespace RaidScheduler.Data
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<RaidSchedulerContext>
    {
        protected override void Seed(RaidSchedulerContext context)
        {
            Raid turn1 = new Raid();
            turn1.RaidID = 1;
            turn1.RaidName = "Coil: Turn 1";

            context.Raids.Add(turn1);

            Raid turn2 = new Raid();
            turn2.RaidID = 2;
            turn2.RaidName = "Coil: Turn 2";

            context.Raids.Add(turn2);

            Raid turn3 = new Raid();
            turn3.RaidID = 3;
            turn3.RaidName = "Coil: Turn 3";

            context.Raids.Add(turn3);

            Raid turn4 = new Raid();
            turn4.RaidID = 4;
            turn4.RaidName = "Coil: Turn 4";

            context.Raids.Add(turn4);

            Raid turn5 = new Raid();
            turn5.RaidID = 5;
            turn5.RaidName = "Coil: Turn 5";

            context.Raids.Add(turn5);

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
