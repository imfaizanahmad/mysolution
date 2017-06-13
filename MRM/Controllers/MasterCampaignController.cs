using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.Model;
using MRM.Business.Services;
using MRM.Database.Model;

namespace MRM.Controllers
{
    public class MasterCampaignController : Controller
    {
        private IndustryServices _industryService = null;
        MasterCampaignViewModel mcvm = new MasterCampaignViewModel();
        public MasterCampaignController()
        {
            _industryService = new IndustryServices();
        }
        // GET: CampaignForm
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MasterCampaign()
        {
            mcvm.IndustryViewModels = _industryService.GetIndustry();
            return View();
        }

        public JsonResult Save()
        {

           // _repo.Insert(MC);
            return Json("saved!", JsonRequestBehavior.AllowGet);
        }

    }
}