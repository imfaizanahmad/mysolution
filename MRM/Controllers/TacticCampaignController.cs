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

        public ActionResult Save(TacticCampaignViewModel model)
        {
            TacticCampaign mst = new TacticCampaign();
            mst.Industries = new Industry();
            mst.BusinessGroups = new BusinessGroup();
            mst.BusinessLines = new BusinessLine();
            mst.Segments = new Segment();
            mst.Themes = new Theme();
            mst.Geographys = new Geography();

            mst.Name = model.Name;
            mst.TacticDescription = model.TacticDescription;
            mst.Industries.Id = model.Industries_Id;
            mst.BusinessGroups.Id = model.BusinessGroups_Id;
            mst.BusinessLines.Id = model.BusinessLines_Id;
            mst.Segments.Id = model.Segments_Id;
            mst.Themes.Id = model.Themes_Id;
            mst.Geographys.Id = model.Geographys_Id;
            mst.StartDate = Convert.ToDateTime(model.StartDate);
            mst.EndDate = Convert.ToDateTime(model.EndDate);
            mst.Status = model.Status;
            mst.Year = model.Year;
            mst.CreatedBy = "user";
            bool result;

            result=  _tacticCampaignServices.CreateTacticCampaign(mst);
            if (result == true)
            {
                return RedirectToAction("Index", "MasterCampaign");
            }
            else
            {
                return RedirectToAction("TacticCampaign", "TacticCampaign");
            }
            //  return Json("saved!", JsonRequestBehavior.AllowGet);
        }
    }
}