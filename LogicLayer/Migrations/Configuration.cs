namespace RaidScheduler.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using RaidScheduler.Domain.DomainModels;
    using System.Collections.Generic;
    using RaidScheduler.Domain.Data;
    using RaidScheduler.Domain.DomainModels.JobDomain;
    using RaidScheduler.Domain.DomainModels.RaidDomain;

    internal sealed class Configuration : DbMigrationsConfiguration<RaidSchedulerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "RaidScheduler.Data.RaidSchedulerContext";
        }

        protected override void Seed(RaidSchedulerContext context)
        {
            //  This method will be called after migrating to the latest version.

            //You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //to avoid creating duplicate seed data. E.g.

            //context.People.AddOrUpdate(
            //  p => p.FullName,
            //  new Person { FullName = "Andrew Peters" },
            //  new Person { FullName = "Brice Lambson" },
            //  new Person { FullName = "Rowan Miller" }
            //);

            //var jobFactory = new JobFactory();

            //context.Jobs.AddOrUpdate(j => j.JobName,
            //        jobFactory.CreateJob(JobTypes.Paladin),
            //        jobFactory.CreateJob(JobTypes.Warrior),
            //        jobFactory.CreateJob(JobTypes.WhiteMage),
            //        jobFactory.CreateJob(JobTypes.Scholar),
            //        jobFactory.CreateJob(JobTypes.Summoner),
            //        jobFactory.CreateJob(JobTypes.Dragoon),
            //        jobFactory.CreateJob(JobTypes.Monk),
            //        jobFactory.CreateJob(JobTypes.BlackMage)
            //    );

            //var raidFactory = new RaidFactory();

            //context.Raids.AddOrUpdate<Raid>(r => r.RaidName,
            //    raidFactory.CreateRaid(RaidType.CoilTurn1),
            //    raidFactory.CreateRaid(RaidType.CoilTurn2),
            //    raidFactory.CreateRaid(RaidType.CoilTurn3),
            //    raidFactory.CreateRaid(RaidType.CoilTurn4),
            //    raidFactory.CreateRaid(RaidType.CoilTurn5),
            //    raidFactory.CreateRaid(RaidType.GarudaExtreme),
            //    raidFactory.CreateRaid(RaidType.TitanExtreme),
            //    raidFactory.CreateRaid(RaidType.IfritExtreme)
            //    );

        }
    }
}
