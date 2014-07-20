using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using RaidScheduler.Domain.DomainModels;
using RaidScheduler.Domain.Migrations;

using Microsoft.AspNet.Identity.EntityFramework;

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
        public DbSet<PlayerDayAndTimeAvailable> PlayerDayAndTimesAvailable { get; set; }
        public DbSet<StaticPartyDayAndTimeSchedule> StaticPartyDayAndTimesAvailable { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<RaidCriteria> RaidCriteria { get; set; }
        public DbSet<PotentialJob> PotentialJobs { get; set; }
        public DbSet<Raid> Raids { get; set; }
        public DbSet<RaidRequested> RaidsRequested { get; set; }
        public DbSet<StaticParty> StaticParties { get; set; }
        public DbSet<StaticMember> StaticPartyMember { get; set; }
        public DbSet<Player> Player { get; set; }
        
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
                .HasMany(p => p.DaysAndTimesAvailable)
                .WithRequired(p => p.Player);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.RaidsRequested)
                .WithRequired(p => p.Player);

            modelBuilder.Entity<Job>()
                .HasKey(j => j.JobId);

            modelBuilder.Entity<PlayerDayAndTimeAvailable>()
                .HasKey(p => p.PlayerDayAndTimeAvailableId);

            modelBuilder.Entity<PlayerDayAndTimeAvailable>()
                .HasRequired(p => p.Player)
                .WithMany(p => p.DaysAndTimesAvailable);

            //modelBuilder.Entity<PotentialJob>()
            //    .HasRequired(j => j.Player)
            //    .WithMany(p => p.PotentialJobs)
            //    .HasForeignKey(p => p.PlayerId);


            modelBuilder.Entity<PotentialJob>()
                .HasRequired(j => j.Job);
                //.WithMany(p => p.PotentialJobs)
                //.HasForeignKey(p => p.JobID);

            //modelBuilder.Entity<RaidCriteria>()
            //    .HasRequired(r => r.Raid)
            //    .WithMany(r => r.RaidCriteria)
            //    .HasForeignKey(r => r.RaidID);

            modelBuilder.Entity<RaidRequested>()
                .HasRequired(r => r.Raid);
                //.WithMany(r => r.RaidRequested)
                //.HasForeignKey(r => r.RaidID);

            modelBuilder.Entity<RaidRequested>()
                .HasRequired(r => r.Player)
                .WithMany(p => p.RaidsRequested)
                .HasForeignKey(r => r.PlayerId);

            modelBuilder.Entity<StaticMember>()
                .HasRequired(s => s.Player)
                .WithMany(p => p.StaticMember)
                .HasForeignKey(s => s.PlayerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaticMember>()
                .HasRequired(s => s.ChosenPotentialJob)
                .WithMany(j => j.StaticMember)
                .HasForeignKey(s => s.ChosenPotentialJobId);

            modelBuilder.Entity<StaticMember>()
                .HasRequired(s => s.StaticParty)
                .WithMany(s => s.StaticMembers)
                .HasForeignKey(s => s.StaticPartyId);

            modelBuilder.Entity<StaticParty>()
                .HasRequired(s => s.Raid);
                //.WithMany(r => r.StaticParty)
                //.HasForeignKey(s => s.RaidID);

            modelBuilder.Entity<StaticPartyDayAndTimeSchedule>()
                .HasRequired(s => s.StaticParty)
                .WithMany(s => s.ScheduledTimes)
                .HasForeignKey(s => s.StaticPartyID);

            modelBuilder.ComplexType<DayAndTime>();

                //Database.SetInitializer<RaidSchedulerContext>(new MigrateDatabaseToLatestVersion<RaidSchedulerContext, Configuration>());
        }
    }


}