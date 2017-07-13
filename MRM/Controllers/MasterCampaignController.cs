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
        private TacticCampaignServices _tacticCampaignServices = null;
       
        public MasterCampaignController()
        {
            _industryService = new IndustryServices();
            _businessgroupService = new BusinessGroupServices();
            _businesslineService = new BusinessLineServices();
            _segmentService = new SegmentServices();
            _geographyService = new GeographyServices();
            _themeService = new ThemeServices();
            _masterCampaignServices = new MasterCampaignServices();
            _tacticCampaignServices = new TacticCampaignServices();
        }

        // GET: CampaignForm
        public ActionResult Index()
        {
            //if (Session["UserInfo"] == null) {return RedirectToAction("Index", "Home");}
            //TempData["mastercount"] = "";
            //var mastercount = _masterCampaignServices.GetMasterCampaign().Count();
            return View();
        }

        public ActionResult MasterCampaign(int Id = 0)
        {
            MasterCampaignViewModel mcvm = new MasterCampaignViewModel();
            mcvm.BusinessGroupViewModels = _businessgroupService.GetBG();
            mcvm.SegmentViewModels = _segmentService.GetSegment();
            mcvm.GeographyViewModels = _geographyService.GetGeography();
            mcvm.ThemeViewModels = _themeService.GetTheme();

            if (Id != 0)
            {
                MasterCampaign masterCampaign = _masterCampaignServices.GetMasterCampaignById(new MasterCampaignViewModel { Id = Id }).First();

                mcvm.InheritanceStatus= ReturnInheritStatus(Id);

                if (masterCampaign.Themes != null && masterCampaign.Themes.Count > 0)
                {
                    mcvm.Themes_Id = masterCampaign.Themes.Select(t => t.Id).ToArray(); ;
                }


                if (masterCampaign.BusinessGroups != null && masterCampaign.BusinessGroups.Count > 0)
                {
                    mcvm.BusinessGroups_Id = masterCampaign.BusinessGroups.Select(t => t.Id).ToArray(); ;
                    mcvm.BgSelectUnselect = true;
                }

                mcvm.BusinessLineViewModels = _businesslineService.GetBusinessLineByBGId(mcvm.BusinessGroups_Id);

                if (masterCampaign.BusinessLines != null && masterCampaign.BusinessLines.Count > 0)
                {
                    mcvm.BusinessLines_Id = masterCampaign.BusinessLines.Select(t => t.Id).ToArray(); ;
                }


                if (masterCampaign.Geographys != null && masterCampaign.Geographys.Count > 0)
                {
                    mcvm.Geographys_Id = masterCampaign.Geographys.Select(t => t.Id).ToArray(); ;
                }

                if (masterCampaign.Segments != null && masterCampaign.Segments.Count > 0)
                {
                    mcvm.Segments_Id = masterCampaign.Segments.Select(t => t.Id).ToArray(); ;
                    mcvm.SegSelectUnselect = true;
                }

                if (masterCampaign.Industries != null && masterCampaign.Industries.Count > 0)
                {
                    mcvm.Industries_Id = masterCampaign.Industries.Select(t => t.Id).ToArray(); ;
                }

                mcvm.IndustryViewModels = _industryService.GetIndustryBySegmentId(mcvm.Segments_Id).Where(t=>t.IsActive==true); ;


                mcvm.Name = masterCampaign.Name;
                mcvm.CampaignDescription = masterCampaign.CampaignDescription;
                if (masterCampaign.StartDate != null) mcvm.StartDate = masterCampaign.StartDate.Value;
                if (masterCampaign.EndDate != null) mcvm.EndDate = masterCampaign.EndDate.Value;
                mcvm.Id = Id;
                mcvm.Status = masterCampaign.Status;


                mcvm.StatusInheritaceStamp = String.Format("{0:yy}", masterCampaign.UpdatedDate) + "." + mcvm.Name + " //" + (mcvm.Status == "Save Draft" ? "Draft" : mcvm.InheritanceStatus) +
                                                " // " + String.Format("{0:ddMMyy HH:MM}", masterCampaign.UpdatedDate);


                ManageSelectUnselect(mcvm);
            }

            return View(mcvm);
        }

        [HttpPost]
        public bool Delete(int masterId)
        {
            var masterCampaign = _masterCampaignServices.GetMasterCampaignById(new MasterCampaignViewModel { Id = masterId }).First();
            masterCampaign.IsActive = false;
            _masterCampaignServices.Update(masterCampaign);
            return true;
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
            ManageSelectUnselect(model);
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
            model.IndustryViewModels = lst.Where(t=>t.IsActive==true);
          
            model.GeographyViewModels = _geographyService.GetGeography();
            model.ThemeViewModels = _themeService.GetTheme();
            ManageSelectUnselect(model);
            return PartialView("MasterCampaignForm", model);
        }

        //Manage SelectUnselect
        public MasterCampaignViewModel ManageSelectUnselect(MasterCampaignViewModel model)
        {
            if ((model.BusinessGroups_Id != null && (model.BusinessGroupViewModels.ToList().Count > model.BusinessGroups_Id.Length)) || model.BusinessGroups_Id == null) { model.BgSelectUnselect = false; }
            else { model.BgSelectUnselect = true; }
            if ((model.BusinessLines_Id != null && (model.BusinessLineViewModels.ToList().Count > model.BusinessLines_Id.Length)) || model.BusinessLines_Id == null) { model.BlSelectUnselect = false; }
            else { model.BlSelectUnselect = true; }
            if ((model.Segments_Id != null && (model.SegmentViewModels.ToList().Count > model.Segments_Id.Length)) || model.Segments_Id == null) { model.SegSelectUnselect = false; }
            else { model.SegSelectUnselect = true; }
            if ((model.Industries_Id != null && (model.IndustryViewModels.ToList().Count > model.Industries_Id.Length)) || model.Industries_Id == null) { model.IndustrySelectUnselect = false; }
            else { model.IndustrySelectUnselect = true; }
            if ((model.Themes_Id != null && (model.ThemeViewModels.ToList().Count > model.Themes_Id.Length)) || model.Themes_Id == null) { model.ThemeSelectUnselect = false; }
            else { model.ThemeSelectUnselect = true; }
            if ((model.Geographys_Id != null && (model.GeographyViewModels.ToList().Count > model.Geographys_Id.Length)) || model.Geographys_Id == null) { model.GeoSelectUnselect = false; }
            else { model.GeoSelectUnselect = true; }

            return model;
        }

        [HttpPost]
        public bool Save(MasterCampaignViewModel model, string button)
        {
            try
            {
                //todo:
                if (button == "Draft")
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

                return false;
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
                if (model.StartDate == null) errorCounter++;
                if (model.EndDate == null) errorCounter++;
                if (Convert.ToDateTime(model.StartDate) > Convert.ToDateTime(model.EndDate)) errorCounter++;
                if (model.Name == "") errorCounter++;
                if (model.CampaignDescription == "") errorCounter++;

            }
            else
            {

                if (model.BusinessGroups_Id == null) errorCounter++;
                if (model.BusinessLines_Id == null) errorCounter++;
                if (model.Segments_Id == null) errorCounter++;
                if (model.Industries_Id == null) errorCounter++;
                if (model.Geographys_Id == null) errorCounter++;
                if (model.StartDate == null) errorCounter++;
                if (model.EndDate == null) errorCounter++;
                if (Convert.ToDateTime(model.StartDate) > Convert.ToDateTime(model.EndDate)) errorCounter++;
                if (model.Name == "") errorCounter++;
                if (model.CampaignDescription == "") errorCounter++;

            }
            return errorCounter == 0;
        }

        public ActionResult CampaignList()
        {
            MasterCampaignViewModel campaignViewModel = new MasterCampaignViewModel();
            return View(campaignViewModel);
        }

        [HttpGet]
        public JsonResult GetMasterCampaignList()
        {
            List<MasterCampaignViewModelListing> masterCampaignList = (from campaign in _masterCampaignServices.GetMasterCampaign()
                                                                       where campaign.IsActive == true
                                                                       select
                                                                       new MasterCampaignViewModelListing
                                                                       {
                                                                           Id = string.Format("M{0}", campaign.Id.ToString("0000000")),
                                                                           Name = campaign.Name,
                                                                           InheritStatus = (ReturnInheritStatus(campaign.Id)) == "Complete" ? "Complete" : (campaign.Status == "Save Draft" ? "Draft" : "Active"),
                                                                           CampaignDescription = campaign.CampaignDescription,
                                                                           Status = campaign.Status=="Save Draft"? "Draft":"Active",
                                                                           StartDate = String.Format("{0:dd/MM/yyyy}", campaign.StartDate),
                                                                           EndDate = String.Format("{0:dd/MM/yyyy}", campaign.EndDate)
                                                                       }

                                                     ).ToList();
            return Json(masterCampaignList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteCampaign(int id)
        {
            var masterCampaign = _masterCampaignServices.GetMasterCampaignById(new MasterCampaignViewModel { Id = id }).FirstOrDefault();
            masterCampaign.IsActive = false;
            _masterCampaignServices.Update(masterCampaign);
            return Json(GetMasterCampaignList(), JsonRequestBehavior.AllowGet);
        }
       
        public string ReturnInheritStatus(int Id)
        {

            List<ChildCampaign> childList = _masterCampaignServices.GetChildCampaignByMasterId(Id).ToList();

            var Inheritanceflag = 1;
            string InheritanceStatus = string.Empty;

            if (childList.Count == 0)
            {InheritanceStatus = "Active";}

            foreach (var itemChildList in childList)
            {
                List<TacticCampaign> tacticList = _tacticCampaignServices.GetTacticCampaignByMasterId(itemChildList.Id).ToList();

                foreach (var itemTCList in tacticList)
                {
                    Inheritanceflag = (itemTCList.InheritStatus == "Complete" ? 0 : 1);
                }
                if (tacticList.Count == 0 || Inheritanceflag == 1)
                {
                    InheritanceStatus = "Active";
                }
                else if (Inheritanceflag == 0)
                {
                    InheritanceStatus = "Complete";
                }

            }
            return InheritanceStatus;
        }
    }
}