namespace RaidScheduler.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeperatingtheplayerobjectfromtheUserobject : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Player", new[] { "User_Id" });
            DropForeignKey("dbo.Player", "FK_dbo.Player_dbo.AspNetUsers_User_Id");
            DropColumn("dbo.Player", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Player", "User_Id", c => c.String(nullable: false, maxLength: 128));
            AddForeignKey("dbo.Player", "User_Id", "dbo.AspNetUsers");
            CreateIndex("dbo.Player", "User_Id");
        }
    }
}
