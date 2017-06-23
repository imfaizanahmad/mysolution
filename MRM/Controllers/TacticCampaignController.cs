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
        private MasterCampaignServices _masterCampaignServices = null;

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

        private bool isValid(TacticCampaignViewModel model)
        {
            int errorCounter = 0;

            if (model.Id != 0)
            {

                if (model.StartDate == "") errorCounter++;
                if (model.EndDate == "") errorCounter++;
                if (Convert.ToDateTime(model.StartDate) > Convert.ToDateTime(model.EndDate)) errorCounter++;
                if (model.TacticDescription == "") errorCounter++;
            }
            else
            {

                if (model.MasterCampaign_Id == 0) errorCounter++;
                if (model.ChildCampaign_Id == 0) errorCounter++;
                if (model.BusinessGroups_Id == null) errorCounter++;
                if (model.BusinessLines_Id == null) errorCounter++;
                if (model.Segments_Id == null) errorCounter++;
                if (model.Vendor_Id == null) errorCounter++;

                if (model.StartDate == "") errorCounter++;
                if (model.EndDate == "") errorCounter++;
                if (Convert.ToDateTime(model.StartDate) > Convert.ToDateTime(model.EndDate)) errorCounter++;
                if (model.Name == "") errorCounter++;
                if (model.TacticDescription == "") errorCounter++;
            }

            //int campaignTypeBGLed = 0;
           
       

            return errorCounter == 0;
        }
        public ActionResult TacticCampaign(int Id = 0)
        {
            TacticCampaignViewModel tacticvm = new TacticCampaignViewModel();
            
            if (Id == 0)
            {
                tacticvm.VendorViewModels = _tacticCampaignServices.GetVendor();
                tacticvm.MasterViewModels = _masterCampaignServices.GetMasterCampaign();
                //  tacticvm.ChildCampaignViewModels = _childCampaignServices.GetChildCampaign();
                return View(tacticvm);
            }

            //created by Suraj
            if (Id != 0)
            {
                //Default Drop-down bind
                tacticvm.MasterViewModels = _masterCampaignServices.GetMasterCampaign();
                tacticvm.ChildCampaignViewModels = _childCampaignServices.GetChildCampaign();
                tacticvm.IndustryViewModels = _industryService.GetIndustry();
                tacticvm.BusinessGroupViewModels = _businessgroupService.GetBG();
                tacticvm.BusinessLineViewModels = _businesslineService.GetBusinessLine();
                tacticvm.SegmentViewModels = _segmentService.GetSegment();
                tacticvm.ThemeViewModels = _themeService.GetTheme();
                tacticvm.GeographyViewModels = _geographyService.GetGeography();
                List<TacticCampaign> tacticobjlist = _tacticCampaignServices.GetTacticCampaignById(new TacticCampaignViewModel { Id = Id });
                foreach (var item in tacticobjlist)
                {
                    //Selected Value bind in drop-down
                    //For Theme
                    int[] SelectedThemes = new int[item.Themes.Count];
                    for (int i = 0; i < item.Themes.Count; i++)
                    {
                        SelectedThemes[i] = item.Themes.ElementAt(i).Id;
                    }
                    tacticvm.Themes_Id = SelectedThemes;

                    //For BusinessGroups
                 
                    int[] SelectedBusinessGroup = new int[item.BusinessGroups.Count];
                    for (int i = 0; i < item.BusinessGroups.Count; i++)
                    {
                        SelectedBusinessGroup[i] = item.BusinessGroups.ElementAt(i).Id;
                    }
                    tacticvm.BusinessGroups_Id = SelectedBusinessGroup;
                    List<BusinessLine> businesslist = _businesslineService.GetBusinessLineByBGId(tacticvm.BusinessGroups_Id);
                    tacticvm.BusinessLineViewModels = businesslist;
                    //For BusinessLines
                    int[] SelectedBusinessLine = new int[item.BusinessLines.Count];
                    for (int i = 0; i < item.BusinessLines.Count; i++)
                    {
                        SelectedBusinessLine[i] = item.BusinessLines.ElementAt(i).Id;
                    }
                    tacticvm.BusinessLines_Id = SelectedBusinessLine;
                    //For Segment
                    int[] SelectedSegment = new int[item.Segments.Count];
                    for (int i = 0; i < item.Segments.Count; i++)
                    {
                        SelectedSegment[i] = item.Segments.ElementAt(i).Id;
                    }
                    tacticvm.Segments_Id = SelectedSegment;

                    //For Geography
                    int[] SelectedGeography = new int[item.Geographys.Count];
                    for (int i = 0; i < item.Geographys.Count; i++)
                    {
                        SelectedGeography[i] = item.Geographys.ElementAt(i).Id;
                    }
                    tacticvm.Geographys_Id = SelectedGeography;
                    //For Industry
                    List<Industry> industryList = _industryService.GetIndustryBySegmentId(tacticvm.Segments_Id);
                    tacticvm.IndustryViewModels = industryList;
                    int[] SelectedIndustry = new int[item.Industries.Count];
                    for (int i = 0; i < item.Industries.Count; i++)
                    {
                        SelectedIndustry[i] = item.Industries.ElementAt(i).Id;
                    }
                    tacticvm.Industries_Id = SelectedIndustry;

                    tacticvm.Name = item.Name;
                    tacticvm.TacticDescription = item.TacticDescription;
                    tacticvm.StartDate = Convert.ToString(item.StartDate);
                    tacticvm.EndDate = Convert.ToString(item.EndDate);
                    tacticvm.Year = item.Year;
                    tacticvm.Status = item.Status;
                }
            }
            return View(tacticvm);
        }

        public ActionResult Save(TacticCampaignViewModel model, string button)
        {
            //  if (Session["UserInfo"] == null) { return RedirectToAction("Index", "Home");

            if (button == "Submit" || button == "Update") { model.Status = "Complete"; }
            else if (button == "Delete Draft") { model.IsActive = false; }
            else if (button == "Save Draft") { model.Status = button; }


            if (model.Id != 0 && model.Status == "Save Draft")
            {
                bool result1 = _tacticCampaignServices.DeleteTacticCampaign(model.Id);
                if (result1 == true)
                    return RedirectToAction("ChildCampaign", "ChildCampaign");
                else
                    return View("MasterCampaign", model);
            }



            bool result;
            if (isValid(model))
            {
                result = _tacticCampaignServices.CreateTacticCampaign(model);
                if (result == true)
                {
                    return RedirectToAction("TacticList", "TacticList");
                }
                else
                {
                    return RedirectToAction("TacticCampaign", "TacticCampaign");
                }
            }



            ////Bind DDl ///////////////////////////
            model.VendorViewModels = _tacticCampaignServices.GetVendor();
            model.MasterViewModels= _masterCampaignServices.GetMasterCampaign();
            List<MasterCampaign> lst = _masterCampaignServices.GetMasterCampaignById(new MasterCampaignViewModel { Id = model.MasterCampaign_Id });
            foreach (var item in lst)
            {
                model.IndustryViewModels = item.Industries;
                model.BusinessGroupViewModels = item.BusinessGroups;
                model.BusinessLineViewModels = item.BusinessLines;
                model.SegmentViewModels = item.Segments;
                model.ThemeViewModels = item.Themes;
                model.GeographyViewModels = item.Geographys;
                model.ChildCampaignViewModels = item.ChildCampaigns;
            }

            //  model.ChildCampaignViewModels = _childCampaignServices.GetChildCampaign();


            List<ChildCampaign> childlst = _childCampaignServices.GetChildCampaignById(new ChildCampaignViewModel { Id = model.ChildCampaign_Id });
            foreach (var item in childlst)
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
                return View("TacticCampaign", model);
            }

            if (model.BusinessGroups_Id != null)
            {
                List<BusinessLine> businesslist = _businesslineService.GetBusinessLineByBGId(model.BusinessGroups_Id);
                model.BusinessGroupViewModels = _businessgroupService.GetBG();
                model.BusinessGroups_Id = model.BusinessGroups_Id;
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
                return View("TacticCampaign", model);
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
                return View("TacticCampaign",model);
            }
            ///////////////////////////////////////
            
         
            return View("TacticCampaign", model);
        }
                
    }
}