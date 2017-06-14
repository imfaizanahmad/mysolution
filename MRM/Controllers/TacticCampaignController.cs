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

        TacticCampaignViewModel Tacticvm = new TacticCampaignViewModel();
        public TacticCampaignController()
        {
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

        public JsonResult Save(TacticCampaignViewModel model)
        {
            TacticCampaign mst = new TacticCampaign();
            mst.Name = model.Name;
            mst.TacticDescription = model.TacticDescription;
            mst.Industries = new Industry();
            mst.Industries.Id = model.Industries_Id;
            mst.BusinessGroups = new BusinessGroup();
            mst.BusinessGroups.Id = model.BusinessGroups_Id;
            mst.BusinessLines = new BusinessLine();
            mst.BusinessLines.Id = model.BusinessLines_Id;
            mst.Segments = new Segment();
            mst.Segments.Id = model.Segments_Id;
            mst.Themes = new Theme();
            mst.Themes.Id = model.Themes_Id;
            mst.Geographys = new Geography();
            mst.Geographys.Id = model.Geographys_Id;
            mst.StartDate = model.StartDate;
            mst.EndDate = model.EndDate;
            mst.Status = model.Status;
            _tacticCampaignServices.CreateTacticCampaign(mst);

            return Json("saved!", JsonRequestBehavior.AllowGet);
        }
    }
}