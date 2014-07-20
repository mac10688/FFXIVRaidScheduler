namespace RaidScheduler.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Job",
                c => new
                    {
                        JobID = c.Int(nullable: false, identity: true),
                        JobName = c.String(),
                        IsMeleeDps = c.Boolean(nullable: false),
                        IsRangedDps = c.Boolean(nullable: false),
                        CanStun = c.Boolean(nullable: false),
                        CanSilence = c.Boolean(nullable: false),
                        IsMagicalDps = c.Boolean(nullable: false),
                        IsPhysicalDps = c.Boolean(nullable: false),
                        IsHealer = c.Boolean(nullable: false),
                        IsTank = c.Boolean(nullable: false),
                        IsDps = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.JobID);
            
            CreateTable(
                "dbo.PotentialJob",
                c => new
                    {
                        PotentialJobID = c.Int(nullable: false, identity: true),
                        ILvl = c.Int(nullable: false),
                        ComfortLevel = c.Int(nullable: false),
                        PlayerID = c.Int(nullable: false),
                        JobID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PotentialJobID)
                .ForeignKey("dbo.Job", t => t.JobID, cascadeDelete: true)
                .ForeignKey("dbo.Player", t => t.PlayerID, cascadeDelete: true)
                .Index(t => t.PlayerID)
                .Index(t => t.JobID);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        PlayerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        TimeZone = c.String(),
                        UserId = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.PlayerID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.PlayerDayAndTimeAvailable",
                c => new
                    {
                        PlayerDayAndTimeAvailableID = c.Int(nullable: false, identity: true),
                        DayAndTime_DayOfWeek = c.Int(nullable: false),
                        DayAndTime_TimeStart = c.Long(nullable: false),
                        DayAndTime_TimeEnd = c.Long(nullable: false),
                        DayAndTime_TimeDurationLimit = c.Long(nullable: false),
                        DayAndTime_IsTentative = c.Boolean(nullable: false),
                        PlayerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerDayAndTimeAvailableID)
                .ForeignKey("dbo.Player", t => t.PlayerID, cascadeDelete: true)
                .Index(t => t.PlayerID);
            
            CreateTable(
                "dbo.RaidRequested",
                c => new
                    {
                        RaidRequestedID = c.Int(nullable: false, identity: true),
                        FoundRaid = c.Boolean(nullable: false),
                        RaidID = c.Int(nullable: false),
                        PlayerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RaidRequestedID)
                .ForeignKey("dbo.Raid", t => t.RaidID, cascadeDelete: true)
                .ForeignKey("dbo.Player", t => t.PlayerID, cascadeDelete: true)
                .Index(t => t.RaidID)
                .Index(t => t.PlayerID);
            
            CreateTable(
                "dbo.Raid",
                c => new
                    {
                        RaidID = c.Int(nullable: false, identity: true),
                        RaidName = c.String(),
                    })
                .PrimaryKey(t => t.RaidID);
            
            CreateTable(
                "dbo.RaidCriteria",
                c => new
                    {
                        RaidCriteriaID = c.Int(nullable: false, identity: true),
                        NumberOfStunners = c.Int(),
                        NumberOfTanks = c.Int(),
                        NumberOfHealers = c.Int(),
                        NumberOfDps = c.Int(),
                        NumberOfMeleeDps = c.Int(),
                        NumberOfRangedDps = c.Int(),
                        NumberOfMagicalDps = c.Int(),
                        NumberOfPhysicalDps = c.Int(),
                        NumberOfSilencers = c.Int(),
                        MinILvl = c.Int(nullable: false),
                        NumberOfPlayersRequired = c.Int(nullable: false),
                        RaidID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RaidCriteriaID)
                .ForeignKey("dbo.Raid", t => t.RaidID, cascadeDelete: true)
                .Index(t => t.RaidID);
            
            CreateTable(
                "dbo.StaticParty",
                c => new
                    {
                        StaticPartyID = c.Int(nullable: false, identity: true),
                        RaidID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StaticPartyID)
                .ForeignKey("dbo.Raid", t => t.RaidID, cascadeDelete: true)
                .Index(t => t.RaidID);
            
            CreateTable(
                "dbo.StaticPartyDayAndTimeSchedule",
                c => new
                    {
                        StaticPartyDayAndTimeScheduleID = c.Int(nullable: false, identity: true),
                        DayAndTime_DayOfWeek = c.Int(nullable: false),
                        DayAndTime_TimeStart = c.Long(nullable: false),
                        DayAndTime_TimeEnd = c.Long(nullable: false),
                        DayAndTime_TimeDurationLimit = c.Long(nullable: false),
                        DayAndTime_IsTentative = c.Boolean(nullable: false),
                        StaticPartyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StaticPartyDayAndTimeScheduleID)
                .ForeignKey("dbo.StaticParty", t => t.StaticPartyID, cascadeDelete: true)
                .Index(t => t.StaticPartyID);
            
            CreateTable(
                "dbo.StaticMember",
                c => new
                    {
                        StaticMemberID = c.Int(nullable: false, identity: true),
                        PlayerID = c.Int(nullable: false),
                        ChosenPotentialJobID = c.Int(nullable: false),
                        StaticPartyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StaticMemberID)
                .ForeignKey("dbo.PotentialJob", t => t.ChosenPotentialJobID, cascadeDelete: true)
                .ForeignKey("dbo.Player", t => t.PlayerID)
                .ForeignKey("dbo.StaticParty", t => t.StaticPartyID, cascadeDelete: true)
                .Index(t => t.PlayerID)
                .Index(t => t.ChosenPotentialJobID)
                .Index(t => t.StaticPartyID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PotentialJob", "PlayerID", "dbo.Player");
            DropForeignKey("dbo.Player", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RaidRequested", "PlayerID", "dbo.Player");
            DropForeignKey("dbo.RaidRequested", "RaidID", "dbo.Raid");
            DropForeignKey("dbo.StaticMember", "StaticPartyID", "dbo.StaticParty");
            DropForeignKey("dbo.StaticMember", "PlayerID", "dbo.Player");
            DropForeignKey("dbo.StaticMember", "ChosenPotentialJobID", "dbo.PotentialJob");
            DropForeignKey("dbo.StaticPartyDayAndTimeSchedule", "StaticPartyID", "dbo.StaticParty");
            DropForeignKey("dbo.StaticParty", "RaidID", "dbo.Raid");
            DropForeignKey("dbo.RaidCriteria", "RaidID", "dbo.Raid");
            DropForeignKey("dbo.PlayerDayAndTimeAvailable", "PlayerID", "dbo.Player");
            DropForeignKey("dbo.PotentialJob", "JobID", "dbo.Job");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.StaticMember", new[] { "StaticPartyID" });
            DropIndex("dbo.StaticMember", new[] { "ChosenPotentialJobID" });
            DropIndex("dbo.StaticMember", new[] { "PlayerID" });
            DropIndex("dbo.StaticPartyDayAndTimeSchedule", new[] { "StaticPartyID" });
            DropIndex("dbo.StaticParty", new[] { "RaidID" });
            DropIndex("dbo.RaidCriteria", new[] { "RaidID" });
            DropIndex("dbo.RaidRequested", new[] { "PlayerID" });
            DropIndex("dbo.RaidRequested", new[] { "RaidID" });
            DropIndex("dbo.PlayerDayAndTimeAvailable", new[] { "PlayerID" });
            DropIndex("dbo.Player", new[] { "User_Id" });
            DropIndex("dbo.PotentialJob", new[] { "JobID" });
            DropIndex("dbo.PotentialJob", new[] { "PlayerID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.StaticMember");
            DropTable("dbo.StaticPartyDayAndTimeSchedule");
            DropTable("dbo.StaticParty");
            DropTable("dbo.RaidCriteria");
            DropTable("dbo.Raid");
            DropTable("dbo.RaidRequested");
            DropTable("dbo.PlayerDayAndTimeAvailable");
            DropTable("dbo.Player");
            DropTable("dbo.PotentialJob");
            DropTable("dbo.Job");
        }
    }
}
