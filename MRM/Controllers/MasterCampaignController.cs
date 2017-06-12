using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.Database.Model;
using MRM.Model;

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
            MC.BusinessGroupsItems = DropdownDummy.GetBusinessGroupsItems();
            MC.BusinessLineItems = DropdownDummy.GetBusinessLine();
            MC.SegmentItems = DropdownDummy.GetSegmentItems();
            MC.ThemeItems = DropdownDummy.GetThemeItems();
            MC.GeographyItems = DropdownDummy.GetGeographyItems();
            MC.IndustryItems = DropdownDummy.GetIndustryItems();
            return View(MC);
        }


        [HttpPost]
        public JsonResult SaveMaster(string s)
        {
            return Json("chamara", JsonRequestBehavior.AllowGet);
        }

    }
}