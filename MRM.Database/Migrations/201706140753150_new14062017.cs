namespace MRM.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new14062017 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChildCampaigns", "MarketingInfluenceLeads", c => c.String());
            AddColumn("dbo.ChildCampaigns", "MarketingGeneratedLeads", c => c.String());
            AddColumn("dbo.ChildCampaigns", "MarketingInfluenceOpportunity", c => c.String());
            AddColumn("dbo.ChildCampaigns", "MarketingGeneratedOpportunity", c => c.String());
            DropColumn("dbo.ChildCampaigns", "TargetNewPilpeline");
            DropColumn("dbo.ChildCampaigns", "TargetTouchPilpeline");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChildCampaigns", "TargetTouchPilpeline", c => c.String());
            AddColumn("dbo.ChildCampaigns", "TargetNewPilpeline", c => c.String());
            DropColumn("dbo.ChildCampaigns", "MarketingGeneratedOpportunity");
            DropColumn("dbo.ChildCampaigns", "MarketingInfluenceOpportunity");
            DropColumn("dbo.ChildCampaigns", "MarketingGeneratedLeads");
            DropColumn("dbo.ChildCampaigns", "MarketingInfluenceLeads");
        }
    }
}
