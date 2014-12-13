namespace RaidScheduler.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingHowToStoreTimezone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlayerDayAndTimeAvailable", "DayAndTime_Timezone", c => c.String());
            AddColumn("dbo.StaticPartyDayAndTimeSchedule", "DayAndTime_Timezone", c => c.String());
            AddColumn("dbo.AspNetUsers", "PreferredTimezone", c => c.String());
            DropColumn("dbo.Player", "TimeZone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Player", "TimeZone", c => c.String());
            DropColumn("dbo.AspNetUsers", "PreferredTimezone");
            DropColumn("dbo.StaticPartyDayAndTimeSchedule", "DayAndTime_Timezone");
            DropColumn("dbo.PlayerDayAndTimeAvailable", "DayAndTime_Timezone");
        }
    }
}
