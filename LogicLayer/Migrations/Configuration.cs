namespace RaidScheduler.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using RaidScheduler.Domain.DomainModels;
    using RaidScheduler.Domain.Helper;
    using System.Collections.Generic;
    using RaidScheduler.Domain.Data;

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

            var jobFactory = new JobFactory();

            context.Jobs.AddOrUpdate(j => j.JobName,
                    jobFactory.CreateJob(JobType.Paladin),
                    jobFactory.CreateJob(JobType.Warrior),
                    jobFactory.CreateJob(JobType.WhiteMage),
                    jobFactory.CreateJob(JobType.Scholar),
                    jobFactory.CreateJob(JobType.Summoner),
                    jobFactory.CreateJob(JobType.Dragoon),
                    jobFactory.CreateJob(JobType.Monk),
                    jobFactory.CreateJob(JobType.BlackMage)
                );

            var raidFactory = new RaidFactory();

            context.Raids.AddOrUpdate(r => r.RaidName,
                raidFactory.CreateRaid(RaidType.CoilTurn1),
                raidFactory.CreateRaid(RaidType.CoilTurn2),
                raidFactory.CreateRaid(RaidType.CoilTurn3),
                raidFactory.CreateRaid(RaidType.CoilTurn4),
                raidFactory.CreateRaid(RaidType.CoilTurn5),
                raidFactory.CreateRaid(RaidType.GarudaExtreme),
                raidFactory.CreateRaid(RaidType.TitanExtreme),
                raidFactory.CreateRaid(RaidType.IfritExtreme)
                );

        }
    }
}
