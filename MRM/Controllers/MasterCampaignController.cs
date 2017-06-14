using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.Model;
using MRM.Business.Services;
using MRM.Database.Model;
using MRM.Database.GenericRepository;
using Newtonsoft.Json;

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

        private IGenericRepository<MasterCampaignViewModel> _repo;

        MasterCampaignViewModel mcvm = new MasterCampaignViewModel();
        public MasterCampaignController()
        {
            // _repo = repo;
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
            return View();
        }

        public ActionResult MasterCampaign()
        {
            mcvm.IndustryViewModels = _industryService.GetIndustry();
            mcvm.BusinessGroupViewModels = _businessgroupService.GetBG();
            mcvm.BusinessLineViewModels = _businesslineService.GetBusinessLine();
            mcvm.SegmentViewModels = _segmentService.GetSegment();
            mcvm.GeographyViewModels = _geographyService.GetGeography();
            mcvm.ThemeViewModels = _themeService.GetTheme();
            return View(mcvm);
        }

        public JsonResult Save(MasterCampaignViewModel model)
        {
           // var temp = form["BusinessGroupViewModels"];

            model.Status = 1;
            model.IsActive = 1;
            model.CreatedDate = DateTime.Now;
            //model.BusinessGroupViewModels = 1;
            
            _repo.Insert(model);
            return Json("saved!", JsonRequestBehavior.AllowGet);
        }

    }
}