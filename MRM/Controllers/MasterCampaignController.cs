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
            //if (Session["UserInfo"] == null) {return RedirectToAction("Index", "Home");}
            TempData["mastercount"] = "";
            var mastercount = _masterCampaignServices.GetMasterCampaign().Count();
            return View();
        }

        public ActionResult MasterCampaign(int Id = 0)
        {
            //    if (Session["UserInfo"] == null) { return RedirectToAction("Index", "Home"); }
            MasterCampaignViewModel mcvm = new MasterCampaignViewModel();
            mcvm.BusinessGroupViewModels = _businessgroupService.GetBG();
            mcvm.SegmentViewModels = _segmentService.GetSegment();
            mcvm.GeographyViewModels = _geographyService.GetGeography();
            mcvm.ThemeViewModels = _themeService.GetTheme();

            if (Id != 0)
            {
                List<MasterCampaign> lst = _masterCampaignServices.GetMasterCampaignById(new MasterCampaignViewModel { Id = Id });
                foreach (var item in lst)
                {
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
                    List<BusinessLine> businesslist = _businesslineService.GetBusinessLineByBGId(mcvm.BusinessGroups_Id);
                    mcvm.BusinessLineViewModels = businesslist;
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
                    List<Industry> industryList = _industryService.GetIndustryBySegmentId(mcvm.Segments_Id);
                    mcvm.IndustryViewModels = industryList;
                    mcvm.Name = item.Name;
                    mcvm.CampaignDescription = item.CampaignDescription;
                    mcvm.StartDate = Convert.ToString(item.StartDate);
                    mcvm.EndDate = Convert.ToString(item.EndDate);
                    mcvm.Id = Id;
                    mcvm.Status = item.Status;

                }

            }



            return View(mcvm);
        }

        [HttpPost]
        public ActionResult Delete(int masterId)
        {
            var masterCampaign = _masterCampaignServices.GetMasterCampaignById(new MasterCampaignViewModel { Id = masterId }).First();
            masterCampaign.IsActive = false;
            _masterCampaignServices.Update(masterCampaign);
            return RedirectToAction("MasterList", "MasterList");
        }


        public ActionResult LoadBusinessLine(MasterCampaignViewModel model)
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
                List<Industry> lst = _industryService.GetIndustryBySegmentId(model.Segments_Id);
                model.IndustryViewModels = lst;
            }
            model.GeographyViewModels = _geographyService.GetGeography();
            model.ThemeViewModels = _themeService.GetTheme();
            return PartialView("MasterCampaignForm", model);
        }


        public ActionResult LoadIndustry(MasterCampaignViewModel model)
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
            List<Industry> lst = _industryService.GetIndustryBySegmentId(model.Segments_Id);
            model.IndustryViewModels = lst;
            model.GeographyViewModels = _geographyService.GetGeography();
            model.ThemeViewModels = _themeService.GetTheme();
            return View("MasterCampaign", model);
        }

        [HttpPost]
        public bool Save(MasterCampaignViewModel model,string button)
        {
            try
            {
                //todo:
                if (button == "Save Draft")
                {
                    if (model.Id == 0)// insert new record as draft
                    {
                        model.Status = "Save Draft";
                        _masterCampaignServices.InsertMasterCampaign(model);
                        return true;
                    }
                    else // Update master campaign 
                    {
                        model.Status = "Save Draft";
                        _masterCampaignServices.UpdateForDraft(model);
                        return true;
                    }
                }
                else // submission 
                {
                    if (isValid(model))
                    {
                        if (model.Id == 0)// insert new record as draft
                        {
                            model.Status = "Complete";
                            _masterCampaignServices.InsertMasterCampaign(model);
                            return true;
                        }
                        else
                        {
                            model.Status = "Complete";
                            _masterCampaignServices.Submit(model);
                            return true;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool isValid(MasterCampaignViewModel model)
        {
            int errorCounter = 0;

            if (model.Id != 0)
            {
                if (model.BusinessGroups_Id == null) errorCounter++;
                if (model.BusinessLines_Id == null) errorCounter++;
                if (model.Segments_Id == null) errorCounter++;
                if (model.Industries_Id == null) errorCounter++;
                if (model.Geographys_Id == null) errorCounter++;
                if (model.StartDate == "") errorCounter++;
                if (model.EndDate == "") errorCounter++;
                if (Convert.ToDateTime(model.StartDate) > Convert.ToDateTime(model.EndDate)) errorCounter++;
                if (model.Name == "") errorCounter++;
                if (model.CampaignDescription == "") errorCounter++;

            }
            else {

                if (model.BusinessGroups_Id == null) errorCounter++;
                if (model.BusinessLines_Id == null) errorCounter++;
                if (model.Segments_Id == null) errorCounter++;
                if (model.Industries_Id == null) errorCounter++;
                if (model.Geographys_Id == null) errorCounter++;
                if (model.StartDate == "") errorCounter++;
                if (model.EndDate == "") errorCounter++;
                if (Convert.ToDateTime(model.StartDate) > Convert.ToDateTime(model.EndDate)) errorCounter++;
                if (model.Name == "") errorCounter++;
                if (model.CampaignDescription == "") errorCounter++;

            }
            return errorCounter == 0;
        }
    }
}