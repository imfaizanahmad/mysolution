namespace MRM.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20062017 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ChildCampaignBusinessLines", newName: "BusinessLineChildCampaigns");
            RenameTable(name: "dbo.MasterCampaignGeographies", newName: "GeographyMasterCampaigns");
            RenameTable(name: "dbo.TacticCampaignIndustries", newName: "IndustryTacticCampaigns");
            DropForeignKey("dbo.BusinessLines", "BusinessGroups_Id", "dbo.BusinessGroups");
            DropIndex("dbo.BusinessLines", new[] { "BusinessGroups_Id" });
            DropPrimaryKey("dbo.BusinessLineChildCampaigns");
            DropPrimaryKey("dbo.GeographyMasterCampaigns");
            DropPrimaryKey("dbo.IndustryTacticCampaigns");
            AddPrimaryKey("dbo.BusinessLineChildCampaigns", new[] { "BusinessLine_Id", "ChildCampaign_Id" });
            AddPrimaryKey("dbo.GeographyMasterCampaigns", new[] { "Geography_Id", "MasterCampaign_Id" });
            AddPrimaryKey("dbo.IndustryTacticCampaigns", new[] { "Industry_Id", "TacticCampaign_Id" });
            DropColumn("dbo.BusinessLines", "BusinessGroups_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BusinessLines", "BusinessGroups_Id", c => c.Int());
            DropPrimaryKey("dbo.IndustryTacticCampaigns");
            DropPrimaryKey("dbo.GeographyMasterCampaigns");
            DropPrimaryKey("dbo.BusinessLineChildCampaigns");
            AddPrimaryKey("dbo.IndustryTacticCampaigns", new[] { "TacticCampaign_Id", "Industry_Id" });
            AddPrimaryKey("dbo.GeographyMasterCampaigns", new[] { "MasterCampaign_Id", "Geography_Id" });
            AddPrimaryKey("dbo.BusinessLineChildCampaigns", new[] { "ChildCampaign_Id", "BusinessLine_Id" });
            CreateIndex("dbo.BusinessLines", "BusinessGroups_Id");
            AddForeignKey("dbo.BusinessLines", "BusinessGroups_Id", "dbo.BusinessGroups", "Id");
            RenameTable(name: "dbo.IndustryTacticCampaigns", newName: "TacticCampaignIndustries");
            RenameTable(name: "dbo.GeographyMasterCampaigns", newName: "MasterCampaignGeographies");
            RenameTable(name: "dbo.BusinessLineChildCampaigns", newName: "ChildCampaignBusinessLines");
        }
    }
}
