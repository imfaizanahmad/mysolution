using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.Model;
using MRM.Business.Services;
using MRM.Database.Model;
using MRM.Database.GenericRepository;

namespace MRM.Controllers
{
    public class ChildCampaignController : Controller
    {
        private IndustryServices _industryService = null;
        private BusinessGroupServices _businessgroupService = null;
        private BusinessLineServices _businesslineService = null;
        private SegmentServices _segmentService = null;
        private GeographyServices _geographyService = null;
        private ThemeServices _themeService = null;
        private MasterCampaignServices _masterCampaignServices = null;

        // private IGenericRepository<ChildCampaignViewModel> _repo;

        ChildCampaignViewModel Childvm = new ChildCampaignViewModel();

        public ChildCampaignController()
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

        // GET: ChildCampaign
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChildCampaign()
        {
            Childvm.IndustryViewModels = _industryService.GetIndustry();
            Childvm.BusinessGroupViewModels = _businessgroupService.GetBG();
            Childvm.BusinessLineViewModels = _businesslineService.GetBusinessLine();
            Childvm.SegmentViewModels = _segmentService.GetSegment();
            Childvm.GeographyViewModels = _geographyService.GetGeography();
            Childvm.ThemeViewModels = _themeService.GetTheme();
            Childvm.MasterViewModels = _masterCampaignServices.GetMasterCampaign();
            return View(Childvm);
        }


        public JsonResult Save(ChildCampaignViewModel model,FormCollection form)
        {

           // _childCampaignServices.CreateChildCampaign(model);
            return Json("saved!", JsonRequestBehavior.AllowGet);
        }
    }
}