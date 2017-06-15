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
        public ActionResult Save(MasterCampaignViewModel model)
        {
            MasterCampaign mst = new MasterCampaign();
            mst.Industries = new Industry();
            mst.BusinessGroups = new BusinessGroup();
            mst.BusinessLines = new BusinessLine();
            mst.Segments = new Segment();
            mst.Themes = new Theme();
            mst.Geographys = new Geography();


            mst.Name = model.Name;
            mst.CampaignDescription = model.CampaignDescription;
            mst.Industries.Id = model.Industries_Id;
            mst.BusinessGroups.Id = model.BusinessGroups_Id;
            mst.BusinessLines.Id = model.BusinessLines_Id;
            mst.Segments.Id = model.Segments_Id;
            mst.Themes.Id = model.Themes_Id;
            mst.Geographys.Id = model.Geographys_Id;
            mst.StartDate = Convert.ToDateTime(model.StartDate);
            mst.EndDate = Convert.ToDateTime(model.EndDate);
            mst.Status = model.Status;
            mst.CreatedBy = "user";

            bool result;
            result=  _masterCampaignServices.CreateMasterCampaign(mst);
            // return Json("saved!", JsonRequestBehavior.AllowGet);
            if (result == true)
            {
                
                return RedirectToAction("Index", "MasterCampaign");
            }
            else
            {
                return RedirectToAction("MasterCampaign", "MasterCampaign");
            }

        }

    }
}