﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MRM.Business.Services;
using MRM.Database.Model;
using MRM.ViewModel;
using DataTables.Mvc;
using MRM.Common;

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

                mcvm.InheritanceStatus= masterCampaign.InheritStatus;

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

                mcvm.IndustryViewModels = _industryService.GetIndustryBySegmentId(mcvm.Segments_Id).Where(t=>t.IsActive==true).ToList();


                mcvm.Name = masterCampaign.Name;
                mcvm.CampaignDescription = masterCampaign.CampaignDescription;
                mcvm.CampaignManager = masterCampaign.CampaignManager;
                if (masterCampaign.StartDate != null) mcvm.StartDate = masterCampaign.StartDate.Value;
                if (masterCampaign.EndDate != null) mcvm.EndDate = masterCampaign.EndDate.Value;
                mcvm.Id = Id;
                mcvm.Status = masterCampaign.Status;


                mcvm.StatusInheritaceStamp = String.Format("{0:yy}", masterCampaign.UpdatedDate) + "." + mcvm.Name + " //" 
                                             + (mcvm.Status == Status.Draft.ToString() ? Status.Draft.ToString() : mcvm.InheritanceStatus) +
                                             " // " + String.Format("{0:ddMMyy HH:MM}", masterCampaign.UpdatedDate);


                ManageSelectUnselect(mcvm);

                //Update visited date
                if (mcvm.Status == Status.Draft.ToString())
                {
                     masterCampaign.VisitedDate = DateTime.Now;
                    _masterCampaignServices.Update(masterCampaign);
                }
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
            model.BusinessGroupViewModels = _businessgroupService.GetBG();
            model.SegmentViewModels = _segmentService.GetSegment();
            if (model.Segments_Id == null)
            {
                model.IndustryViewModels = (new Industry[] { new Industry() });
                model.Industries_Id = null;
            }
            else
            {
                List<Industry> lst = _industryService.GetIndustryBySegmentId(model.Segments_Id);
                model.IndustryViewModels = lst;
            }

            if (model.BusinessGroups_Id == null)
            {
                model.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
                model.BusinessLines_Id = null;
            }
            else
            {
                model.BusinessGroups_Id = model.BusinessGroups_Id;
                List<BusinessLine> businesslist = _businesslineService.GetBusinessLineByBGId(model.BusinessGroups_Id);
                model.BusinessLineViewModels = businesslist;
            }

            model.GeographyViewModels = _geographyService.GetGeography();
            model.ThemeViewModels = _themeService.GetTheme();
            ManageSelectUnselect(model);
            return PartialView("MasterCampaignForm", model);
        }

        public ActionResult LoadIndustry(MasterCampaignViewModel model)
        {
            model.BusinessGroupViewModels = _businessgroupService.GetBG();


            model.SegmentViewModels = _segmentService.GetSegment();
            if (model.BusinessGroups_Id == null)
            {
                model.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
                model.BusinessLines_Id = null;
            }
            else
            {
                List<BusinessLine> businesslist = _businesslineService.GetBusinessLineByBGId(model.BusinessGroups_Id);
                model.BusinessLineViewModels = businesslist;
            }

            if (model.Segments_Id == null)
            {
                model.IndustryViewModels = (new Industry[] { new Industry() });
                model.Industries_Id = null;
            }
            else
            {
                model.Segments_Id = model.Segments_Id;
                List<Industry> Ilst = _industryService.GetIndustryBySegmentId(model.Segments_Id);
                model.IndustryViewModels = Ilst;
                model.IndustryViewModels = Ilst.Where(t => t.IsActive == true).ToList();
            }
      
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
                if (button == "Save Draft")
                {
                    if (model.Id == 0)// insert new record as draft
                    {
                        model.Status = Status.Draft.ToString();
                        _masterCampaignServices.InsertMasterCampaign(model);
                        return true;
                    }
                    else // Update master campaign 
                    {
                        model.Status = Status.Draft.ToString();
                        _masterCampaignServices.UpdateForDraft(model);
                        return true;
                    }
                }
                else // submission 
                {
                    if (isValid(model, button))
                    {
                        if (model.Id == 0)// insert new record as draft
                        {
                            model.Status = Status.Complete.ToString();
                            _masterCampaignServices.InsertMasterCampaign(model);
                            return true;
                        }
                        else
                        {
                            model.Status = Status.Complete.ToString();
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

        private bool isValid(MasterCampaignViewModel model,string button)
        {
            int errorCounter = 0;

            if (model.Id != 0)
            {
                if (button != "Update")
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
                                                                           CampaignManager = campaign.CampaignManager,
                                                                           CreatedBy = campaign.CreatedBy,
                                                                           InheritStatus = campaign.InheritStatus,                                                                           
                                                                           CampaignDescription = campaign.CampaignDescription,
                                                                           Status = campaign.Status,
                                                                           StartDate = String.Format("{0:dd MMM yyyy}", campaign.StartDate),
                                                                           EndDate = String.Format("{0:dd MMM yyyy}", campaign.EndDate)
                                                                       }

                                                     ).ToList();
            return Json(masterCampaignList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetMasterCampaignListByPage([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestmodel)
        {
            var masterList = _masterCampaignServices.GetOrderedMasterCampaign().Where(x => x.IsActive == true);

            //var filteredData =  masterList.Where(_item => _item.Name.ToLower().StartsWith(requestmodel.Search.Value.ToLower()));

            //var result = masterList.Skip(requestmodel.Start).Take(requestmodel.Length);
            Util util = new Util();
            var data = !String.IsNullOrEmpty(requestmodel.Search.Value) ? masterList.Where(_item => _item.Name.ToLower().StartsWith(requestmodel.Search.Value.ToLower())) : masterList.Skip(requestmodel.Start).Take(requestmodel.Length);
            List <MasterCampaignViewModelListing> masterCampaignList = (from campaign in data.ToList()
                                                                        where campaign.IsActive == true
                                                                        select
                                                                       new MasterCampaignViewModelListing
                                                                       {
                                                                           DigitalID = string.Format("M{0}", util.DigitalId(campaign.Id).PadLeft(5, '0')),
                                                                           Id = string.Format("M{0}", campaign.Id.ToString("0000000")),
                                                                           Name = campaign.Name,
                                                                           CampaignManager = campaign.CampaignManager,
                                                                           CreatedBy = campaign.CreatedBy,
                                                                           InheritStatus = campaign.InheritStatus,                                                                           
                                                                           CampaignDescription = campaign.CampaignDescription,
                                                                           Status = campaign.Status,
                                                                           StartDate = String.Format("{0:dd MMM yyyy}", campaign.StartDate),
                                                                           EndDate = String.Format("{0:dd MMM yyyy}", campaign.EndDate)
                                                                       }

                                                     ).ToList();
            
            return Json(new DataTablesResponse(requestmodel.Draw, masterCampaignList, !String.IsNullOrEmpty(requestmodel.Search.Value) ? data.Count() : masterList.Count(), !String.IsNullOrEmpty(requestmodel.Search.Value) ? data.Count() : masterList.Count()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteCampaign(int id)
        {
            var masterCampaign = _masterCampaignServices.GetMasterCampaignById(new MasterCampaignViewModel { Id = id }).FirstOrDefault();
            masterCampaign.IsActive = false;
            _masterCampaignServices.Update(masterCampaign);
            return Json(JsonRequestBehavior.AllowGet);
        }
    }
}