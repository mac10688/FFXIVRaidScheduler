namespace RaidScheduler.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedtheplayerentity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Player", "UserId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Player", "UserId", c => c.String());
        }
    }
}
