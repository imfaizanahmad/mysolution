using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.Business.Services;
using MRM.Database.Model;
using MRM.Database.GenericRepository;
using Newtonsoft.Json;
using MRM.ViewModel;

namespace MRM.Controllers
{

    public class MasterCampaignController : Controller
    {
        private IndustryServices _industryService = null;
        private BusinessGroupServices _businessgroupService = null;
        private BusinessLineServices _businesslineService = null;
        private SegmentServices _segmentService = null;
        private GeographyServices _geographyService = null;
        private ThemeServices _themeService = null;
        private MasterCampaignServices _masterCampaignServices = null;

        MasterCampaignViewModel mcvm = new MasterCampaignViewModel();

        public MasterCampaignController()
        {
            _industryService = new IndustryServices();
            _businessgroupService = new BusinessGroupServices();
            _businesslineService = new BusinessLineServices();
            _segmentService = new SegmentServices();
            _geographyService = new GeographyServices();
            _themeService = new ThemeServices();
            _masterCampaignServices = new MasterCampaignServices();
        }

        // GET: CampaignForm
        public ActionResult Index()
        {
          //  if (Session["UserInfo"] == null) {return RedirectToAction("Index", "Home");}
            TempData["mastercount"] = "";
            var mastercount = _masterCampaignServices.GetMasterCampaign().Count();
            if (mastercount <= 0)
            {
                TempData["mastercount"] = "There is no master campaign available,Create master campaign first!";
                return RedirectToAction("Index", "MasterCampaign");
            }
           
            return View();
        }

        public ActionResult MasterCampaign()
        {
         //   if (Session["UserInfo"] == null) { return RedirectToAction("Index", "Home"); }
            mcvm.IndustryViewModels = _industryService.GetIndustry();
            mcvm.BusinessGroupViewModels = _businessgroupService.GetBG();
            mcvm.BusinessLineViewModels = _businesslineService.GetBusinessLine();
            mcvm.SegmentViewModels = _segmentService.GetSegment();
            mcvm.GeographyViewModels = _geographyService.GetGeography();
            mcvm.ThemeViewModels = _themeService.GetTheme();
            return View(mcvm);
        }

        [HttpPost]
        public ActionResult Save(MasterCampaignViewModel model)
        {
           // if (Session["UserInfo"] == null) { return RedirectToAction("Index", "Home"); }

            bool result;
            result =  _masterCampaignServices.CreateMasterCampaign(model);
            if (result == true)
            {
                return RedirectToAction("MasterList", "MasterList");
            }
            else
            {
                return RedirectToAction("MasterCampaign", "MasterCampaign");
            }

        }
        
    }
}