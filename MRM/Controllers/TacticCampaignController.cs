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
            TC.BusinessGroups = DropdownDummy.GetBusinessGroupsItems();
            TC.BusinessLines = DropdownDummy.GetBusinessLine();
            TC.Segments = DropdownDummy.GetSegmentItems();
            TC.Themes = DropdownDummy.GetThemeItems();
            TC.Geographys = DropdownDummy.GetGeographyItems();
            TC.Industries = DropdownDummy.GetIndustryItems();
            TC.ChildCamPaigns = DropdownDummy.GetChildCamPaignItems();
            return View(TC);
        }

        public JsonResult Save(TacticCampaignVM MC, FormCollection form)
        {
            string BusinessGroups = form["BusinessGroups"].ToString();
            string BusinessLines = form["BusinessLines"].ToString();
            string Segments = form["Segments"].ToString();
            string Industries = form["Industries"].ToString();
            string Themes = form["Themes"].ToString();
            string Geographys = form["Geographys"].ToString();
            string ChildCamPaigns = form["ChildCamPaigns"].ToString();



            return Json("saved!", JsonRequestBehavior.AllowGet);
        }
    }
}