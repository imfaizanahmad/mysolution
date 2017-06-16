namespace MRM.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationShichanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChildCampaigns", "BusinessGroups_Id", "dbo.BusinessGroups");
            DropForeignKey("dbo.ChildCampaigns", "BusinessLines_Id", "dbo.BusinessLines");
            DropForeignKey("dbo.ChildCampaigns", "Geographys_Id", "dbo.Geographies");
            DropForeignKey("dbo.ChildCampaigns", "Industries_Id", "dbo.Industries");
            DropForeignKey("dbo.MasterCampaigns", "BusinessGroups_Id", "dbo.BusinessGroups");
            DropForeignKey("dbo.MasterCampaigns", "BusinessLines_Id", "dbo.BusinessLines");
            DropForeignKey("dbo.MasterCampaignChildCampaigns", "MasterCampaign_Id", "dbo.MasterCampaigns");
            DropForeignKey("dbo.MasterCampaignChildCampaigns", "ChildCampaign_Id", "dbo.ChildCampaigns");
            DropForeignKey("dbo.MasterCampaigns", "Geographys_Id", "dbo.Geographies");
            DropForeignKey("dbo.MasterCampaigns", "Industries_Id", "dbo.Industries");
            DropForeignKey("dbo.MasterCampaigns", "Segments_Id", "dbo.Segments");
            DropForeignKey("dbo.MasterCampaigns", "Themes_Id", "dbo.Themes");
            DropForeignKey("dbo.ChildCampaigns", "Segments_Id", "dbo.Segments");
            DropForeignKey("dbo.TacticCampaigns", "BusinessGroups_Id", "dbo.BusinessGroups");
            DropForeignKey("dbo.TacticCampaigns", "BusinessLines_Id", "dbo.BusinessLines");
            DropForeignKey("dbo.TacticCampaignChildCampaigns", "TacticCampaign_Id", "dbo.TacticCampaigns");
            DropForeignKey("dbo.TacticCampaignChildCampaigns", "ChildCampaign_Id", "dbo.ChildCampaigns");
            DropForeignKey("dbo.TacticCampaigns", "Geographys_Id", "dbo.Geographies");
            DropForeignKey("dbo.TacticCampaigns", "Industries_Id", "dbo.Industries");
            DropForeignKey("dbo.TacticCampaigns", "Segments_Id", "dbo.Segments");
            DropForeignKey("dbo.TacticCampaigns", "Themes_Id", "dbo.Themes");
            DropForeignKey("dbo.ChildCampaigns", "Themes_Id", "dbo.Themes");
            DropIndex("dbo.ChildCampaigns", new[] { "BusinessGroups_Id" });
            DropIndex("dbo.ChildCampaigns", new[] { "BusinessLines_Id" });
            DropIndex("dbo.ChildCampaigns", new[] { "Geographys_Id" });
            DropIndex("dbo.ChildCampaigns", new[] { "Industries_Id" });
            DropIndex("dbo.ChildCampaigns", new[] { "Segments_Id" });
            DropIndex("dbo.ChildCampaigns", new[] { "Themes_Id" });
            DropIndex("dbo.MasterCampaigns", new[] { "BusinessGroups_Id" });
            DropIndex("dbo.MasterCampaigns", new[] { "BusinessLines_Id" });
            DropIndex("dbo.MasterCampaigns", new[] { "Geographys_Id" });
            DropIndex("dbo.MasterCampaigns", new[] { "Industries_Id" });
            DropIndex("dbo.MasterCampaigns", new[] { "Segments_Id" });
            DropIndex("dbo.MasterCampaigns", new[] { "Themes_Id" });
            DropIndex("dbo.TacticCampaigns", new[] { "BusinessGroups_Id" });
            DropIndex("dbo.TacticCampaigns", new[] { "BusinessLines_Id" });
            DropIndex("dbo.TacticCampaigns", new[] { "Geographys_Id" });
            DropIndex("dbo.TacticCampaigns", new[] { "Industries_Id" });
            DropIndex("dbo.TacticCampaigns", new[] { "Segments_Id" });
            DropIndex("dbo.TacticCampaigns", new[] { "Themes_Id" });
            DropIndex("dbo.MasterCampaignChildCampaigns", new[] { "MasterCampaign_Id" });
            DropIndex("dbo.MasterCampaignChildCampaigns", new[] { "ChildCampaign_Id" });
            DropIndex("dbo.TacticCampaignChildCampaigns", new[] { "TacticCampaign_Id" });
            DropIndex("dbo.TacticCampaignChildCampaigns", new[] { "ChildCampaign_Id" });
            CreateTable(
                "dbo.ChildCampaignBusinessGroups",
                c => new
                    {
                        ChildCampaign_Id = c.Int(nullable: false),
                        BusinessGroup_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ChildCampaign_Id, t.BusinessGroup_Id })
                .ForeignKey("dbo.ChildCampaigns", t => t.ChildCampaign_Id, cascadeDelete: true)
                .ForeignKey("dbo.BusinessGroups", t => t.BusinessGroup_Id, cascadeDelete: true)
                .Index(t => t.ChildCampaign_Id)
                .Index(t => t.BusinessGroup_Id);
            
            CreateTable(
                "dbo.BusinessLineChildCampaigns",
                c => new
                    {
                        BusinessLine_Id = c.Int(nullable: false),
                        ChildCampaign_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BusinessLine_Id, t.ChildCampaign_Id })
                .ForeignKey("dbo.BusinessLines", t => t.BusinessLine_Id, cascadeDelete: true)
                .ForeignKey("dbo.ChildCampaigns", t => t.ChildCampaign_Id, cascadeDelete: true)
                .Index(t => t.BusinessLine_Id)
                .Index(t => t.ChildCampaign_Id);
            
            CreateTable(
                "dbo.MasterCampaignBusinessGroups",
                c => new
                    {
                        MasterCampaign_Id = c.Int(nullable: false),
                        BusinessGroup_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MasterCampaign_Id, t.BusinessGroup_Id })
                .ForeignKey("dbo.MasterCampaigns", t => t.MasterCampaign_Id, cascadeDelete: true)
                .ForeignKey("dbo.BusinessGroups", t => t.BusinessGroup_Id, cascadeDelete: true)
                .Index(t => t.MasterCampaign_Id)
                .Index(t => t.BusinessGroup_Id);
            
            CreateTable(
                "dbo.MasterCampaignBusinessLines",
                c => new
                    {
                        MasterCampaign_Id = c.Int(nullable: false),
                        BusinessLine_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MasterCampaign_Id, t.BusinessLine_Id })
                .ForeignKey("dbo.MasterCampaigns", t => t.MasterCampaign_Id, cascadeDelete: true)
                .ForeignKey("dbo.BusinessLines", t => t.BusinessLine_Id, cascadeDelete: true)
                .Index(t => t.MasterCampaign_Id)
                .Index(t => t.BusinessLine_Id);
            
            CreateTable(
                "dbo.GeographyChildCampaigns",
                c => new
                    {
                        Geography_Id = c.Int(nullable: false),
                        ChildCampaign_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Geography_Id, t.ChildCampaign_Id })
                .ForeignKey("dbo.Geographies", t => t.Geography_Id, cascadeDelete: true)
                .ForeignKey("dbo.ChildCampaigns", t => t.ChildCampaign_Id, cascadeDelete: true)
                .Index(t => t.Geography_Id)
                .Index(t => t.ChildCampaign_Id);
            
            CreateTable(
                "dbo.GeographyMasterCampaigns",
                c => new
                    {
                        Geography_Id = c.Int(nullable: false),
                        MasterCampaign_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Geography_Id, t.MasterCampaign_Id })
                .ForeignKey("dbo.Geographies", t => t.Geography_Id, cascadeDelete: true)
                .ForeignKey("dbo.MasterCampaigns", t => t.MasterCampaign_Id, cascadeDelete: true)
                .Index(t => t.Geography_Id)
                .Index(t => t.MasterCampaign_Id);
            
            CreateTable(
                "dbo.TacticCampaignBusinessGroups",
                c => new
                    {
                        TacticCampaign_Id = c.Int(nullable: false),
                        BusinessGroup_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TacticCampaign_Id, t.BusinessGroup_Id })
                .ForeignKey("dbo.TacticCampaigns", t => t.TacticCampaign_Id, cascadeDelete: true)
                .ForeignKey("dbo.BusinessGroups", t => t.BusinessGroup_Id, cascadeDelete: true)
                .Index(t => t.TacticCampaign_Id)
                .Index(t => t.BusinessGroup_Id);
            
            CreateTable(
                "dbo.TacticCampaignBusinessLines",
                c => new
                    {
                        TacticCampaign_Id = c.Int(nullable: false),
                        BusinessLine_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TacticCampaign_Id, t.BusinessLine_Id })
                .ForeignKey("dbo.TacticCampaigns", t => t.TacticCampaign_Id, cascadeDelete: true)
                .ForeignKey("dbo.BusinessLines", t => t.BusinessLine_Id, cascadeDelete: true)
                .Index(t => t.TacticCampaign_Id)
                .Index(t => t.BusinessLine_Id);
            
            CreateTable(
                "dbo.TacticCampaignGeographies",
                c => new
                    {
                        TacticCampaign_Id = c.Int(nullable: false),
                        Geography_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TacticCampaign_Id, t.Geography_Id })
                .ForeignKey("dbo.TacticCampaigns", t => t.TacticCampaign_Id, cascadeDelete: true)
                .ForeignKey("dbo.Geographies", t => t.Geography_Id, cascadeDelete: true)
                .Index(t => t.TacticCampaign_Id)
                .Index(t => t.Geography_Id);
            
            CreateTable(
                "dbo.IndustryChildCampaigns",
                c => new
                    {
                        Industry_Id = c.Int(nullable: false),
                        ChildCampaign_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Industry_Id, t.ChildCampaign_Id })
                .ForeignKey("dbo.Industries", t => t.Industry_Id, cascadeDelete: true)
                .ForeignKey("dbo.ChildCampaigns", t => t.ChildCampaign_Id, cascadeDelete: true)
                .Index(t => t.Industry_Id)
                .Index(t => t.ChildCampaign_Id);
            
            CreateTable(
                "dbo.IndustryMasterCampaigns",
                c => new
                    {
                        Industry_Id = c.Int(nullable: false),
                        MasterCampaign_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Industry_Id, t.MasterCampaign_Id })
                .ForeignKey("dbo.Industries", t => t.Industry_Id, cascadeDelete: true)
                .ForeignKey("dbo.MasterCampaigns", t => t.MasterCampaign_Id, cascadeDelete: true)
                .Index(t => t.Industry_Id)
                .Index(t => t.MasterCampaign_Id);
            
            CreateTable(
                "dbo.IndustryTacticCampaigns",
                c => new
                    {
                        Industry_Id = c.Int(nullable: false),
                        TacticCampaign_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Industry_Id, t.TacticCampaign_Id })
                .ForeignKey("dbo.Industries", t => t.Industry_Id, cascadeDelete: true)
                .ForeignKey("dbo.TacticCampaigns", t => t.TacticCampaign_Id, cascadeDelete: true)
                .Index(t => t.Industry_Id)
                .Index(t => t.TacticCampaign_Id);
            
            CreateTable(
                "dbo.SegmentChildCampaigns",
                c => new
                    {
                        Segment_Id = c.Int(nullable: false),
                        ChildCampaign_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Segment_Id, t.ChildCampaign_Id })
                .ForeignKey("dbo.Segments", t => t.Segment_Id, cascadeDelete: true)
                .ForeignKey("dbo.ChildCampaigns", t => t.ChildCampaign_Id, cascadeDelete: true)
                .Index(t => t.Segment_Id)
                .Index(t => t.ChildCampaign_Id);
            
            CreateTable(
                "dbo.SegmentMasterCampaigns",
                c => new
                    {
                        Segment_Id = c.Int(nullable: false),
                        MasterCampaign_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Segment_Id, t.MasterCampaign_Id })
                .ForeignKey("dbo.Segments", t => t.Segment_Id, cascadeDelete: true)
                .ForeignKey("dbo.MasterCampaigns", t => t.MasterCampaign_Id, cascadeDelete: true)
                .Index(t => t.Segment_Id)
                .Index(t => t.MasterCampaign_Id);
            
            CreateTable(
                "dbo.SegmentTacticCampaigns",
                c => new
                    {
                        Segment_Id = c.Int(nullable: false),
                        TacticCampaign_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Segment_Id, t.TacticCampaign_Id })
                .ForeignKey("dbo.Segments", t => t.Segment_Id, cascadeDelete: true)
                .ForeignKey("dbo.TacticCampaigns", t => t.TacticCampaign_Id, cascadeDelete: true)
                .Index(t => t.Segment_Id)
                .Index(t => t.TacticCampaign_Id);
            
            CreateTable(
                "dbo.ThemeChildCampaigns",
                c => new
                    {
                        Theme_Id = c.Int(nullable: false),
                        ChildCampaign_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Theme_Id, t.ChildCampaign_Id })
                .ForeignKey("dbo.Themes", t => t.Theme_Id, cascadeDelete: true)
                .ForeignKey("dbo.ChildCampaigns", t => t.ChildCampaign_Id, cascadeDelete: true)
                .Index(t => t.Theme_Id)
                .Index(t => t.ChildCampaign_Id);
            
            CreateTable(
                "dbo.ThemeMasterCampaigns",
                c => new
                    {
                        Theme_Id = c.Int(nullable: false),
                        MasterCampaign_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Theme_Id, t.MasterCampaign_Id })
                .ForeignKey("dbo.Themes", t => t.Theme_Id, cascadeDelete: true)
                .ForeignKey("dbo.MasterCampaigns", t => t.MasterCampaign_Id, cascadeDelete: true)
                .Index(t => t.Theme_Id)
                .Index(t => t.MasterCampaign_Id);
            
            CreateTable(
                "dbo.ThemeTacticCampaigns",
                c => new
                    {
                        Theme_Id = c.Int(nullable: false),
                        TacticCampaign_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Theme_Id, t.TacticCampaign_Id })
                .ForeignKey("dbo.Themes", t => t.Theme_Id, cascadeDelete: true)
                .ForeignKey("dbo.TacticCampaigns", t => t.TacticCampaign_Id, cascadeDelete: true)
                .Index(t => t.Theme_Id)
                .Index(t => t.TacticCampaign_Id);
            
            AddColumn("dbo.ChildCampaigns", "MasterCampaigns_Id", c => c.Int());
            AddColumn("dbo.TacticCampaigns", "ChildCampaigns_Id", c => c.Int());
            CreateIndex("dbo.ChildCampaigns", "MasterCampaigns_Id");
            CreateIndex("dbo.TacticCampaigns", "ChildCampaigns_Id");
            AddForeignKey("dbo.ChildCampaigns", "MasterCampaigns_Id", "dbo.MasterCampaigns", "Id");
            AddForeignKey("dbo.TacticCampaigns", "ChildCampaigns_Id", "dbo.ChildCampaigns", "Id");
            DropColumn("dbo.ChildCampaigns", "BusinessGroups_Id");
            DropColumn("dbo.ChildCampaigns", "BusinessLines_Id");
            DropColumn("dbo.ChildCampaigns", "Geographys_Id");
            DropColumn("dbo.ChildCampaigns", "Industries_Id");
            DropColumn("dbo.ChildCampaigns", "Segments_Id");
            DropColumn("dbo.ChildCampaigns", "Themes_Id");
            DropColumn("dbo.MasterCampaigns", "BusinessGroups_Id");
            DropColumn("dbo.MasterCampaigns", "BusinessLines_Id");
            DropColumn("dbo.MasterCampaigns", "Geographys_Id");
            DropColumn("dbo.MasterCampaigns", "Industries_Id");
            DropColumn("dbo.MasterCampaigns", "Segments_Id");
            DropColumn("dbo.MasterCampaigns", "Themes_Id");
            DropColumn("dbo.TacticCampaigns", "BusinessGroups_Id");
            DropColumn("dbo.TacticCampaigns", "BusinessLines_Id");
            DropColumn("dbo.TacticCampaigns", "Geographys_Id");
            DropColumn("dbo.TacticCampaigns", "Industries_Id");
            DropColumn("dbo.TacticCampaigns", "Segments_Id");
            DropColumn("dbo.TacticCampaigns", "Themes_Id");
            DropTable("dbo.MasterCampaignChildCampaigns");
            DropTable("dbo.TacticCampaignChildCampaigns");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TacticCampaignChildCampaigns",
                c => new
                    {
                        TacticCampaign_Id = c.Int(nullable: false),
                        ChildCampaign_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TacticCampaign_Id, t.ChildCampaign_Id });
            
            CreateTable(
                "dbo.MasterCampaignChildCampaigns",
                c => new
                    {
                        MasterCampaign_Id = c.Int(nullable: false),
                        ChildCampaign_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MasterCampaign_Id, t.ChildCampaign_Id });
            
            AddColumn("dbo.TacticCampaigns", "Themes_Id", c => c.Int());
            AddColumn("dbo.TacticCampaigns", "Segments_Id", c => c.Int());
            AddColumn("dbo.TacticCampaigns", "Industries_Id", c => c.Int());
            AddColumn("dbo.TacticCampaigns", "Geographys_Id", c => c.Int());
            AddColumn("dbo.TacticCampaigns", "BusinessLines_Id", c => c.Int());
            AddColumn("dbo.TacticCampaigns", "BusinessGroups_Id", c => c.Int());
            AddColumn("dbo.MasterCampaigns", "Themes_Id", c => c.Int());
            AddColumn("dbo.MasterCampaigns", "Segments_Id", c => c.Int());
            AddColumn("dbo.MasterCampaigns", "Industries_Id", c => c.Int());
            AddColumn("dbo.MasterCampaigns", "Geographys_Id", c => c.Int());
            AddColumn("dbo.MasterCampaigns", "BusinessLines_Id", c => c.Int());
            AddColumn("dbo.MasterCampaigns", "BusinessGroups_Id", c => c.Int());
            AddColumn("dbo.ChildCampaigns", "Themes_Id", c => c.Int());
            AddColumn("dbo.ChildCampaigns", "Segments_Id", c => c.Int());
            AddColumn("dbo.ChildCampaigns", "Industries_Id", c => c.Int());
            AddColumn("dbo.ChildCampaigns", "Geographys_Id", c => c.Int());
            AddColumn("dbo.ChildCampaigns", "BusinessLines_Id", c => c.Int());
            AddColumn("dbo.ChildCampaigns", "BusinessGroups_Id", c => c.Int());
            DropForeignKey("dbo.ThemeTacticCampaigns", "TacticCampaign_Id", "dbo.TacticCampaigns");
            DropForeignKey("dbo.ThemeTacticCampaigns", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.ThemeMasterCampaigns", "MasterCampaign_Id", "dbo.MasterCampaigns");
            DropForeignKey("dbo.ThemeMasterCampaigns", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.ThemeChildCampaigns", "ChildCampaign_Id", "dbo.ChildCampaigns");
            DropForeignKey("dbo.ThemeChildCampaigns", "Theme_Id", "dbo.Themes");
            DropForeignKey("dbo.SegmentTacticCampaigns", "TacticCampaign_Id", "dbo.TacticCampaigns");
            DropForeignKey("dbo.SegmentTacticCampaigns", "Segment_Id", "dbo.Segments");
            DropForeignKey("dbo.SegmentMasterCampaigns", "MasterCampaign_Id", "dbo.MasterCampaigns");
            DropForeignKey("dbo.SegmentMasterCampaigns", "Segment_Id", "dbo.Segments");
            DropForeignKey("dbo.SegmentChildCampaigns", "ChildCampaign_Id", "dbo.ChildCampaigns");
            DropForeignKey("dbo.SegmentChildCampaigns", "Segment_Id", "dbo.Segments");
            DropForeignKey("dbo.IndustryTacticCampaigns", "TacticCampaign_Id", "dbo.TacticCampaigns");
            DropForeignKey("dbo.IndustryTacticCampaigns", "Industry_Id", "dbo.Industries");
            DropForeignKey("dbo.IndustryMasterCampaigns", "MasterCampaign_Id", "dbo.MasterCampaigns");
            DropForeignKey("dbo.IndustryMasterCampaigns", "Industry_Id", "dbo.Industries");
            DropForeignKey("dbo.IndustryChildCampaigns", "ChildCampaign_Id", "dbo.ChildCampaigns");
            DropForeignKey("dbo.IndustryChildCampaigns", "Industry_Id", "dbo.Industries");
            DropForeignKey("dbo.TacticCampaignGeographies", "Geography_Id", "dbo.Geographies");
            DropForeignKey("dbo.TacticCampaignGeographies", "TacticCampaign_Id", "dbo.TacticCampaigns");
            DropForeignKey("dbo.TacticCampaigns", "ChildCampaigns_Id", "dbo.ChildCampaigns");
            DropForeignKey("dbo.TacticCampaignBusinessLines", "BusinessLine_Id", "dbo.BusinessLines");
            DropForeignKey("dbo.TacticCampaignBusinessLines", "TacticCampaign_Id", "dbo.TacticCampaigns");
            DropForeignKey("dbo.TacticCampaignBusinessGroups", "BusinessGroup_Id", "dbo.BusinessGroups");
            DropForeignKey("dbo.TacticCampaignBusinessGroups", "TacticCampaign_Id", "dbo.TacticCampaigns");
            DropForeignKey("dbo.GeographyMasterCampaigns", "MasterCampaign_Id", "dbo.MasterCampaigns");
            DropForeignKey("dbo.GeographyMasterCampaigns", "Geography_Id", "dbo.Geographies");
            DropForeignKey("dbo.GeographyChildCampaigns", "ChildCampaign_Id", "dbo.ChildCampaigns");
            DropForeignKey("dbo.GeographyChildCampaigns", "Geography_Id", "dbo.Geographies");
            DropForeignKey("dbo.ChildCampaigns", "MasterCampaigns_Id", "dbo.MasterCampaigns");
            DropForeignKey("dbo.MasterCampaignBusinessLines", "BusinessLine_Id", "dbo.BusinessLines");
            DropForeignKey("dbo.MasterCampaignBusinessLines", "MasterCampaign_Id", "dbo.MasterCampaigns");
            DropForeignKey("dbo.MasterCampaignBusinessGroups", "BusinessGroup_Id", "dbo.BusinessGroups");
            DropForeignKey("dbo.MasterCampaignBusinessGroups", "MasterCampaign_Id", "dbo.MasterCampaigns");
            DropForeignKey("dbo.BusinessLineChildCampaigns", "ChildCampaign_Id", "dbo.ChildCampaigns");
            DropForeignKey("dbo.BusinessLineChildCampaigns", "BusinessLine_Id", "dbo.BusinessLines");
            DropForeignKey("dbo.ChildCampaignBusinessGroups", "BusinessGroup_Id", "dbo.BusinessGroups");
            DropForeignKey("dbo.ChildCampaignBusinessGroups", "ChildCampaign_Id", "dbo.ChildCampaigns");
            DropIndex("dbo.ThemeTacticCampaigns", new[] { "TacticCampaign_Id" });
            DropIndex("dbo.ThemeTacticCampaigns", new[] { "Theme_Id" });
            DropIndex("dbo.ThemeMasterCampaigns", new[] { "MasterCampaign_Id" });
            DropIndex("dbo.ThemeMasterCampaigns", new[] { "Theme_Id" });
            DropIndex("dbo.ThemeChildCampaigns", new[] { "ChildCampaign_Id" });
            DropIndex("dbo.ThemeChildCampaigns", new[] { "Theme_Id" });
            DropIndex("dbo.SegmentTacticCampaigns", new[] { "TacticCampaign_Id" });
            DropIndex("dbo.SegmentTacticCampaigns", new[] { "Segment_Id" });
            DropIndex("dbo.SegmentMasterCampaigns", new[] { "MasterCampaign_Id" });
            DropIndex("dbo.SegmentMasterCampaigns", new[] { "Segment_Id" });
            DropIndex("dbo.SegmentChildCampaigns", new[] { "ChildCampaign_Id" });
            DropIndex("dbo.SegmentChildCampaigns", new[] { "Segment_Id" });
            DropIndex("dbo.IndustryTacticCampaigns", new[] { "TacticCampaign_Id" });
            DropIndex("dbo.IndustryTacticCampaigns", new[] { "Industry_Id" });
            DropIndex("dbo.IndustryMasterCampaigns", new[] { "MasterCampaign_Id" });
            DropIndex("dbo.IndustryMasterCampaigns", new[] { "Industry_Id" });
            DropIndex("dbo.IndustryChildCampaigns", new[] { "ChildCampaign_Id" });
            DropIndex("dbo.IndustryChildCampaigns", new[] { "Industry_Id" });
            DropIndex("dbo.TacticCampaignGeographies", new[] { "Geography_Id" });
            DropIndex("dbo.TacticCampaignGeographies", new[] { "TacticCampaign_Id" });
            DropIndex("dbo.TacticCampaignBusinessLines", new[] { "BusinessLine_Id" });
            DropIndex("dbo.TacticCampaignBusinessLines", new[] { "TacticCampaign_Id" });
            DropIndex("dbo.TacticCampaignBusinessGroups", new[] { "BusinessGroup_Id" });
            DropIndex("dbo.TacticCampaignBusinessGroups", new[] { "TacticCampaign_Id" });
            DropIndex("dbo.GeographyMasterCampaigns", new[] { "MasterCampaign_Id" });
            DropIndex("dbo.GeographyMasterCampaigns", new[] { "Geography_Id" });
            DropIndex("dbo.GeographyChildCampaigns", new[] { "ChildCampaign_Id" });
            DropIndex("dbo.GeographyChildCampaigns", new[] { "Geography_Id" });
            DropIndex("dbo.MasterCampaignBusinessLines", new[] { "BusinessLine_Id" });
            DropIndex("dbo.MasterCampaignBusinessLines", new[] { "MasterCampaign_Id" });
            DropIndex("dbo.MasterCampaignBusinessGroups", new[] { "BusinessGroup_Id" });
            DropIndex("dbo.MasterCampaignBusinessGroups", new[] { "MasterCampaign_Id" });
            DropIndex("dbo.BusinessLineChildCampaigns", new[] { "ChildCampaign_Id" });
            DropIndex("dbo.BusinessLineChildCampaigns", new[] { "BusinessLine_Id" });
            DropIndex("dbo.ChildCampaignBusinessGroups", new[] { "BusinessGroup_Id" });
            DropIndex("dbo.ChildCampaignBusinessGroups", new[] { "ChildCampaign_Id" });
            DropIndex("dbo.TacticCampaigns", new[] { "ChildCampaigns_Id" });
            DropIndex("dbo.ChildCampaigns", new[] { "MasterCampaigns_Id" });
            DropColumn("dbo.TacticCampaigns", "ChildCampaigns_Id");
            DropColumn("dbo.ChildCampaigns", "MasterCampaigns_Id");
            DropTable("dbo.ThemeTacticCampaigns");
            DropTable("dbo.ThemeMasterCampaigns");
            DropTable("dbo.ThemeChildCampaigns");
            DropTable("dbo.SegmentTacticCampaigns");
            DropTable("dbo.SegmentMasterCampaigns");
            DropTable("dbo.SegmentChildCampaigns");
            DropTable("dbo.IndustryTacticCampaigns");
            DropTable("dbo.IndustryMasterCampaigns");
            DropTable("dbo.IndustryChildCampaigns");
            DropTable("dbo.TacticCampaignGeographies");
            DropTable("dbo.TacticCampaignBusinessLines");
            DropTable("dbo.TacticCampaignBusinessGroups");
            DropTable("dbo.GeographyMasterCampaigns");
            DropTable("dbo.GeographyChildCampaigns");
            DropTable("dbo.MasterCampaignBusinessLines");
            DropTable("dbo.MasterCampaignBusinessGroups");
            DropTable("dbo.BusinessLineChildCampaigns");
            DropTable("dbo.ChildCampaignBusinessGroups");
            CreateIndex("dbo.TacticCampaignChildCampaigns", "ChildCampaign_Id");
            CreateIndex("dbo.TacticCampaignChildCampaigns", "TacticCampaign_Id");
            CreateIndex("dbo.MasterCampaignChildCampaigns", "ChildCampaign_Id");
            CreateIndex("dbo.MasterCampaignChildCampaigns", "MasterCampaign_Id");
            CreateIndex("dbo.TacticCampaigns", "Themes_Id");
            CreateIndex("dbo.TacticCampaigns", "Segments_Id");
            CreateIndex("dbo.TacticCampaigns", "Industries_Id");
            CreateIndex("dbo.TacticCampaigns", "Geographys_Id");
            CreateIndex("dbo.TacticCampaigns", "BusinessLines_Id");
            CreateIndex("dbo.TacticCampaigns", "BusinessGroups_Id");
            CreateIndex("dbo.MasterCampaigns", "Themes_Id");
            CreateIndex("dbo.MasterCampaigns", "Segments_Id");
            CreateIndex("dbo.MasterCampaigns", "Industries_Id");
            CreateIndex("dbo.MasterCampaigns", "Geographys_Id");
            CreateIndex("dbo.MasterCampaigns", "BusinessLines_Id");
            CreateIndex("dbo.MasterCampaigns", "BusinessGroups_Id");
            CreateIndex("dbo.ChildCampaigns", "Themes_Id");
            CreateIndex("dbo.ChildCampaigns", "Segments_Id");
            CreateIndex("dbo.ChildCampaigns", "Industries_Id");
            CreateIndex("dbo.ChildCampaigns", "Geographys_Id");
            CreateIndex("dbo.ChildCampaigns", "BusinessLines_Id");
            CreateIndex("dbo.ChildCampaigns", "BusinessGroups_Id");
            AddForeignKey("dbo.ChildCampaigns", "Themes_Id", "dbo.Themes", "Id");
            AddForeignKey("dbo.TacticCampaigns", "Themes_Id", "dbo.Themes", "Id");
            AddForeignKey("dbo.TacticCampaigns", "Segments_Id", "dbo.Segments", "Id");
            AddForeignKey("dbo.TacticCampaigns", "Industries_Id", "dbo.Industries", "Id");
            AddForeignKey("dbo.TacticCampaigns", "Geographys_Id", "dbo.Geographies", "Id");
            AddForeignKey("dbo.TacticCampaignChildCampaigns", "ChildCampaign_Id", "dbo.ChildCampaigns", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TacticCampaignChildCampaigns", "TacticCampaign_Id", "dbo.TacticCampaigns", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TacticCampaigns", "BusinessLines_Id", "dbo.BusinessLines", "Id");
            AddForeignKey("dbo.TacticCampaigns", "BusinessGroups_Id", "dbo.BusinessGroups", "Id");
            AddForeignKey("dbo.ChildCampaigns", "Segments_Id", "dbo.Segments", "Id");
            AddForeignKey("dbo.MasterCampaigns", "Themes_Id", "dbo.Themes", "Id");
            AddForeignKey("dbo.MasterCampaigns", "Segments_Id", "dbo.Segments", "Id");
            AddForeignKey("dbo.MasterCampaigns", "Industries_Id", "dbo.Industries", "Id");
            AddForeignKey("dbo.MasterCampaigns", "Geographys_Id", "dbo.Geographies", "Id");
            AddForeignKey("dbo.MasterCampaignChildCampaigns", "ChildCampaign_Id", "dbo.ChildCampaigns", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MasterCampaignChildCampaigns", "MasterCampaign_Id", "dbo.MasterCampaigns", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MasterCampaigns", "BusinessLines_Id", "dbo.BusinessLines", "Id");
            AddForeignKey("dbo.MasterCampaigns", "BusinessGroups_Id", "dbo.BusinessGroups", "Id");
            AddForeignKey("dbo.ChildCampaigns", "Industries_Id", "dbo.Industries", "Id");
            AddForeignKey("dbo.ChildCampaigns", "Geographys_Id", "dbo.Geographies", "Id");
            AddForeignKey("dbo.ChildCampaigns", "BusinessLines_Id", "dbo.BusinessLines", "Id");
            AddForeignKey("dbo.ChildCampaigns", "BusinessGroups_Id", "dbo.BusinessGroups", "Id");
        }
    }
}
