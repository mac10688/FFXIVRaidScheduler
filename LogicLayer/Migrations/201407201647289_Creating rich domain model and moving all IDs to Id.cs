namespace RaidScheduler.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatingrichdomainmodelandmovingallIDstoId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RaidCriteria", "RaidID", "dbo.Raid");
            DropIndex("dbo.PotentialJob", new[] { "PlayerID" });
            DropIndex("dbo.PotentialJob", new[] { "JobID" });
            DropIndex("dbo.RaidRequested", new[] { "RaidID" });
            DropIndex("dbo.RaidRequested", new[] { "PlayerID" });
            DropIndex("dbo.RaidCriteria", new[] { "RaidID" });
            DropIndex("dbo.StaticParty", new[] { "RaidID" });
            DropIndex("dbo.StaticMember", new[] { "PlayerID" });
            DropIndex("dbo.StaticMember", new[] { "ChosenPotentialJobID" });
            DropIndex("dbo.StaticMember", new[] { "StaticPartyID" });
            RenameColumn(table: "dbo.RaidCriteria", name: "RaidID", newName: "Raid_RaidId");
            AlterColumn("dbo.RaidCriteria", "Raid_RaidId", c => c.Int());
            CreateIndex("dbo.PotentialJob", "PlayerId");
            CreateIndex("dbo.PotentialJob", "JobId");
            CreateIndex("dbo.StaticMember", "PlayerId");
            CreateIndex("dbo.StaticMember", "ChosenPotentialJobId");
            CreateIndex("dbo.StaticMember", "StaticPartyId");
            CreateIndex("dbo.StaticParty", "RaidId");
            CreateIndex("dbo.RaidCriteria", "Raid_RaidId");
            CreateIndex("dbo.RaidRequested", "RaidId");
            CreateIndex("dbo.RaidRequested", "PlayerId");
            AddForeignKey("dbo.RaidCriteria", "Raid_RaidId", "dbo.Raid", "RaidId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RaidCriteria", "Raid_RaidId", "dbo.Raid");
            DropIndex("dbo.RaidRequested", new[] { "PlayerId" });
            DropIndex("dbo.RaidRequested", new[] { "RaidId" });
            DropIndex("dbo.RaidCriteria", new[] { "Raid_RaidId" });
            DropIndex("dbo.StaticParty", new[] { "RaidId" });
            DropIndex("dbo.StaticMember", new[] { "StaticPartyId" });
            DropIndex("dbo.StaticMember", new[] { "ChosenPotentialJobId" });
            DropIndex("dbo.StaticMember", new[] { "PlayerId" });
            DropIndex("dbo.PotentialJob", new[] { "JobId" });
            DropIndex("dbo.PotentialJob", new[] { "PlayerId" });
            AlterColumn("dbo.RaidCriteria", "Raid_RaidId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.RaidCriteria", name: "Raid_RaidId", newName: "RaidID");
            CreateIndex("dbo.StaticMember", "StaticPartyID");
            CreateIndex("dbo.StaticMember", "ChosenPotentialJobID");
            CreateIndex("dbo.StaticMember", "PlayerID");
            CreateIndex("dbo.StaticParty", "RaidID");
            CreateIndex("dbo.RaidCriteria", "RaidID");
            CreateIndex("dbo.RaidRequested", "PlayerID");
            CreateIndex("dbo.RaidRequested", "RaidID");
            CreateIndex("dbo.PotentialJob", "JobID");
            CreateIndex("dbo.PotentialJob", "PlayerID");
            AddForeignKey("dbo.RaidCriteria", "RaidID", "dbo.Raid", "RaidID", cascadeDelete: true);
        }
    }
}
