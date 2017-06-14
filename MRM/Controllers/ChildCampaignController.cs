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
        private ChildCampaignServices _childCampaignServices = null;

        ChildCampaignViewModel Childvm = new ChildCampaignViewModel();

        public ChildCampaignController()
        {
            _industryService = new IndustryServices();
            _businessgroupService = new BusinessGroupServices();
            _businesslineService = new BusinessLineServices();
            _segmentService = new SegmentServices();
            _geographyService = new GeographyServices();
            _themeService = new ThemeServices();
            _masterCampaignServices = new MasterCampaignServices();
            _childCampaignServices = new ChildCampaignServices();
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

        public JsonResult Save(ChildCampaignViewModel model)
        {
            ChildCampaign mst = new ChildCampaign();
            mst.Name = model.Name;
            mst.CampaignDescription = model.CampaignDescription;
            mst.Budget = model.Budget;
            mst.Spend = model.Spend;
            mst.MarketingInfluenceLeads = model.MarketingInfluenceLeads;
            mst.MarketingGeneratedLeads = model.MarketingGeneratedLeads;
            mst.MarketingInfluenceOpportunity = model.MarketingInfluenceOpportunity;
            mst.MarketingGeneratedOpportunity = model.MarketingGeneratedOpportunity;
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
            _childCampaignServices.CreateChildCampaign(mst);

            return Json("saved!", JsonRequestBehavior.AllowGet);
        }
    }
}