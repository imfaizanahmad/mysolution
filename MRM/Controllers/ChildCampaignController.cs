using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.Business.Services;
using MRM.Database.Model;
using MRM.Database.GenericRepository;
using MRM.ViewModel;
using System.Collections;

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

            //Bind Campaign Type List
            var CampaignTypelst = new List<CampaignTypes> { new CampaignTypes() { Id = 0, Name = "BG Led" },
                new CampaignTypes() { Id = 1, Name = "GEPS" } };
            model.campaignTypeViewModels = CampaignTypelst;
            //
            foreach(var i in CampaignTypelst)
            {
                if (i.Id == model.CampaignType)
                {
                    model.CampaignType = i.Id;
                }
            }
           

            model.MasterViewModels = _masterCampaignServices.GetMasterCampaign();

          
            //Load DDl values//////////////////////
            List<MasterCampaign> lst = _masterCampaignServices.GetMasterCampaignById(new MasterCampaignViewModel { Id = model.MasterCampaignId });
            foreach (var item in lst)
            {
                model.IndustryViewModels = item.Industries;
                model.BusinessGroupViewModels = item.BusinessGroups;
                model.BusinessLineViewModels = item.BusinessLines;
                model.SegmentViewModels = item.Segments;
                model.ThemeViewModels = item.Themes;
                model.GeographyViewModels = item.Geographys;
            }

            if (model.BusinessGroups_Id == null)
            {
                model.BusinessGroupViewModels = _businessgroupService.GetBG();
                model.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
                model.SegmentViewModels = _segmentService.GetSegment();
                model.IndustryViewModels = (new Industry[] { new Industry() });
                model.GeographyViewModels = _geographyService.GetGeography();
                model.ThemeViewModels = _themeService.GetTheme();
                return View("ChildCampaign",model);
            }

            if (model.BusinessGroups_Id != null)
            {
                List<BusinessLine> businesslist = _businesslineService.GetBusinessLineByBGId(model.BusinessGroups_Id);
                model.BusinessGroupViewModels = _businessgroupService.GetBG();
                model.BusinessLineViewModels = businesslist;
                model.SegmentViewModels = _segmentService.GetSegment();
                if (model.Segments_Id == null)
                    model.IndustryViewModels = (new Industry[] { new Industry() });
                else
                {
                    List<Industry> Industrylst = _industryService.GetIndustryBySegmentId(model.Segments_Id);
                    model.IndustryViewModels = Industrylst;
                }
                model.GeographyViewModels = _geographyService.GetGeography();
                model.ThemeViewModels = _themeService.GetTheme();
                return View("ChildCampaign", model);
            }
            else if (model.Segments_Id != null)
            {
                model.BusinessGroupViewModels = _businessgroupService.GetBG();
                if (model.BusinessGroups_Id == null)
                    model.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
                else
                {
                    List<BusinessLine> businesslist = _businesslineService.GetBusinessLineByBGId(model.BusinessGroups_Id);
                    model.BusinessLineViewModels = businesslist;
                }
                model.SegmentViewModels = _segmentService.GetSegment();
                model.Segments_Id = model.Segments_Id;
                List<Industry> Industrylst = _industryService.GetIndustryBySegmentId(model.Segments_Id);
                model.IndustryViewModels = Industrylst;
                model.GeographyViewModels = _geographyService.GetGeography();
                model.ThemeViewModels = _themeService.GetTheme();
                return View("ChildCampaign",model);
            }

            ////////////////////////////
            

            bool result;
            if (isValid(model))
            {
                result = _childCampaignServices.CreateChildCampaign(model);
                if (result == true)
                {
                    return RedirectToAction("ChildList", "ChildList");
                }
                else
                {
                    return RedirectToAction("ChildCampaign", "ChildCampaign");
                }
            }
           
            return View("ChildCampaign",model);
            // return Json("saved!", JsonRequestBehavior.AllowGet);
        }

        private bool isValid(ChildCampaignViewModel model)
        {
            int errorCounter = 0;
            int campaignTypeBGLed = 0;
            if (model.MasterCampaignId == 0) errorCounter++;
            if (model.BusinessGroups_Id == null) errorCounter++;
            if (model.BusinessLines_Id == null) errorCounter++;
            if (model.Segments_Id == null) errorCounter++;
            if (model.CampaignType!=0 || model.CampaignType !=1) errorCounter++;
            if (Convert.ToInt32(model.CampaignType) == 1 && model.Industries_Id == null) errorCounter++;
            if (model.StartDate == "") errorCounter++;
            if (model.EndDate == "") errorCounter++;
            if (Convert.ToDateTime(model.StartDate) > Convert.ToDateTime(model.EndDate)) errorCounter++;
            if (model.Name == "") errorCounter++;
            if (model.CampaignDescription == "") errorCounter++;
            if (model.Budget == "") errorCounter++;
            //if (model.Status == "") errorCounter++;
            if (model.CampaignType == campaignTypeBGLed) 
            {
                var bgArr = model.BusinessGroups_Id;
                if (bgArr != null && bgArr.Length != 1) errorCounter++;
            }
            else
            {
                var segArr = model.Segments_Id;
                if (segArr != null && segArr.Length != 1) errorCounter++;
            }

            return errorCounter == 0;
        }

        public ActionResult ChildCampaign(int[] BusinessGroups_Id, int[] Segments_Id, string CTVal, int id = 0)
        {
            ChildCampaignViewModel Childvm = new ChildCampaignViewModel();
            //Bind Campaign Type List
            var CampaignTypelst = new List<CampaignTypes> { new CampaignTypes() { Id = 0, Name = "BG Led" },
                new CampaignTypes() { Id = 1, Name = "GEPS" } };
            Childvm.campaignTypeViewModels = CampaignTypelst;
            //


            if (id == 0)
            {
                Childvm.MasterViewModels = _masterCampaignServices.GetMasterCampaign();
                Childvm.BusinessGroupViewModels = (new BusinessGroup[] { new BusinessGroup() });
                //Childvm.SegmentViewModels = (new Segment[] { new Segment() });
                //Childvm.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
                //Childvm.ThemeViewModels = (new Theme[] { new Theme() });
                //Childvm.GeographyViewModels = (new Geography[] { new Geography() });
                //Childvm.IndustryViewModels = (new Industry[] { new Industry() });

                return View(Childvm);
            }
            Childvm.MasterViewModels = _masterCampaignServices.GetMasterCampaign();

            // List<CampaignTypes> cmpgntype = new List<CampaignTypes> { new cmpgntype { name = "hg", id = 0 }, new CampaignTypes { name = "hg", id = 0 } };
            
            return View(Childvm);
        }

        
    }
}