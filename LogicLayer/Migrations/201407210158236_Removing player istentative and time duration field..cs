namespace RaidScheduler.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removingplayeristentativeandtimedurationfield : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PlayerDayAndTimeAvailable", "DayAndTime_TimeDurationLimit");
            DropColumn("dbo.PlayerDayAndTimeAvailable", "DayAndTime_IsTentative");
            DropColumn("dbo.StaticPartyDayAndTimeSchedule", "DayAndTime_TimeDurationLimit");
            DropColumn("dbo.StaticPartyDayAndTimeSchedule", "DayAndTime_IsTentative");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StaticPartyDayAndTimeSchedule", "DayAndTime_IsTentative", c => c.Boolean(nullable: false));
            AddColumn("dbo.StaticPartyDayAndTimeSchedule", "DayAndTime_TimeDurationLimit", c => c.Long(nullable: false));
            AddColumn("dbo.PlayerDayAndTimeAvailable", "DayAndTime_IsTentative", c => c.Boolean(nullable: false));
            AddColumn("dbo.PlayerDayAndTimeAvailable", "DayAndTime_TimeDurationLimit", c => c.Long(nullable: false));
        }
    }
}
