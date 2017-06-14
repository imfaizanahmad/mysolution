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

        [HttpPost]
        public JsonResult Save(MasterCampaignViewModel model)
        {
            // var temp = form["BusinessGroupViewModels"];
            MasterCampaign mst = new MasterCampaign();
            mst.Name = model.Name;
            mst.CampaignDescription = model.CampaignDescription;
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
            _masterCampaignServices.CreateMasterCampaign(mst);

            return Json("saved!", JsonRequestBehavior.AllowGet);
        }

    }
}