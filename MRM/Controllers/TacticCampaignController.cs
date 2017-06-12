using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.Database.Model;
using MRM.Model;

namespace MRM.Controllers
{
    public class TacticCampaignController : Controller
    {
        TacticCampaignVM TC = new TacticCampaignVM();
        // GET: TacticCampaign
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TacticCampaign()
        {
            TC.BusinessGroupsItems = DropdownDummy.GetBusinessGroupsItems();
            TC.BusinessLineItems = DropdownDummy.GetBusinessLine();
            TC.SegmentItems = DropdownDummy.GetSegmentItems();
            TC.ThemeItems = DropdownDummy.GetThemeItems();
            TC.GeographyItems = DropdownDummy.GetGeographyItems();
            TC.IndustryItems = DropdownDummy.GetIndustryItems();
            TC.ChildCamPaignItems = DropdownDummy.GetChildCamPaignItems();
            return View(TC);
        }
    }
}