using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.Business.Services;
using MRM.Database.Model;
using MRM.Database.GenericRepository;
using Newtonsoft.Json;
using MRM.ViewModel;
using System.Collections;

namespace MRM.Controllers
{
    [AllowAnonymous]
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
            if (Session["UserInfo"] == null) {return RedirectToAction("Index", "Home");}
            TempData["mastercount"] = "";
            var mastercount = _masterCampaignServices.GetMasterCampaign().Count();
            //if (mastercount <= 0)
            //{
            //    TempData["mastercount"] = "There is no master campaign available,Create master campaign first!";
            //    return RedirectToAction("Index", "MasterCampaign");
            //}
           
            return View();
        }


        //updated by suraj
        public ActionResult MasterCampaign(int id = 0)
        {
            if (id == 0)
            {
                //   if (Session["UserInfo"] == null) { return RedirectToAction("Index", "Home"); }
                mcvm.IndustryViewModels = _industryService.GetIndustry();
                mcvm.BusinessGroupViewModels = _businessgroupService.GetBG();
                mcvm.BusinessLineViewModels = _businesslineService.GetBusinessLine();
                mcvm.SegmentViewModels = _segmentService.GetSegment();
                mcvm.GeographyViewModels = _geographyService.GetGeography();
                mcvm.ThemeViewModels = _themeService.GetTheme();

            }

            else if (id != 0)
            {
                List<MasterCampaign> lst = _masterCampaignServices.GetMasterCampaignById(new MasterCampaignViewModel { Id = id });
                foreach (var item in lst)
                {
                  
                    mcvm.IndustryViewModels = _industryService.GetIndustry();
                    mcvm.BusinessGroupViewModels = _businessgroupService.GetBG();
                    mcvm.BusinessLineViewModels = _businesslineService.GetBusinessLine();
                    mcvm.SegmentViewModels = _segmentService.GetSegment();
                    mcvm.GeographyViewModels = _geographyService.GetGeography();
                    mcvm.ThemeViewModels = _themeService.GetTheme();
                    //For Theme
                    int[] SelectedThemes = new int[item.Themes.Count];
                    for (int i = 0; i < item.Themes.Count; i++)
                    {
                        SelectedThemes[i] = item.Themes.ElementAt(i).Id;
                    }
                    mcvm.Themes_Id = SelectedThemes;

                    //For BusinessGroups

                    int[] SelectedBusinessGroup = new int[item.BusinessGroups.Count];
                    for (int i = 0; i < item.BusinessGroups.Count; i++)
                    {
                        SelectedBusinessGroup[i] = item.BusinessGroups.ElementAt(i).Id;
                    }
                    mcvm.BusinessGroups_Id = SelectedBusinessGroup;

                    //For BusinessLines
                    int[] SelectedBusinessLine = new int[item.BusinessLines.Count];
                    for (int i = 0; i < item.BusinessLines.Count; i++)
                    {
                        SelectedBusinessLine[i] = item.BusinessLines.ElementAt(i).Id;
                    }
                    mcvm.BusinessLines_Id = SelectedBusinessLine;
                    //For Segment
                    int[] SelectedSegment = new int[item.Segments.Count];
                    for (int i = 0; i < item.Segments.Count; i++)
                    {
                        SelectedSegment[i] = item.Segments.ElementAt(i).Id;
                    }
                    mcvm.Segments_Id = SelectedSegment;

                    //For Geography
                    int[] SelectedGeography = new int[item.Geographys.Count];
                    for (int i = 0; i < item.Geographys.Count; i++)
                    {
                        SelectedGeography[i] = item.Geographys.ElementAt(i).Id;
                    }
                    mcvm.Geographys_Id = SelectedGeography;
                    //For Industry
                    int[] SelectedIndustry = new int[item.Industries.Count];
                    for (int i = 0; i < item.Industries.Count; i++)
                    {
                        SelectedIndustry[i] = item.Industries.ElementAt(i).Id;
                    }
                    mcvm.Industries_Id = SelectedIndustry;
                    
                    //for testing only
                    //mcvm.SelectedThemes_Id = new int[] { 1, 2 };

                }

                List<MasterCampaign> masterobjlist = new List<MasterCampaign>();
                masterobjlist = _masterCampaignServices.GetMasterCampaignById(new MasterCampaignViewModel { Id = id });
                foreach (var item in masterobjlist)
                {
                    mcvm.Name = item.Name;
                    mcvm.CampaignDescription = item.CampaignDescription;
                    mcvm.StartDate = Convert.ToString(item.StartDate);
                    mcvm.EndDate = Convert.ToString(item.EndDate);
                    mcvm.Id = id;
                    
                }

            }
            
            return View(mcvm);
        }


       

        [HttpPost]
        public ActionResult Save(MasterCampaignViewModel model,string button)
        {
            // if (Session["UserInfo"] == null) { return RedirectToAction("Index", "Home"); }
             bool result =false;

            if (button == "Submit")
            {
                result = _masterCampaignServices.CreateMasterCampaign(model);
            }
            else if (button == "Delete")
            {
             
            }
            else if(button== "Update") 
            {
               

                 result = _masterCampaignServices.CreateMasterCampaign(model);
            }
            
            
            if (result == true)
            {
                return RedirectToAction("MasterList", "MasterList");
            }
            else
            {
                return RedirectToAction("MasterCampaign", "MasterCampaign");
            }

        }

       

    }
}