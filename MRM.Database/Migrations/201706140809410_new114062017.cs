namespace MRM.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new114062017 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ChildCampaigns", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.MasterCampaigns", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.TacticCampaigns", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TacticCampaigns", "Status", c => c.String());
            AlterColumn("dbo.MasterCampaigns", "Status", c => c.String());
            AlterColumn("dbo.ChildCampaigns", "Status", c => c.String());
        }
    }
}
