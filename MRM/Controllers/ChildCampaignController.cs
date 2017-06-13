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
            CC.BusinessGroups = DropdownDummy.GetBusinessGroupsItems();
            CC.BusinessLines = DropdownDummy.GetBusinessLine();
            CC.Segments = DropdownDummy.GetSegmentItems();
            CC.Themes = DropdownDummy.GetThemeItems();
            CC.Geographys = DropdownDummy.GetGeographyItems();
            CC.Industries = DropdownDummy.GetIndustryItems();
            CC.MasterCamPaigns = DropdownDummy.GetMasterCamPaignItems();
            return View(CC);
        }


        public JsonResult Save(ChildCampaignVM MC, FormCollection form)
        {
            string MasterCamPaigns = form["MasterCamPaigns"].ToString();
            string BGLed = form["BGLed"].ToString();
            string BusinessGroups = form["BusinessGroups"].ToString();
            string BusinessLines = form["BusinessLines"].ToString();
            string Segments = form["Segments"].ToString();
            string Industries = form["Industries"].ToString();
            string Themes = form["Themes"].ToString();
            string Geographys = form["Geographys"].ToString();


            return Json("saved!", JsonRequestBehavior.AllowGet);
        }
    }
}