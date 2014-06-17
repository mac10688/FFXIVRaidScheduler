namespace RaidScheduler.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using RaidScheduler.Entities;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<RaidScheduler.Data.RaidSchedulerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "RaidScheduler.Data.RaidSchedulerContext";
        }

        protected override void Seed(RaidScheduler.Data.RaidSchedulerContext context)
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
            context.Jobs.AddOrUpdate(j => j.JobName,
                    new Job
                    {
                        JobID = 1,
                        JobName = "Paladin",
                        IsDps = false,
                        IsMeleeDps = false,
                        IsRangedDps = false,
                        IsMagicalDps = false,
                        IsPhysicalDps = false,
                        IsTank = true,
                        IsHealer = false,
                        CanSilence = true,
                        CanStun = true

                    },
                    new Job
                    {
                        JobID = 2,
                        JobName = "Warrior",
                        IsDps = false,
                        IsMeleeDps = false,
                        IsRangedDps = false,
                        IsMagicalDps = false,
                        IsPhysicalDps = false,
                        IsTank = true,
                        IsHealer = false,
                        CanSilence = false,
                        CanStun = true
                    },
                    new Job
                    {
                        JobID = 3,
                        JobName = "White Mage",
                        IsDps = false,
                        IsMeleeDps = false,
                        IsRangedDps = false,
                        IsMagicalDps = false,
                        IsPhysicalDps = false,
                        IsTank = false,
                        IsHealer = true,
                        CanSilence = false,
                        CanStun = false
                    },
                    new Job
                    {
                        JobID = 4,
                        JobName = "Scholar",
                        IsDps = false,
                        IsMeleeDps = false,
                        IsRangedDps = false,
                        IsMagicalDps = false,
                        IsPhysicalDps = false,
                        IsTank = false,
                        IsHealer = true,
                        CanSilence = false,
                        CanStun = false
                    },
                    new Job
                    {
                        JobID = 5,
                        JobName = "Summoner",
                        IsDps = true,
                        IsMeleeDps = false,
                        IsRangedDps = true,
                        IsMagicalDps = true,
                        IsPhysicalDps = false,
                        IsTank = false,
                        IsHealer = false,
                        CanSilence = false,
                        CanStun = false
                    },
                    new Job
                    {
                        JobID = 6,
                        JobName = "Dragoon",
                        IsDps = true,
                        IsMeleeDps = true,
                        IsRangedDps = false,
                        IsMagicalDps = false,
                        IsPhysicalDps = true,
                        IsTank = false,
                        IsHealer = false,
                        CanSilence = false,
                        CanStun = false
                    },
                    new Job
                    {
                        JobID = 7,
                        JobName = "Monk",
                        IsDps = true,
                        IsMeleeDps = true,
                        IsRangedDps = false,
                        IsMagicalDps = false,
                        IsPhysicalDps = true,
                        IsTank = false,
                        IsHealer = false,
                        CanSilence = true,
                        CanStun = false
                    }
                );

            context.Raids.AddOrUpdate(r => r.RaidName,
                new Raid
                {
                    RaidID = 1,
                    RaidName = "Coil: Turn 1",
                    RaidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria
                        {
                            NumberOfDps = 4,
                            NumberOfTanks = 2,
                            NumberOfHealers = 2,
                            NumberOfSilencers = 2,
                            MinILvl = 70,
                            NumberOfPlayersRequired = 8
                        }
                    }
                },
                new Raid
                {
                    RaidID = 2,
                    RaidName = "Coil: Turn 2",
                    RaidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria
                        {
                            NumberOfDps = 4,
                            NumberOfTanks = 1,
                            NumberOfHealers = 3,
                            NumberOfSilencers = 2,
                            MinILvl = 73,
                            NumberOfPlayersRequired = 8
                        }
                    }
                },
                new Raid
                {
                    RaidID = 3,
                    RaidName = "Coil: Turn 3",
                    RaidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria
                        {
                            NumberOfDps = 4,
                            NumberOfTanks = 2,
                            NumberOfHealers = 2,
                            MinILvl = 73,
                            NumberOfPlayersRequired = 8
                        }
                    }
                },
                new Raid
                {
                    RaidID = 4,
                    RaidName = "Coil: Turn 4",
                    RaidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria
                        {
                            NumberOfTanks = 2,
                            NumberOfHealers = 2,
                            NumberOfMagicalDps = 2,
                            NumberOfPhysicalDps = 2,
                            MinILvl = 77,
                            NumberOfPlayersRequired = 8
                        }
                    }
                },
                new Raid
                {
                    RaidID = 5,
                    RaidName = "Coil: Turn 5",
                    RaidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria
                        {
                            NumberOfDps = 4,
                            NumberOfTanks = 2,
                            NumberOfHealers = 2,
                            MinILvl = 82,
                            NumberOfPlayersRequired = 8
                        }
                    }
                },
                new Raid
                {
                    RaidID = 6,
                    RaidName = "Garuda Extreme",
                    RaidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria
                        {
                            NumberOfDps = 4,
                            NumberOfTanks = 2,
                            NumberOfHealers = 2,
                            MinILvl = 65,
                            NumberOfPlayersRequired = 8
                        }
                    }
                },
                new Raid
                {
                    RaidID = 7,
                    RaidName = "Titan Extreme",
                    RaidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria
                        {
                            NumberOfDps = 4,
                            NumberOfTanks = 2,
                            NumberOfHealers = 2,
                            MinILvl = 67,
                            NumberOfPlayersRequired = 8
                        }
                    }
                },
                new Raid
                {
                    RaidID = 8,
                    RaidName = "Ifrit Extreme",
                    RaidCriteria = new List<RaidCriteria>()
                    {
                        new RaidCriteria
                        {
                            NumberOfDps = 4,
                            NumberOfTanks = 2,
                            NumberOfHealers = 2,
                            MinILvl = 70,
                            NumberOfPlayersRequired = 8
                        }
                    }
                }
                );

        }
    }
}
