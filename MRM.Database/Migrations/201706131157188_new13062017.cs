namespace MRM.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new13062017 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusinessGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BusinessLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChildCampaigns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CampaignDescription = c.String(),
                        Budget = c.String(),
                        Spend = c.String(),
                        TargetNewPilpeline = c.String(),
                        TargetTouchPilpeline = c.String(),
                        Status = c.String(),
                        StartDate = c.String(),
                        EndDate = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        BusinessGroups_Id = c.Int(),
                        BusinessLines_Id = c.Int(),
                        Geographys_Id = c.Int(),
                        Industries_Id = c.Int(),
                        Segments_Id = c.Int(),
                        Themes_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusinessGroups", t => t.BusinessGroups_Id)
                .ForeignKey("dbo.BusinessLines", t => t.BusinessLines_Id)
                .ForeignKey("dbo.Geographies", t => t.Geographys_Id)
                .ForeignKey("dbo.Industries", t => t.Industries_Id)
                .ForeignKey("dbo.Segments", t => t.Segments_Id)
                .ForeignKey("dbo.Themes", t => t.Themes_Id)
                .Index(t => t.BusinessGroups_Id)
                .Index(t => t.BusinessLines_Id)
                .Index(t => t.Geographys_Id)
                .Index(t => t.Industries_Id)
                .Index(t => t.Segments_Id)
                .Index(t => t.Themes_Id);
            
            CreateTable(
                "dbo.Geographies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Industries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MasterCampaigns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CampaignDescription = c.String(),
                        Status = c.String(),
                        StartDate = c.String(),
                        EndDate = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        BusinessGroups_Id = c.Int(),
                        BusinessLines_Id = c.Int(),
                        Geographys_Id = c.Int(),
                        Industries_Id = c.Int(),
                        Segments_Id = c.Int(),
                        Themes_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusinessGroups", t => t.BusinessGroups_Id)
                .ForeignKey("dbo.BusinessLines", t => t.BusinessLines_Id)
                .ForeignKey("dbo.Geographies", t => t.Geographys_Id)
                .ForeignKey("dbo.Industries", t => t.Industries_Id)
                .ForeignKey("dbo.Segments", t => t.Segments_Id)
                .ForeignKey("dbo.Themes", t => t.Themes_Id)
                .Index(t => t.BusinessGroups_Id)
                .Index(t => t.BusinessLines_Id)
                .Index(t => t.Geographys_Id)
                .Index(t => t.Industries_Id)
                .Index(t => t.Segments_Id)
                .Index(t => t.Themes_Id);
            
            CreateTable(
                "dbo.Segments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TacticCampaigns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TacticDescription = c.String(),
                        Year = c.String(),
                        StartDate = c.String(),
                        EndDate = c.String(),
                        Status = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(),
                        BusinessGroups_Id = c.Int(),
                        BusinessLines_Id = c.Int(),
                        Geographys_Id = c.Int(),
                        Industries_Id = c.Int(),
                        Segments_Id = c.Int(),
                        Themes_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BusinessGroups", t => t.BusinessGroups_Id)
                .ForeignKey("dbo.BusinessLines", t => t.BusinessLines_Id)
                .ForeignKey("dbo.Geographies", t => t.Geographys_Id)
                .ForeignKey("dbo.Industries", t => t.Industries_Id)
                .ForeignKey("dbo.Segments", t => t.Segments_Id)
                .ForeignKey("dbo.Themes", t => t.Themes_Id)
                .Index(t => t.BusinessGroups_Id)
                .Index(t => t.BusinessLines_Id)
                .Index(t => t.Geographys_Id)
                .Index(t => t.Industries_Id)
                .Index(t => t.Segments_Id)
                .Index(t => t.Themes_Id);
            
            CreateTable(
                "dbo.MasterCampaignChildCampaigns",
                c => new
                    {
                        MasterCampaign_Id = c.Int(nullable: false),
                        ChildCampaign_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MasterCampaign_Id, t.ChildCampaign_Id })
                .ForeignKey("dbo.MasterCampaigns", t => t.MasterCampaign_Id, cascadeDelete: true)
                .ForeignKey("dbo.ChildCampaigns", t => t.ChildCampaign_Id, cascadeDelete: true)
                .Index(t => t.MasterCampaign_Id)
                .Index(t => t.ChildCampaign_Id);
            
            CreateTable(
                "dbo.TacticCampaignChildCampaigns",
                c => new
                    {
                        TacticCampaign_Id = c.Int(nullable: false),
                        ChildCampaign_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TacticCampaign_Id, t.ChildCampaign_Id })
                .ForeignKey("dbo.TacticCampaigns", t => t.TacticCampaign_Id, cascadeDelete: true)
                .ForeignKey("dbo.ChildCampaigns", t => t.ChildCampaign_Id, cascadeDelete: true)
                .Index(t => t.TacticCampaign_Id)
                .Index(t => t.ChildCampaign_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChildCampaigns", "Themes_Id", "dbo.Themes");
            DropForeignKey("dbo.TacticCampaigns", "Themes_Id", "dbo.Themes");
            DropForeignKey("dbo.TacticCampaigns", "Segments_Id", "dbo.Segments");
            DropForeignKey("dbo.TacticCampaigns", "Industries_Id", "dbo.Industries");
            DropForeignKey("dbo.TacticCampaigns", "Geographys_Id", "dbo.Geographies");
            DropForeignKey("dbo.TacticCampaignChildCampaigns", "ChildCampaign_Id", "dbo.ChildCampaigns");
            DropForeignKey("dbo.TacticCampaignChildCampaigns", "TacticCampaign_Id", "dbo.TacticCampaigns");
            DropForeignKey("dbo.TacticCampaigns", "BusinessLines_Id", "dbo.BusinessLines");
            DropForeignKey("dbo.TacticCampaigns", "BusinessGroups_Id", "dbo.BusinessGroups");
            DropForeignKey("dbo.ChildCampaigns", "Segments_Id", "dbo.Segments");
            DropForeignKey("dbo.MasterCampaigns", "Themes_Id", "dbo.Themes");
            DropForeignKey("dbo.MasterCampaigns", "Segments_Id", "dbo.Segments");
            DropForeignKey("dbo.MasterCampaigns", "Industries_Id", "dbo.Industries");
            DropForeignKey("dbo.MasterCampaigns", "Geographys_Id", "dbo.Geographies");
            DropForeignKey("dbo.MasterCampaignChildCampaigns", "ChildCampaign_Id", "dbo.ChildCampaigns");
            DropForeignKey("dbo.MasterCampaignChildCampaigns", "MasterCampaign_Id", "dbo.MasterCampaigns");
            DropForeignKey("dbo.MasterCampaigns", "BusinessLines_Id", "dbo.BusinessLines");
            DropForeignKey("dbo.MasterCampaigns", "BusinessGroups_Id", "dbo.BusinessGroups");
            DropForeignKey("dbo.ChildCampaigns", "Industries_Id", "dbo.Industries");
            DropForeignKey("dbo.ChildCampaigns", "Geographys_Id", "dbo.Geographies");
            DropForeignKey("dbo.ChildCampaigns", "BusinessLines_Id", "dbo.BusinessLines");
            DropForeignKey("dbo.ChildCampaigns", "BusinessGroups_Id", "dbo.BusinessGroups");
            DropIndex("dbo.TacticCampaignChildCampaigns", new[] { "ChildCampaign_Id" });
            DropIndex("dbo.TacticCampaignChildCampaigns", new[] { "TacticCampaign_Id" });
            DropIndex("dbo.MasterCampaignChildCampaigns", new[] { "ChildCampaign_Id" });
            DropIndex("dbo.MasterCampaignChildCampaigns", new[] { "MasterCampaign_Id" });
            DropIndex("dbo.TacticCampaigns", new[] { "Themes_Id" });
            DropIndex("dbo.TacticCampaigns", new[] { "Segments_Id" });
            DropIndex("dbo.TacticCampaigns", new[] { "Industries_Id" });
            DropIndex("dbo.TacticCampaigns", new[] { "Geographys_Id" });
            DropIndex("dbo.TacticCampaigns", new[] { "BusinessLines_Id" });
            DropIndex("dbo.TacticCampaigns", new[] { "BusinessGroups_Id" });
            DropIndex("dbo.MasterCampaigns", new[] { "Themes_Id" });
            DropIndex("dbo.MasterCampaigns", new[] { "Segments_Id" });
            DropIndex("dbo.MasterCampaigns", new[] { "Industries_Id" });
            DropIndex("dbo.MasterCampaigns", new[] { "Geographys_Id" });
            DropIndex("dbo.MasterCampaigns", new[] { "BusinessLines_Id" });
            DropIndex("dbo.MasterCampaigns", new[] { "BusinessGroups_Id" });
            DropIndex("dbo.ChildCampaigns", new[] { "Themes_Id" });
            DropIndex("dbo.ChildCampaigns", new[] { "Segments_Id" });
            DropIndex("dbo.ChildCampaigns", new[] { "Industries_Id" });
            DropIndex("dbo.ChildCampaigns", new[] { "Geographys_Id" });
            DropIndex("dbo.ChildCampaigns", new[] { "BusinessLines_Id" });
            DropIndex("dbo.ChildCampaigns", new[] { "BusinessGroups_Id" });
            DropTable("dbo.TacticCampaignChildCampaigns");
            DropTable("dbo.MasterCampaignChildCampaigns");
            DropTable("dbo.TacticCampaigns");
            DropTable("dbo.Themes");
            DropTable("dbo.Segments");
            DropTable("dbo.MasterCampaigns");
            DropTable("dbo.Industries");
            DropTable("dbo.Geographies");
            DropTable("dbo.ChildCampaigns");
            DropTable("dbo.BusinessLines");
            DropTable("dbo.BusinessGroups");
        }
    }
}
