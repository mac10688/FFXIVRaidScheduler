namespace RaidScheduler.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removingplayerreferencefrompotentialjobs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PotentialJob", "PlayerId", "dbo.Player");
            DropIndex("dbo.PotentialJob", new[] { "PlayerId" });
            RenameColumn(table: "dbo.PotentialJob", name: "PlayerId", newName: "Player_PlayerId");
            AlterColumn("dbo.PotentialJob", "Player_PlayerId", c => c.Int());
            CreateIndex("dbo.PotentialJob", "Player_PlayerId");
            AddForeignKey("dbo.PotentialJob", "Player_PlayerId", "dbo.Player", "PlayerId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PotentialJob", "Player_PlayerId", "dbo.Player");
            DropIndex("dbo.PotentialJob", new[] { "Player_PlayerId" });
            AlterColumn("dbo.PotentialJob", "Player_PlayerId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.PotentialJob", name: "Player_PlayerId", newName: "PlayerId");
            CreateIndex("dbo.PotentialJob", "PlayerId");
            AddForeignKey("dbo.PotentialJob", "PlayerId", "dbo.Player", "PlayerId", cascadeDelete: true);
        }
    }
}
