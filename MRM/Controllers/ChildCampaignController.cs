using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.Business.Services;
using MRM.Database.Model;
using MRM.Database.GenericRepository;
using MRM.ViewModel;
namespace MRM.Controllers
{
    [AllowAnonymous]
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

        //public ActionResult ChildCampaign()
        //{
        //    ChildCampaignViewModel Childvm = new ChildCampaignViewModel();
        //    Childvm.MasterViewModels = _masterCampaignServices.GetMasterCampaign();

        //    Childvm.BusinessGroupViewModels = (new BusinessGroup[] { new BusinessGroup() });
        //    Childvm.SegmentViewModels = (new Segment[] { new Segment() });
        //    Childvm.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
        //    Childvm.ThemeViewModels = (new Theme[] { new Theme() });
        //    Childvm.GeographyViewModels = (new Geography[] { new Geography() });
        //    Childvm.IndustryViewModels = (new Industry[] { new Industry() });

        //    return View(Childvm);
        //}

        [HttpPost]
        public ActionResult Save(ChildCampaignViewModel model, string button)
        {
            //  if (Session["UserInfo"] == null) { return RedirectToAction("Index", "Home"); }

            if (button == "Submit")
            {

            }
            else if (button == "Delete")
            { }
            else
            {

            }


            bool result;
            result=  _childCampaignServices.CreateChildCampaign(model);
            if (result == true)
            {
                return RedirectToAction("ChildList", "ChildList");
            }
            else
            {
                return RedirectToAction("ChildCampaign", "ChildCampaign");
            }
            // return Json("saved!", JsonRequestBehavior.AllowGet);
        }


        public ActionResult ChildCampaign(int[] BusinessGroups_Id, int[] Segments_Id, int id = 0)
        {
            ChildCampaignViewModel Childvm = new ChildCampaignViewModel();

            if (id == 0)
            {
                Childvm.MasterViewModels = _masterCampaignServices.GetMasterCampaign();

                Childvm.BusinessGroupViewModels = (new BusinessGroup[] { new BusinessGroup() });
                Childvm.SegmentViewModels = (new Segment[] { new Segment() });
                Childvm.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
                Childvm.ThemeViewModels = (new Theme[] { new Theme() });
                Childvm.GeographyViewModels = (new Geography[] { new Geography() });
                Childvm.IndustryViewModels = (new Industry[] { new Industry() });

                return View(Childvm);
            }
            Childvm.MasterViewModels = _masterCampaignServices.GetMasterCampaign();
             

            List<MasterCampaign> lst = _masterCampaignServices.GetMasterCampaignById(new MasterCampaignViewModel { Id = id });            
            foreach (var item in lst)
            {
                Childvm.IndustryViewModels = item.Industries;
                Childvm.BusinessGroupViewModels = item.BusinessGroups;
                Childvm.BusinessLineViewModels = item.BusinessLines;
                Childvm.SegmentViewModels = item.Segments;
                Childvm.ThemeViewModels = item.Themes;
                Childvm.GeographyViewModels = item.Geographys;
                Childvm.MasterCampaignId = id;
            }


            if (BusinessGroups_Id == null)
            {
                Childvm.BusinessGroupViewModels = _businessgroupService.GetBG();
                Childvm.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
                Childvm.SegmentViewModels = _segmentService.GetSegment();
                Childvm.IndustryViewModels = (new Industry[] { new Industry() });
                Childvm.GeographyViewModels = _geographyService.GetGeography();
                Childvm.ThemeViewModels = _themeService.GetTheme();
                return View(Childvm);
            }

            if (BusinessGroups_Id != null)
            {
                List<BusinessLine> businesslist = _businesslineService.GetBusinessLineByBGId(BusinessGroups_Id);
                Childvm.BusinessGroupViewModels = _businessgroupService.GetBG();
                Childvm.BusinessGroups_Id = BusinessGroups_Id;
                Childvm.BusinessLineViewModels = businesslist;
                Childvm.SegmentViewModels = _segmentService.GetSegment();
                if (Segments_Id == null)
                    Childvm.IndustryViewModels = (new Industry[] { new Industry() });
                else
                {
                    List<Industry> Industrylst = _industryService.GetIndustryBySegmentId(Segments_Id);
                    Childvm.IndustryViewModels = Industrylst;
                }
                Childvm.GeographyViewModels = _geographyService.GetGeography();
                Childvm.ThemeViewModels = _themeService.GetTheme();
                return View(Childvm);
            }
            else if (Segments_Id != null)
            {
                Childvm.BusinessGroupViewModels = _businessgroupService.GetBG();
                if (BusinessGroups_Id == null)
                    Childvm.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
                else
                {
                    List<BusinessLine> businesslist = _businesslineService.GetBusinessLineByBGId(BusinessGroups_Id);
                    Childvm.BusinessLineViewModels = businesslist;
                }
                Childvm.SegmentViewModels = _segmentService.GetSegment();
                Childvm.Segments_Id = Segments_Id;
                List<Industry> Industrylst = _industryService.GetIndustryBySegmentId(Segments_Id);
                Childvm.IndustryViewModels = Industrylst;
                Childvm.GeographyViewModels = _geographyService.GetGeography();
                Childvm.ThemeViewModels = _themeService.GetTheme();
                return View(Childvm);
            }

            return View(Childvm);
        }
    }
}