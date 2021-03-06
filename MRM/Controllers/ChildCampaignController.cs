﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.Business.Services;
using MRM.Database.Model;
using MRM.Database.GenericRepository;
using MRM.ViewModel;
using System.Collections;
using DataTables.Mvc;
using MRM.Models;
using System.Web.Script.Serialization;
using MRM.Common;

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
        private TacticCampaignServices _tacticCampaignServices = null;

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
            _tacticCampaignServices=new TacticCampaignServices();
        }

        public ActionResult ChildCampaign(int Id = 0)
        {
            ChildCampaignViewModel Childvm = new ChildCampaignViewModel();

            Childvm.MasterViewModels = _masterCampaignServices.GetOrderedMasterCampaign().Where(t => t.Status == Status.Complete.ToString()).ToList();

            if (Id != 0)
            {
                ChildCampaign childCampaign = _childCampaignServices.GetChildCampaignById(new ChildCampaignViewModel { Id = Id }).First();

                Childvm.SubCampaignBudgetingDetailViewModels = _childCampaignServices.GetSubCampaignBudgetingDetails(Id).ToList();

                //List<TacticCampaign> tacticList = _tacticCampaignServices.GetTacticCampaignByChildId(Id).ToList();
                //var Inheritanceflag=0;
                //foreach (var itemtacticList in tacticList)
                //{                   
                //   if (itemtacticList.Status != Status.Complete.ToString() && (itemtacticList.EndDate>DateTime.Now))
                //        Inheritanceflag = 1;
                //}
                //if (tacticList.Count == 0)
                //{
                //    Childvm.InheritanceStatus = childCampaign.InheritStatus;
                //}
                //else if (Inheritanceflag== 0) { Childvm.InheritanceStatus = InheritStatus.Complete.ToString(); }
                //else { Childvm.InheritanceStatus = InheritStatus.Active.ToString(); }

                if (childCampaign.MasterCampaigns.Id != 0)
                {
                    List<MasterCampaign> masterChild =
                        _masterCampaignServices.GetMasterCampaignById(childCampaign.MasterCampaigns.Id);
                    foreach (var item in masterChild)
                    {
                        if (Childvm.CampaignTypes == 0)
                        {
                            Childvm.BusinessGroupViewModels = Childvm.BusinessGroupViewModels.Concat(item.BusinessGroups).ToList();
                            Childvm.SegmentViewModels = item.Segments.ToList();
                        }
                        else
                        {
                            Childvm.BusinessGroupViewModels = item.BusinessGroups.ToList();
                            Childvm.SegmentViewModels = Childvm.SegmentViewModels.Concat(item.Segments).ToList();
                        }
                        
                        Childvm.GeographyViewModels = item.Geographys.ToList();
                        Childvm.ThemeViewModels = item.Themes.ToList();

                        Childvm.BusinessLineViewModels = item.BusinessLines.ToList();
                        Childvm.IndustryViewModels = item.Industries.ToList();
                        Childvm.MCStartDate = item.StartDate;
                        Childvm.MCEndDate = item.EndDate;
                    }
                }

                if (childCampaign.MasterCampaigns != null && childCampaign.MasterCampaigns.Id != 0)
                {
                    Childvm.MasterCampaignId = childCampaign.MasterCampaigns.Id;
                }

                if (childCampaign.Themes != null && childCampaign.Themes.Count > 0)
                {
                    Childvm.Themes_Id = childCampaign.Themes.Select(t => t.Id).ToArray(); ;
                }


                if (childCampaign.BusinessGroups != null && childCampaign.BusinessGroups.Count > 0)
                {
                    Childvm.BusinessGroups_Id = childCampaign.BusinessGroups.Select(t => t.Id).ToArray(); ;
                }


                if (childCampaign.BusinessLines != null && childCampaign.BusinessLines.Count > 0)
                {
                    Childvm.BusinessLines_Id = childCampaign.BusinessLines.Select(t => t.Id).ToArray(); ;
                }


                if (childCampaign.Segments != null && childCampaign.Segments.Count > 0)
                {
                    Childvm.Segments_Id = childCampaign.Segments.Select(t => t.Id).ToArray(); ;
                }

                if (childCampaign.Geographys != null && childCampaign.Geographys.Count > 0)
                {
                    Childvm.Geographys_Id = childCampaign.Geographys.Select(t => t.Id).ToArray(); ;
                }


                if (childCampaign.Industries != null && childCampaign.Industries.Count > 0)
                {
                    Childvm.Industries_Id = childCampaign.Industries.Select(t => t.Id).ToArray(); ;
                }

                Childvm.Name = childCampaign.Name;
                Childvm.CampaignDescription = childCampaign.CampaignDescription;
                Childvm.CampaignManager = childCampaign.CampaignManager;
             
                Childvm.Budget = childCampaign.Budget;
                Childvm.StartDate = childCampaign.StartDate;
                Childvm.EndDate = childCampaign.EndDate;
               
                Childvm.Spend = childCampaign.Spend;
                Childvm.Id = childCampaign.Id;
                Childvm.Status = childCampaign.Status;
                Childvm.MILGoal = childCampaign.MILGoal;
                Childvm.MILLow = childCampaign.MILLow;
                Childvm.MILHigh = childCampaign.MILHigh;
                Childvm.MGLGoal = childCampaign.MGLGoal;
                Childvm.MGLLow = childCampaign.MGLLow;
                Childvm.MGLHigh = childCampaign.MGLHigh;
                Childvm.MIOGoal = childCampaign.MIOGoal;
                Childvm.MIOLow = childCampaign.MIOLow;
                Childvm.MIOHigh = childCampaign.MIOHigh;
                Childvm.MGOGoal = childCampaign.MGOGoal;
                Childvm.MGOLow = childCampaign.MGOLow;
                Childvm.MGOHigh = childCampaign.MGOHigh;
                Childvm.MILSource = childCampaign.MILSource;
                Childvm.MGLSource = childCampaign.MGLSource;
                Childvm.MIOSource = childCampaign.MIOSource;
                Childvm.MGOSource = childCampaign.MGOSource;

                Childvm.CampaignTypes = (childCampaign.CampaignType == 0 ? CampaignType.BG_Led : CampaignType.GEPS);
                Childvm.BusinessLineId = childCampaign.BusinessLineId;

                var MasterCampaignName=string.Empty;
                foreach (var val in Childvm.MasterViewModels)
                {
                    if (val.Id == childCampaign.MasterCampaigns.Id)
                    {
                        MasterCampaignName = val.Name;
                    }
                }

                Childvm.InheritanceStatus = childCampaign.InheritStatus;
                Childvm.StatusInheritaceStamp = String.Format("{0:yy}", childCampaign.UpdatedDate) + "." + MasterCampaignName + "." + Childvm.Name + " //" 
                                                +  (Childvm.Status==Status.Draft.ToString()?Status.Draft.ToString():Childvm.InheritanceStatus) +
                                                 " // " + String.Format("{0:ddMMyy HH:MM}", childCampaign.UpdatedDate);

                ManageSelectUnselect(Childvm);

                //Update visited date
                if (Childvm.Status == Status.Draft.ToString())
                {
                     childCampaign.VisitedDate = DateTime.Now;
                    _childCampaignServices.Update(childCampaign);
                }
            }

            return View(Childvm);
        }

        [HttpPost]
        public bool Delete(int childId)
        {
            var childCampaign = _childCampaignServices.GetChildCampaignById(new ChildCampaignViewModel() { Id = childId }).First();

            childCampaign.IsActive = false;
            _childCampaignServices.Update(childCampaign);
            return true;
        }

        public ActionResult LoadBusinessLine(ChildCampaignViewModel model)
        {
            if (model.MasterCampaignId != 0)
            {
                List<MasterCampaign> masterChild =
                    _masterCampaignServices.GetMasterCampaignById(model.MasterCampaignId);
             
                foreach (var item in masterChild)
                {
                    if (model.CampaignTypes == 0)
                    {
                        model.BusinessGroupViewModels = model.BusinessGroupViewModels.Concat(item.BusinessGroups).ToList();
                        model.SegmentViewModels = item.Segments.ToList();
                    }
                    else
                    {
                        model.BusinessGroupViewModels = item.BusinessGroups.ToList();
                        model.SegmentViewModels = model.SegmentViewModels.Concat(item.Segments).ToList();
                    }
                    model.GeographyViewModels = item.Geographys.ToList();
                    model.ThemeViewModels = item.Themes.ToList();
                    if (model.Segments_Id != null)
                    {
                        if (model.Segments_Id[0] != 0 && model.Segments_Id[0] != -1)
                            model.IndustryViewModels = item.Industries.ToList();

                        //List<Industry> lst = _industryService.GetIndustryBySegmentId(model.Segments_Id);
                        //model.IndustryViewModels = lst;

                    }
                    else
                    {
                        model.Industries_Id = null;
                    }
                    if (model.BusinessGroups_Id != null)
                    {
                        if (model.BusinessGroups_Id[0] != 0 && model.BusinessGroups_Id[0] != -1)
                            model.BusinessLineViewModels = model.BusinessLineViewModels.Concat(item.BusinessLines).ToList();
                      //  model.BusinessLineViewModels = item.BusinessLines.ToList();

                        //List<BusinessLine> businesslist = _businesslineService.GetBusinessLineByBGId(model.BusinessGroups_Id);
                        //model.BusinessLineViewModels = businesslist;
                    }
                    else
                    {
                        model.BusinessLines_Id = null;
                    }
                    model.MCStartDate = item.StartDate;
                    model.MCEndDate = item.EndDate;
                }
            }

            ManageSelectUnselect(model);
            model.MasterViewModels = _masterCampaignServices.GetOrderedMasterCampaign().Where(t => t.Status == Status.Complete.ToString()).ToList();
       
            return PartialView("ChildCampaignForm", model);
        }

        [HttpPost]
        public ActionResult LoadMultBlForMultiBG(int[] businessGroupIDs)
        {
            List<BusinessLine> businesslist = _businesslineService.GetBusinessLineByBGId(businessGroupIDs);
            List<DropDownValues> dropDownValues = businesslist.Select(x => new DropDownValues { Id = x.Id, Value = x.Name }).ToList();
            return Json(dropDownValues, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult LoadIndustry(ChildCampaignViewModel model)
        {
            if (model.MasterCampaignId != 0)
            {
                List<MasterCampaign> masterChild =
                    _masterCampaignServices.GetMasterCampaignById(model.MasterCampaignId);
                foreach (var item in masterChild)
                {
                    if (model.CampaignTypes == 0)
                    {
                        model.BusinessGroupViewModels = model.BusinessGroupViewModels.Concat(item.BusinessGroups).ToList();
                        model.SegmentViewModels = item.Segments.ToList();
                    }
                    else
                    {
                        model.BusinessGroupViewModels = item.BusinessGroups.ToList();
                        model.SegmentViewModels = model.SegmentViewModels.Concat(item.Segments).ToList();
                    }

                    model.GeographyViewModels = item.Geographys.ToList();
                    model.ThemeViewModels = item.Themes.ToList();
                    if (model.Segments_Id != null)
                    {
                        if (model.Segments_Id[0] != 0 && model.Segments_Id[0] != -1)
                            model.IndustryViewModels = item.Industries.ToList();
                    }
                    else
                    {
                        model.Industries_Id = null;
                    }
                    if (model.BusinessGroups_Id != null)
                    {
                        if (model.BusinessGroups_Id[0] != 0 && model.BusinessGroups_Id[0] != -1)
                            model.BusinessLineViewModels = model.BusinessLineViewModels.Concat(item.BusinessLines).ToList();
                        //  model.BusinessLineViewModels = item.BusinessLines.ToList();
                    }
                    else
                    {
                        model.BusinessLines_Id = null;
                    }
                    model.MCStartDate = item.StartDate;
                    model.MCEndDate = item.EndDate;
                }
            }
            
            ManageSelectUnselect(model);

            model.MasterViewModels = _masterCampaignServices.GetOrderedMasterCampaign().Where(t => t.Status == Status.Complete.ToString()).ToList();
            return PartialView("ChildCampaignForm", model);
        }
        public ActionResult LoadMasterCampaign(ChildCampaignViewModel model)
        {
            model.Industries_Id = null;
            model.BusinessLines_Id = null;

            if (model.MasterCampaignId != 0)
            {
                List<MasterCampaign> masterChild =
                    _masterCampaignServices.GetMasterCampaignById(model.MasterCampaignId);
                foreach (var item in masterChild)
                {
                   // model.IndustryViewModels = item.Industries.ToList();
                    if (model.CampaignTypes == 0)
                    {
                        model.BusinessGroupViewModels = model.BusinessGroupViewModels.Concat(item.BusinessGroups).ToList();
                        model.SegmentViewModels = item.Segments.ToList();
                    }
                    else
                    {
                        model.BusinessGroupViewModels = item.BusinessGroups.ToList();
                        model.SegmentViewModels = model.SegmentViewModels.Concat(item.Segments).ToList();
                    }

                    //if (model.Segments_Id != null)
                    //{
                    //    if (model.Segments_Id[0] != 0 && model.Segments_Id[0] != -1)
                    //        model.IndustryViewModels = item.Industries.ToList();
                    //}
                    //else {
                    //    model.Industries_Id = null;
                    //}
                    //if (model.BusinessGroups_Id != null)
                    //{
                    //    if (model.BusinessGroups_Id[0] != 0 && model.BusinessGroups_Id[0] != -1)
                    //        model.BusinessLineViewModels = item.BusinessLines.ToList();
                    //}
                    //else {
                    //    model.BusinessLines_Id = null;
                    //}
                    
                    model.ThemeViewModels = item.Themes.ToList();
                    model.GeographyViewModels = item.Geographys.ToList();
                    model.MCStartDate = item.StartDate;
                    model.MCEndDate = item.EndDate;
                }
            }
            else if (model.MasterCampaignId == 0)
            {
                ModelState.Clear();
                ChildCampaignViewModel modelCCVM = new ChildCampaignViewModel();
                modelCCVM.MasterViewModels = _masterCampaignServices.GetOrderedMasterCampaign().Where(t => t.Status == "Complete").ToList();
                return PartialView("ChildCampaignForm", modelCCVM);
            }

            ManageSelectUnselect(model);

            model.MasterViewModels = _masterCampaignServices.GetOrderedMasterCampaign().Where(t => t.Status == Status.Complete.ToString()).ToList();

            return PartialView("ChildCampaignForm", model);
        }


        public ActionResult OnChangeCampaignTypes(ChildCampaignViewModel model)
        {
            List<MasterCampaign> masterChild = _masterCampaignServices.GetMasterCampaignById(model.MasterCampaignId);

            model.BusinessGroups_Id = null;
            model.BusinessLines_Id = null;
            model.Industries_Id = null;
            model.Segments_Id=null;

            foreach (var item in masterChild)
            {
               // model.IndustryViewModels = item.Industries.ToList();
                if (model.CampaignTypes == 0)
                {
                    model.BusinessGroupViewModels = model.BusinessGroupViewModels.Concat(item.BusinessGroups).ToList();
                    model.SegmentViewModels = item.Segments.ToList();
                }
                else
                {
                    model.BusinessGroups_Id = null;
                    model.BusinessLines_Id = null;
                    model.BusinessGroupViewModels = item.BusinessGroups.ToList();
                    model.SegmentViewModels = model.SegmentViewModels.Concat(item.Segments).ToList();
                }
                model.ThemeViewModels = item.Themes.ToList();
                model.GeographyViewModels = item.Geographys.ToList();


                model.MCStartDate = item.StartDate;
                model.MCEndDate = item.EndDate;
            }
            

            ManageSelectUnselect(model);
            model.MasterViewModels = _masterCampaignServices.GetOrderedMasterCampaign().Where(t => t.Status == Status.Complete.ToString()).ToList();
            return PartialView("ChildCampaignForm", model);
        }

        //Manage SelectUnselect
        public ChildCampaignViewModel ManageSelectUnselect(ChildCampaignViewModel model)
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
        public bool Save(ChildCampaignViewModel model, string button,string budgetModel)
        {

            model.MasterViewModels = _masterCampaignServices.GetOrderedMasterCampaign().Where(t => t.Status == Status.Complete.ToString()).ToList();
            // Deserializing json model to object 
            List<SubCampaignBudgetingDetail> viewModel = new JavaScriptSerializer().Deserialize<List<SubCampaignBudgetingDetail>>(budgetModel);
            model.SubCampaignBudgetingDetailViewModels = viewModel;
            try
            {
                //todo:
                if (button == "Save Draft")
                {
                    if (model.Id == 0)// insert new record as draft
                    {
                        model.Status = Status.Draft.ToString();
                        _childCampaignServices.InsertChildCampaign(model);
                        return true;
                    }
                    model.Status = Status.Draft.ToString();
                    _childCampaignServices.Update(model);
                    return true;
                }
                if (isValid(model))
                {
                    if (model.Id == 0)
                    {
                        model.Status = Status.Complete.ToString();
                        _childCampaignServices.InsertChildCampaign(model);
                        return true;
                    }
                    else
                    {
                        model.Status = Status.Complete.ToString();
                        _childCampaignServices.Update(model);
                        return true;
                    }
                    
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool isValid(ChildCampaignViewModel model)
        {
            int errorCounter = 0;

            if (model.Id != 0)
            {
                if (Convert.ToDateTime(model.StartDate) > Convert.ToDateTime(model.EndDate)) errorCounter++;
                if (model.CampaignDescription == "") errorCounter++;
                if (model.StartDate == null) errorCounter++;
                if (model.EndDate == null) errorCounter++;
                if (model.StartDate != null && model.EndDate != null)
                {
                    if (model.StartDate != null && model.EndDate != null)
                    {
                        if (model.StartDate < model.MCStartDate || model.EndDate > model.MCEndDate) errorCounter++;
                    }

                }
            }
            else
            {
                if (model.MasterCampaignId == 0) errorCounter++;
                if (model.BusinessGroups_Id == null) errorCounter++;
                if (model.BusinessLines_Id == null) errorCounter++;
                if (model.Segments_Id == null) errorCounter++;
                if (model.StartDate == null) errorCounter++;
                if (model.EndDate == null) errorCounter++;
                //if (model.CampaignTypes == CampaignType.GEPS && model.Segments_Id == null) errorCounter++;
                if (Convert.ToDateTime(model.StartDate) > Convert.ToDateTime(model.EndDate)) errorCounter++;
                if (model.Name == "") errorCounter++;
                if (model.CampaignDescription == "") errorCounter++;
                if (model.Budget == 0) errorCounter++;
                if (model.CampaignTypes == CampaignType.BG_Led)
                {
                    var bgArr = model.BusinessGroups_Id;
                    if (bgArr != null && bgArr.Length != 1) errorCounter++;
                }
                else
                {
                    var segArr = model.Segments_Id;
                    if (segArr != null && segArr.Length != 1) errorCounter++;
                }
                if (model.CampaignTypes == CampaignType.GEPS)
                {
                    if (model.Industries_Id == null) errorCounter++;
                }

                if (model.StartDate != null && model.EndDate != null)
                {
                    if (model.StartDate != null && model.EndDate != null)
                    {
                        if (model.StartDate < model.MCStartDate || model.EndDate > model.MCEndDate) errorCounter++;
                    }

                }
            }

            return errorCounter == 0;
        }

        public ActionResult ChildCampaignList()
        {
            ChildCampaignViewModel childCampaignViewModel = new ChildCampaignViewModel();
            return View(childCampaignViewModel);
        }

        [HttpGet]
        public JsonResult GetChildCampaignList()
        {
            List<ChildCampaignViewModelList> childCampaignList = (from campaign in _childCampaignServices.GetChildCampaign()
                                                                  where campaign.IsActive == true
                                                                  select
                                                                  new ChildCampaignViewModelList
                                                                  {
                                                                      Id = string.Format("C{0}", campaign.Id.ToString("0000000")),                                                                      
                                                                      InheritStatus = campaign.InheritStatus,
                                                                      Name = campaign.Name,
                                                                      CampaignDescription = campaign.CampaignDescription,
                                                                      CampaignManager=campaign.CampaignManager,
                                                                      CreatedBy = campaign.CreatedBy,
                                                                      Status = campaign.Status,
                                                                      StartDate = String.Format("{0:dd MMM yyyy}", campaign.StartDate),
                                                                      EndDate = String.Format("{0:dd MMM yyyy}", campaign.EndDate)
                                                                  }
                                                                 ).ToList();
            return Json(childCampaignList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetChildCampaignListByPage([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestmodel)
        {
            var chilList = _childCampaignServices.GetOrderedChildCampaign().Where(x => x.IsActive == true);

            //var filteredData =  chilList.Where(_item => _item.Name.ToLower().StartsWith(requestmodel.Search.Value.ToLower()));

            //var result = chilList.Skip(requestmodel.Start).Take(requestmodel.Length);
            Util util = new Util();
            var data = !String.IsNullOrEmpty(requestmodel.Search.Value) ? chilList.Where(_item => _item.Name.ToLower().StartsWith(requestmodel.Search.Value.ToLower())) : chilList.Skip(requestmodel.Start).Take(requestmodel.Length);
            List<ChildCampaignViewModelList> childCampaignList = (from campaign in data.ToList()
                                                                   where campaign.IsActive == true
                                                                   select
                                                                      new ChildCampaignViewModelList
                                                                      {
                                                                          DigitalID = string.Format("C{0}", util.DigitalId(campaign.Id).PadLeft(5, '0')),
                                                                          Id = string.Format("C{0}", campaign.Id.ToString("0000000")),
                                                                          InheritStatus = campaign.InheritStatus,                                                                          
                                                                          Name = campaign.Name,
                                                                          CampaignDescription = campaign.CampaignDescription,
                                                                          CampaignManager = campaign.CampaignManager,
                                                                          CreatedBy = campaign.CreatedBy,
                                                                          Status = campaign.Status,
                                                                          StartDate = String.Format("{0:dd MMM yyyy}", campaign.StartDate),
                                                                          EndDate = String.Format("{0:dd MMM yyyy}", campaign.EndDate)
                                                                      }

                                                     ).ToList();
            
            return Json(new DataTablesResponse(requestmodel.Draw, childCampaignList, !String.IsNullOrEmpty(requestmodel.Search.Value) ? data.Count() : chilList.Count(), !String.IsNullOrEmpty(requestmodel.Search.Value) ? data.Count() : chilList.Count()), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteChildCampaign(int id)
        {
            var childCampaign = _childCampaignServices.GetChildCampaignById(new ChildCampaignViewModel() { Id = id }).FirstOrDefault();
            childCampaign.IsActive = false;
            _childCampaignServices.Update(childCampaign);
            return Json(JsonRequestBehavior.AllowGet);
        }        
    }
}