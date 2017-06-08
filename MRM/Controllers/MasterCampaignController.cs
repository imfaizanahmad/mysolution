using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.Database.Model;

namespace MRM.Controllers
{
    public class MasterCampaignController : Controller
    {
        MasterCampaignVM MC = new MasterCampaignVM();
        // GET: CampaignForm
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MasterCampaign()
        {
            MC.BusinessGroupsItems = GetBusinessGroupsItems();
            MC.BusinessLineItems = GetBusinessLine();
            MC.SegmentItems = GetSegmentItems();
            MC.ThemeItems = GetThemeItems();
            MC.GeographyItems = GetGeographyItems();
            MC.IndustryItems = GetIndustryItems();
            return View(MC);
        }

        private List<MasterCampaignVM.BusinessGroup> GetBusinessGroupsItems()
        {
            // TODO: you could obviously fetch your categories from your DAL
            // instead of hardcoding them as shown in this example
            var BusinessGroupsItems = new List<MasterCampaignVM.BusinessGroup>();
            BusinessGroupsItems.Add(new MasterCampaignVM.BusinessGroup { ID = 1, Name = "BG 1" });
            BusinessGroupsItems.Add(new MasterCampaignVM.BusinessGroup { ID = 2, Name = "BG 2" });
            BusinessGroupsItems.Add(new MasterCampaignVM.BusinessGroup { ID = 3, Name = "BG 3" });
            return BusinessGroupsItems;
        }

        private List<MasterCampaignVM.BusinessLine> GetBusinessLine()
        {
            // TODO: you could obviously fetch your categories from your DAL
            // instead of hardcoding them as shown in this example
            var BusinessLineItems = new List<MasterCampaignVM.BusinessLine>();
            BusinessLineItems.Add(new MasterCampaignVM.BusinessLine { ID = 1, Name = "BG 1" });
            BusinessLineItems.Add(new MasterCampaignVM.BusinessLine { ID = 2, Name = "BG 2" });
            BusinessLineItems.Add(new MasterCampaignVM.BusinessLine { ID = 3, Name = "BG 3" });
            return BusinessLineItems;
        }

        private List<MasterCampaignVM.Geography> GetGeographyItems()
        {
            // TODO: you could obviously fetch your categories from your DAL
            // instead of hardcoding them as shown in this example
            var GeographyItems = new List<MasterCampaignVM.Geography>();
            GeographyItems.Add(new MasterCampaignVM.Geography { ID = 1, Name = "BG 1" });
            GeographyItems.Add(new MasterCampaignVM.Geography { ID = 2, Name = "BG 2" });
            GeographyItems.Add(new MasterCampaignVM.Geography { ID = 3, Name = "BG 3" });
            return GeographyItems;
        }
        private List<MasterCampaignVM.Industry> GetIndustryItems()
        {
            // TODO: you could obviously fetch your categories from your DAL
            // instead of hardcoding them as shown in this example
            var IndustryItems = new List<MasterCampaignVM.Industry>();
            IndustryItems.Add(new MasterCampaignVM.Industry { ID = 1, Name = "BG 1" });
            IndustryItems.Add(new MasterCampaignVM.Industry { ID = 2, Name = "BG 2" });
            IndustryItems.Add(new MasterCampaignVM.Industry { ID = 3, Name = "BG 3" });
            return IndustryItems;
        }
        private List<MasterCampaignVM.Segment> GetSegmentItems()
        {
            // TODO: you could obviously fetch your categories from your DAL
            // instead of hardcoding them as shown in this example
            var SegmentItems = new List<MasterCampaignVM.Segment>();
            SegmentItems.Add(new MasterCampaignVM.Segment { ID = 1, Name = "BG 1" });
            SegmentItems.Add(new MasterCampaignVM.Segment { ID = 2, Name = "BG 2" });
            SegmentItems.Add(new MasterCampaignVM.Segment { ID = 3, Name = "BG 3" });
            return SegmentItems;
        }
        private List<MasterCampaignVM.Theme> GetThemeItems()
        {
            // TODO: you could obviously fetch your categories from your DAL
            // instead of hardcoding them as shown in this example
            var ThemeItems = new List<MasterCampaignVM.Theme>();
            ThemeItems.Add(new MasterCampaignVM.Theme { ID = 1, Name = "BG 1" });
            ThemeItems.Add(new MasterCampaignVM.Theme { ID = 2, Name = "BG 2" });
            ThemeItems.Add(new MasterCampaignVM.Theme { ID = 3, Name = "BG 3" });
            return ThemeItems;
        }

    }
}