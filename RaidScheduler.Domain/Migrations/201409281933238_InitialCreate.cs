namespace RaidScheduler.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        PlayerId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        TimeZone = c.String(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.PlayerId);
            
            CreateTable(
                "dbo.PlayerDayAndTimeAvailable",
                c => new
                    {
                        PlayerDayAndTimeAvailableId = c.Int(nullable: false, identity: true),
                        DayAndTime_DayOfWeek = c.Int(nullable: false),
                        DayAndTime_TimeStart = c.Long(nullable: false),
                        DayAndTime_TimeEnd = c.Long(nullable: false),
                        Player_PlayerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PlayerDayAndTimeAvailableId)
                .ForeignKey("dbo.Player", t => t.Player_PlayerId)
                .Index(t => t.Player_PlayerId);
            
            CreateTable(
                "dbo.PotentialJob",
                c => new
                    {
                        PotentialJobId = c.Int(nullable: false, identity: true),
                        ILvl = c.Int(nullable: false),
                        ComfortLevel = c.Int(nullable: false),
                        JobId = c.Int(nullable: false),
                        Player_PlayerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PotentialJobId)
                .ForeignKey("dbo.Player", t => t.Player_PlayerId)
                .Index(t => t.Player_PlayerId);
            
            CreateTable(
                "dbo.RaidRequested",
                c => new
                    {
                        RaidRequestedId = c.Int(nullable: false, identity: true),
                        FoundRaid = c.Boolean(nullable: false),
                        RaidType = c.Int(nullable: false),
                        Player_PlayerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RaidRequestedId)
                .ForeignKey("dbo.Player", t => t.Player_PlayerId)
                .Index(t => t.Player_PlayerId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.StaticParty",
                c => new
                    {
                        StaticPartyId = c.String(nullable: false, maxLength: 128),
                        RaidType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StaticPartyId);
            
            CreateTable(
                "dbo.StaticPartyDayAndTimeSchedule",
                c => new
                    {
                        StaticPartyDayAndTimeScheduleID = c.Int(nullable: false, identity: true),
                        DayAndTime_DayOfWeek = c.Int(nullable: false),
                        DayAndTime_TimeStart = c.Long(nullable: false),
                        DayAndTime_TimeEnd = c.Long(nullable: false),
                        StaticParty_StaticPartyId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.StaticPartyDayAndTimeScheduleID)
                .ForeignKey("dbo.StaticParty", t => t.StaticParty_StaticPartyId)
                .Index(t => t.StaticParty_StaticPartyId);
            
            CreateTable(
                "dbo.StaticMember",
                c => new
                    {
                        StaticMemberId = c.Int(nullable: false, identity: true),
                        PlayerId = c.String(),
                        ChosenJob = c.Int(nullable: false),
                        StaticParty_StaticPartyId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.StaticMemberId)
                .ForeignKey("dbo.StaticParty", t => t.StaticParty_StaticPartyId)
                .Index(t => t.StaticParty_StaticPartyId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StaticMember", "StaticParty_StaticPartyId", "dbo.StaticParty");
            DropForeignKey("dbo.StaticPartyDayAndTimeSchedule", "StaticParty_StaticPartyId", "dbo.StaticParty");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RaidRequested", "Player_PlayerId", "dbo.Player");
            DropForeignKey("dbo.PotentialJob", "Player_PlayerId", "dbo.Player");
            DropForeignKey("dbo.PlayerDayAndTimeAvailable", "Player_PlayerId", "dbo.Player");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.StaticMember", new[] { "StaticParty_StaticPartyId" });
            DropIndex("dbo.StaticPartyDayAndTimeSchedule", new[] { "StaticParty_StaticPartyId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RaidRequested", new[] { "Player_PlayerId" });
            DropIndex("dbo.PotentialJob", new[] { "Player_PlayerId" });
            DropIndex("dbo.PlayerDayAndTimeAvailable", new[] { "Player_PlayerId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.StaticMember");
            DropTable("dbo.StaticPartyDayAndTimeSchedule");
            DropTable("dbo.StaticParty");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.RaidRequested");
            DropTable("dbo.PotentialJob");
            DropTable("dbo.PlayerDayAndTimeAvailable");
            DropTable("dbo.Player");
        }
    }
}
