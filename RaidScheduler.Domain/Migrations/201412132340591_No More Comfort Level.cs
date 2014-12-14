namespace RaidScheduler.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoMoreComfortLevel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PotentialJob", "ComfortLevel");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PotentialJob", "ComfortLevel", c => c.Int(nullable: false));
        }
    }
}
