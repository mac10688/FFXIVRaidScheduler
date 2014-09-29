using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using RaidScheduler.Domain.DomainModels;
using RaidScheduler.Domain.DomainModels.JobDomain;
using RaidScheduler.Domain.Migrations;

using Microsoft.AspNet.Identity.EntityFramework;
using RaidScheduler.Domain.DomainModels.RaidDomain;
using RaidScheduler.Domain.DomainModels.UserDomain;
using RaidScheduler.Domain.DomainModels.PlayerDomain;
using RaidScheduler.Domain.DomainModels.StaticPartyDomain;
using RaidScheduler.Domain.DomainModels.SharedValueObject;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaidScheduler.Domain.Data
{
    public class RaidSchedulerContext: IdentityDbContext<User>
    {

        public RaidSchedulerContext()
            : base("RaidSchedulerContext", false) { }
        
        public RaidSchedulerContext(string connectionString)
            : base(connectionString)
        {
        }

        //public DbSet<DayAndTime> DayAndTime { get; set; }
        public IDbSet<PlayerDayAndTimeAvailable> PlayerDayAndTimesAvailable { get; set; }
        public IDbSet<StaticPartyDayAndTimeSchedule> StaticPartyDayAndTimesAvailable { get; set; }
        public IDbSet<PotentialJob> PotentialJobs { get; set; }
        public IDbSet<RaidRequested> RaidsRequested { get; set; }
        public IDbSet<StaticParty> StaticParties { get; set; }
        public IDbSet<StaticMember> StaticPartyMember { get; set; }
        public IDbSet<Player> Player { get; set; }
        
        /// <summary>
        /// Overrides IdentityUser from IdentityUserContext
        /// </summary>
        public override IDbSet<User> Users { get; set; }

        /// <summary>
        /// Configure the relationships of the entities.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Configuration.LazyLoadingEnabled = true;

            modelBuilder.Entity<Player>()
                .HasKey(p => p.PlayerId);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.DaysAndTimesAvailable);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.RaidsRequested);

            modelBuilder.Entity<PlayerDayAndTimeAvailable>()
                .HasKey(p => p.PlayerDayAndTimeAvailableId);

            modelBuilder.Entity<PlayerDayAndTimeAvailable>()
                .Property(p => p.PlayerDayAndTimeAvailableId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<PotentialJob>()
                .HasKey(pj => pj.PotentialJobId);

            modelBuilder.Entity<RaidRequested>()
                .HasKey(rr => rr.RaidRequestedId);

            modelBuilder.ComplexType<DayAndTime>();
                //Database.SetInitializer<RaidSchedulerContext>(new MigrateDatabaseToLatestVersion<RaidSchedulerContext, Configuration>());
        }
    }


}