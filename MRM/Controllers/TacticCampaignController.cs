using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.ViewModel;
using MRM.Business.Services;
using MRM.Database.Model;
using MRM.Database.GenericRepository;

namespace MRM.Controllers
{
    [AllowAnonymous]
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


        //public ActionResult TacticCampaign()
        //{
        //  //  if (Session["UserInfo"] == null) { return RedirectToAction("Index", "Home"); }
        //    Tacticvm.IndustryViewModels = _industryService.GetIndustry();
        //    Tacticvm.BusinessGroupViewModels = _businessgroupService.GetBG();
        //    Tacticvm.BusinessLineViewModels = _businesslineService.GetBusinessLine();
        //    Tacticvm.SegmentViewModels = _segmentService.GetSegment();
        //    Tacticvm.GeographyViewModels = _geographyService.GetGeography();
        //    Tacticvm.ThemeViewModels = _themeService.GetTheme();
        //    Tacticvm.ChildCampaignViewModels = _childCampaignServices.GetChildCampaign();
        //    return View(Tacticvm);
        //}


        public ActionResult TacticCampaign(int[] BusinessGroups_Id, int[] Segments_Id,int id = 0)
        {
            TacticCampaignViewModel tacticvm = new TacticCampaignViewModel();

            if (id == 0)
            {
                tacticvm.ChildCampaignViewModels = _childCampaignServices.GetChildCampaign();

                tacticvm.BusinessGroupViewModels = (new BusinessGroup[] { new BusinessGroup() });
                tacticvm.SegmentViewModels = (new Segment[] { new Segment() });
                tacticvm.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
                tacticvm.ThemeViewModels = (new Theme[] { new Theme() });
                tacticvm.GeographyViewModels = (new Geography[] { new Geography() });
                tacticvm.IndustryViewModels = (new Industry[] { new Industry() });

                return View(tacticvm);
            }

            tacticvm.ChildCampaignViewModels = _childCampaignServices.GetChildCampaign();
            List<ChildCampaign> lst = _childCampaignServices.GetChildCampaignById(new ChildCampaignViewModel { Id = id });
            foreach (var item in lst)
            {
                tacticvm.IndustryViewModels = item.Industries;
                tacticvm.BusinessGroupViewModels = item.BusinessGroups;
                tacticvm.BusinessLineViewModels = item.BusinessLines;
                tacticvm.SegmentViewModels = item.Segments;
                tacticvm.ThemeViewModels = item.Themes;
                tacticvm.GeographyViewModels = item.Geographys;
            }

            if (BusinessGroups_Id == null)
            {
                tacticvm.BusinessGroupViewModels = _businessgroupService.GetBG();
                tacticvm.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
                tacticvm.SegmentViewModels = _segmentService.GetSegment();
                tacticvm.IndustryViewModels = (new Industry[] { new Industry() });
                tacticvm.GeographyViewModels = _geographyService.GetGeography();
                tacticvm.ThemeViewModels = _themeService.GetTheme();
                return View(tacticvm);
            }

            if (BusinessGroups_Id != null)
            {
                List<BusinessLine> businesslist = _businesslineService.GetBusinessLineByBGId(BusinessGroups_Id);
                tacticvm.BusinessGroupViewModels = _businessgroupService.GetBG();
                tacticvm.BusinessGroups_Id = BusinessGroups_Id;
                tacticvm.BusinessLineViewModels = businesslist;
                tacticvm.SegmentViewModels = _segmentService.GetSegment();
                if (Segments_Id == null)
                    tacticvm.IndustryViewModels = (new Industry[] { new Industry() });
                else
                {
                    List<Industry> Industrylst = _industryService.GetIndustryBySegmentId(Segments_Id);
                    tacticvm.IndustryViewModels = Industrylst;
                }
                tacticvm.GeographyViewModels = _geographyService.GetGeography();
                tacticvm.ThemeViewModels = _themeService.GetTheme();
                return View(tacticvm);
            }
            else if (Segments_Id != null)
            {
                tacticvm.BusinessGroupViewModels = _businessgroupService.GetBG();
                if (BusinessGroups_Id == null)
                    tacticvm.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
                else
                {
                    List<BusinessLine> businesslist = _businesslineService.GetBusinessLineByBGId(BusinessGroups_Id);
                    tacticvm.BusinessLineViewModels = businesslist;
                }
                tacticvm.SegmentViewModels = _segmentService.GetSegment();
                tacticvm.Segments_Id = Segments_Id;
                List<Industry> Industrylst = _industryService.GetIndustryBySegmentId(Segments_Id);
                tacticvm.IndustryViewModels = Industrylst;
                tacticvm.GeographyViewModels = _geographyService.GetGeography();
                tacticvm.ThemeViewModels = _themeService.GetTheme();
                return View(tacticvm);
            }
            
            return View(tacticvm);
        }

        public ActionResult Save(TacticCampaignViewModel model, string button)
        {
            //  if (Session["UserInfo"] == null) { return RedirectToAction("Index", "Home");

            if (button == "Submit")
            {

            }
            else if (button == "Delete")
            { }
            else
            {

            }


            bool result;

            result = _tacticCampaignServices.CreateTacticCampaign(model);
            if (result == true)
            {
                return RedirectToAction("TacticList", "TacticList");
            }
            else
            {
                return RedirectToAction("TacticCampaign", "TacticCampaign");
            }
            return Json("saved!", JsonRequestBehavior.AllowGet);
        }
    }
}