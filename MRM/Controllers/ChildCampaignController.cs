using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.Database.Model;
using MRM.Model;

namespace MRM.Controllers
{
    public class ChildCampaignController : Controller
    {
        ChildCampaignVM CC = new ChildCampaignVM();
        // GET: ChildCampaign
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChildCampaign()
        {
            CC.BusinessGroupsItems = DropdownDummy.GetBusinessGroupsItems();
            CC.BusinessLineItems = DropdownDummy.GetBusinessLine();
            CC.SegmentItems = DropdownDummy.GetSegmentItems();
            CC.ThemeItems = DropdownDummy.GetThemeItems();
            CC.GeographyItems = DropdownDummy.GetGeographyItems();
            CC.IndustryItems = DropdownDummy.GetIndustryItems();
            CC.MasterCamPaignItems = DropdownDummy.GetMasterCamPaignItems();
            return View(CC);
        }
    }
}