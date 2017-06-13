using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.Database.Model;
using MRM.Model;
using MRM.Database.GenericRepository;

namespace MRM.Controllers
{
    public class MasterCampaignController : Controller
    {
         private IGenericRepository<MasterCampaign> _repo;
         MasterCampaignVM MC = new MasterCampaignVM();

        //public MasterCampaignController(IGenericRepository<MasterCampaign> repo)
        //{
        //    _repo = repo;
        //}
        // GET: CampaignForm
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MasterCampaign()
        {
            MC.BusinessGroups = DropdownDummy.GetBusinessGroupsItems();
            MC.BusinessLines = DropdownDummy.GetBusinessLine();
            MC.Segments = DropdownDummy.GetSegmentItems();
            MC.Themes = DropdownDummy.GetThemeItems();
            MC.Geographys = DropdownDummy.GetGeographyItems();
            MC.Industries = DropdownDummy.GetIndustryItems();
            return View(MC);
        }

        public JsonResult Save(MasterCampaignVM MC, FormCollection form)
        {
            int BusinessGroups = Convert.ToInt32(form["BusinessGroups"].ToString());
            int BusinessLineItems = Convert.ToInt32(form["BusinessLines"].ToString());
            int SegmentItems = Convert.ToInt32(form["Segments"].ToString());
            int IndustryItems = Convert.ToInt32(form["Industries"].ToString());
            int ThemeItems = Convert.ToInt32(form["Themes"].ToString());
            int GeographyItems = Convert.ToInt32(form["Geographys"].ToString());

           // _repo.Insert(MC);
            return Json("saved!", JsonRequestBehavior.AllowGet);
        }

    }
}