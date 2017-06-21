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
    [AllowAnonymous]
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
            //if (Session["UserInfo"] == null) {return RedirectToAction("Index", "Home");}
            TempData["mastercount"] = "";
            var mastercount = _masterCampaignServices.GetMasterCampaign().Count();           
            return View();
        }

        public ActionResult MasterCampaign()
        {
            //   if (Session["UserInfo"] == null) { return RedirectToAction("Index", "Home"); }
            mcvm.BusinessGroupViewModels = _businessgroupService.GetBG();
            mcvm.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
            mcvm.SegmentViewModels = _segmentService.GetSegment();
            mcvm.IndustryViewModels = (new Industry[] { new Industry() });
            mcvm.GeographyViewModels = _geographyService.GetGeography();
            mcvm.ThemeViewModels = _themeService.GetTheme();
            return View(mcvm);
        }

        [HttpPost]
        public ActionResult Save(MasterCampaignViewModel model,string button)
        {
            // if (Session["UserInfo"] == null) { return RedirectToAction("Index", "Home"); }


            if (button == "Submit")
            {

            }
            else if (button == "Delete")
            { }
            else 
            {
 
            }


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

        public ActionResult BusinessLine(string id)
        {

            if (id == null)
            {
                mcvm.BusinessGroupViewModels = _businessgroupService.GetBG();
                mcvm.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
                mcvm.SegmentViewModels = _segmentService.GetSegment();
                mcvm.IndustryViewModels = (new Industry[] { new Industry() });
                mcvm.GeographyViewModels = _geographyService.GetGeography();
                mcvm.ThemeViewModels = _themeService.GetTheme();
                return View(mcvm);
            }

            List<BusinessLine> lst = _businesslineService.GetBusinessLineByBGId(id);
            mcvm.BusinessLineViewModels = lst;
            return View(mcvm);
        }

        public ActionResult Industry(string id = null)
        {
            if (id == null)
            {
                mcvm.BusinessGroupViewModels = _businessgroupService.GetBG();
                mcvm.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
                mcvm.SegmentViewModels = _segmentService.GetSegment();
                mcvm.IndustryViewModels = (new Industry[] { new Industry() });
                mcvm.GeographyViewModels = _geographyService.GetGeography();
                mcvm.ThemeViewModels = _themeService.GetTheme();
                return View(mcvm);
            }

            List<Industry> lst = _industryService.GetIndustryBySegmentId(id);
            mcvm.IndustryViewModels = lst;
            return View(mcvm);
        }

    }
}