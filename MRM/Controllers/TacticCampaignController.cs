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
    public class TacticCampaignController : Controller
    {
        private IndustryServices _industryService = null;
        private BusinessGroupServices _businessgroupService = null;
        private BusinessLineServices _businesslineService = null;
        private SegmentServices _segmentService = null;
        private GeographyServices _geographyService = null;
        private ThemeServices _themeService = null;
        private TacticCampaignServices _tacticCampaignServices = null;
        private ChildCampaignServices _childCampaignServices = null;

        private IGenericRepository<TacticCampaignViewModel> _repo;

        TacticCampaignViewModel Tacticvm = new TacticCampaignViewModel();
        public TacticCampaignController()
        {
            // _repo = repo;
            _industryService = new IndustryServices();
            _businessgroupService = new BusinessGroupServices();
            _businesslineService = new BusinessLineServices();
            _segmentService = new SegmentServices();
            _geographyService = new GeographyServices();
            _themeService = new ThemeServices();
            _tacticCampaignServices = new TacticCampaignServices();
            _childCampaignServices = new ChildCampaignServices();
        }
        // GET: TacticCampaign
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TacticCampaign()
        {
            Tacticvm.IndustryViewModels = _industryService.GetIndustry();
            Tacticvm.BusinessGroupViewModels = _businessgroupService.GetBG();
            Tacticvm.BusinessLineViewModels = _businesslineService.GetBusinessLine();
            Tacticvm.SegmentViewModels = _segmentService.GetSegment();
            Tacticvm.GeographyViewModels = _geographyService.GetGeography();
            Tacticvm.ThemeViewModels = _themeService.GetTheme();
            Tacticvm.ChildCampaignViewModels = _childCampaignServices.GetChildCampaign();
            return View(Tacticvm);
        }

        public JsonResult Save()
        {
            //_tacticCampaignServices.CreateTacticCampaign(model);
            return Json("saved!", JsonRequestBehavior.AllowGet);
        }
    }
}