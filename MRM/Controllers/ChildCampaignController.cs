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


            Childvm.MasterViewModels = _masterCampaignServices.GetMasterCampaign().Where(t => t.Status == "Complete");
            //Childvm.BusinessGroupViewModels = _businessgroupService.GetBG();
            //Childvm.SegmentViewModels = _segmentService.GetSegment();
            //Childvm.ThemeViewModels = _themeService.GetTheme();
            //Childvm.GeographyViewModels = _geographyService.GetGeography();

            if (Id != 0)
            {

                ChildCampaign childCampaign = _childCampaignServices.GetChildCampaignById(new ChildCampaignViewModel { Id = Id }).First();

                List<TacticCampaign> tacticList = _tacticCampaignServices.GetTacticCampaignByChildId(Id).ToList();
                var Inheritanceflag=0;
                foreach (var itemtacticList in tacticList)
                {
                   // if (itemtacticList.InheritStatus != "Complete")
                   if (itemtacticList.Status != "Complete" && (itemtacticList.EndDate>DateTime.Now))
                        Inheritanceflag = 1;
                }
                if (tacticList.Count == 0)
                {
                    Childvm.InheritanceStatus = (childCampaign.Status == "Save Draft" ? "Draft" : "Active");
                }
                else if (Inheritanceflag== 0) { Childvm.InheritanceStatus = "Complete"; }
                else { Childvm.InheritanceStatus = "Active"; }

                if (childCampaign.MasterCampaigns.Id != 0)
                {
                    List<MasterCampaign> masterChild =
                        _masterCampaignServices.GetMasterCampaignById(childCampaign.MasterCampaigns.Id);
                    foreach (var item in masterChild)
                    {
                        Childvm.BusinessGroupViewModels = item.BusinessGroups;
                        Childvm.SegmentViewModels = item.Segments;
                        Childvm.GeographyViewModels = item.Geographys;
                        Childvm.ThemeViewModels = item.Themes;

                        Childvm.BusinessLineViewModels = item.BusinessLines;
                        Childvm.IndustryViewModels = item.Industries;
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

              //  Childvm.BusinessLineViewModels = _businesslineService.GetBusinessLineByBGId(Childvm.BusinessGroups_Id);

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

                
                //Childvm.IndustryViewModels = _industryService.GetIndustryBySegmentId(Childvm.Segments_Id); ;

                Childvm.Name = childCampaign.Name;
                Childvm.CampaignDescription = childCampaign.CampaignDescription;
                Childvm.MarketingInfluenceLeads = childCampaign.MarketingInfluenceLeads;
                Childvm.MarketingGeneratedLeads = childCampaign.MarketingGeneratedLeads;
                Childvm.Budget = childCampaign.Budget;
                Childvm.StartDate = childCampaign.StartDate;
                Childvm.EndDate = childCampaign.EndDate;
                Childvm.MarketingInfluenceOpportunity = childCampaign.MarketingInfluenceOpportunity;
                Childvm.MarketingGeneratedOpportunity = childCampaign.MarketingGeneratedOpportunity;
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
                Childvm.CampaignTypes = (childCampaign.CampaignType == 0 ? CampaignType.BG_Led : CampaignType.GEPS);
               // Childvm.CampaignTypes = (CampaignType)Enum.Parse(typeof(CampaignType), Childvm.CampaignTypes);

                //if(Childvm.MasterViewModels.Any(x => x..Contains(childCampaign.MasterCampaigns.Id)))


                var MasterCampaignName=string.Empty;
                foreach (var val in Childvm.MasterViewModels)
                {
                    if (val.Id == childCampaign.MasterCampaigns.Id)
                    {
                        MasterCampaignName = val.Name;
                    }
                }
               
                Childvm.StatusInheritaceStamp = String.Format("{0:yy}", childCampaign.UpdatedDate) + "." + MasterCampaignName + "." + Childvm.Name + " //" +  (Childvm.Status=="Save Draft"?"Draft":Childvm.InheritanceStatus) +
                                                 " // " + String.Format("{0:ddMMyy HH:MM}", childCampaign.UpdatedDate);

                ManageSelectUnselect(Childvm);

                //Update visited date
                if (Childvm.Status == "Save Draft")
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
            //List<BusinessLine> businesslist = _businesslineService.GetBusinessLineByBGId(model.BusinessGroups_Id);
            //model.BusinessGroupViewModels = _businessgroupService.GetBG();
            //model.BusinessGroups_Id = model.BusinessGroups_Id;
            //model.BusinessLineViewModels = businesslist;
            //model.SegmentViewModels = _segmentService.GetSegment();


            //if (model.Segments_Id == null)
            //    model.IndustryViewModels = (new Industry[] { new Industry() });
            //else
            //{
            //    List<Industry> lst = _industryService.GetIndustryBySegmentId(model.Segments_Id);
            //    model.IndustryViewModels = lst;
            //}

            if (model.MasterCampaignId != 0)
            {
                List<MasterCampaign> masterChild =
                    _masterCampaignServices.GetMasterCampaignById(model.MasterCampaignId);
                foreach (var item in masterChild)
                {
                    //model.BusinessGroupViewModels = item.BusinessGroups;
                    //model.SegmentViewModels = item.Segments;
                    if (model.CampaignTypes == 0)
                    {
                        model.BusinessGroupViewModels = model.BusinessGroupViewModels.Concat(item.BusinessGroups);
                        model.SegmentViewModels = item.Segments;
                    }
                    else
                    {
                        model.BusinessGroupViewModels = item.BusinessGroups;
                        model.SegmentViewModels = model.SegmentViewModels.Concat(item.Segments);
                    }
                    model.GeographyViewModels = item.Geographys;
                    model.ThemeViewModels = item.Themes;
                    if (model.Segments_Id != null)
                    {
                        model.IndustryViewModels = item.Industries;
                    }
                    if (model.BusinessGroups_Id != null)
                    {
                        model.BusinessLineViewModels = item.BusinessLines;
                    }
                    model.MCStartDate = item.StartDate;
                    model.MCEndDate = item.EndDate;
                }
            }

            ManageSelectUnselect(model);

            //   model.GeographyViewModels = _geographyService.GetGeography();
            model.MasterViewModels = _masterCampaignServices.GetMasterCampaign().Where(t => t.Status == "Complete");
           // model.ThemeViewModels = _themeService.GetTheme();
            return PartialView("ChildCampaignForm", model);
        }
        public ActionResult LoadIndustry(ChildCampaignViewModel model)
        {

            //model.BusinessGroupViewModels = _businessgroupService.GetBG();

            //if (model.BusinessGroups_Id == null)
            //    model.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
            //else
            //{
            //    List<BusinessLine> businesslist = _businesslineService.GetBusinessLineByBGId(model.BusinessGroups_Id);
            //    model.BusinessLineViewModels = businesslist;
            //}

            ////model.SegmentViewModels = _segmentService.GetSegment();
            //model.Segments_Id = model.Segments_Id;
            //List<Industry> lst = _industryService.GetIndustryBySegmentId(model.Segments_Id);
            //model.IndustryViewModels = lst.Where(t=>t.IsActive==true);

            //model.GeographyViewModels = _geographyService.GetGeography();
            // model.ThemeViewModels = _themeService.GetTheme();

            if (model.MasterCampaignId != 0)
            {
                List<MasterCampaign> masterChild =
                    _masterCampaignServices.GetMasterCampaignById(model.MasterCampaignId);
                foreach (var item in masterChild)
                {
                    //model.BusinessGroupViewModels = item.BusinessGroups;
                    //model.SegmentViewModels = item.Segments;
                    if (model.CampaignTypes == 0)
                    {
                        model.BusinessGroupViewModels = model.BusinessGroupViewModels.Concat(item.BusinessGroups);
                        model.SegmentViewModels = item.Segments;
                    }
                    else
                    {
                        model.BusinessGroupViewModels = item.BusinessGroups;
                        model.SegmentViewModels = model.SegmentViewModels.Concat(item.Segments);
                    }


                    model.GeographyViewModels = item.Geographys;
                    model.ThemeViewModels = item.Themes;
                    if (model.Segments_Id != null)
                    {
                        model.IndustryViewModels = item.Industries;
                    }
                    if (model.BusinessGroups_Id != null)
                    {
                        model.BusinessLineViewModels = item.BusinessLines;
                    }
                    model.MCStartDate = item.StartDate;
                    model.MCEndDate = item.EndDate;
                }
            }
            
            ManageSelectUnselect(model);

            model.MasterViewModels = _masterCampaignServices.GetMasterCampaign().Where(t => t.Status == "Complete");
            return PartialView("ChildCampaignForm", model);
        }
        public ActionResult LoadMasterCampaign(ChildCampaignViewModel model)
        {


            //List<ChildCampaign> masterChild = _childCampaignServices.GetChildCampaignByMasterId(model.MasterCampaignId);

            if (model.MasterCampaignId != 0)
            {
                List<MasterCampaign> masterChild =
                    _masterCampaignServices.GetMasterCampaignById(model.MasterCampaignId);
                foreach (var item in masterChild)
                {
                    model.IndustryViewModels = item.Industries;
                    if (model.CampaignTypes == 0)
                    {
                        model.BusinessGroupViewModels = model.BusinessGroupViewModels.Concat(item.BusinessGroups);
                        model.SegmentViewModels = item.Segments;
                    }
                    else
                    {
                        model.BusinessGroupViewModels = item.BusinessGroups;
                        model.SegmentViewModels = model.SegmentViewModels.Concat(item.Segments);
                    }
                    model.BusinessLineViewModels = item.BusinessLines;
                    model.ThemeViewModels = item.Themes;
                    model.GeographyViewModels = item.Geographys;
                    //model.StartDate = item.StartDate;
                    //model.EndDate = item.EndDate;
                    model.MCStartDate = item.StartDate;
                    model.MCEndDate = item.EndDate;
                }
            }

            ManageSelectUnselect(model);

            model.MasterViewModels = _masterCampaignServices.GetMasterCampaign().Where(t => t.Status == "Complete");

            return PartialView("ChildCampaignForm", model);
        }


        public ActionResult OnChangeCampaignTypes(ChildCampaignViewModel model)
        {
            List<MasterCampaign> masterChild = _masterCampaignServices.GetMasterCampaignById(model.MasterCampaignId);
            
            foreach (var item in masterChild)
            {
                model.IndustryViewModels = item.Industries;
                if (model.CampaignTypes == 0)
                {
                    model.BusinessGroupViewModels = model.BusinessGroupViewModels.Concat(item.BusinessGroups);
                    model.SegmentViewModels = item.Segments;
                }
                else
                {
                    model.BusinessGroupViewModels = item.BusinessGroups;
                    model.SegmentViewModels = model.SegmentViewModels.Concat(item.Segments);
                }
            }
            ManageSelectUnselect(model);
            model.MasterViewModels = _masterCampaignServices.GetMasterCampaign().Where(t => t.Status == "Complete");
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
        public bool Save(ChildCampaignViewModel model, string button)
        {

            model.MasterViewModels = _masterCampaignServices.GetMasterCampaign().Where(t => t.Status == "Complete");

            try
            {
                //todo:
                if (button == "Draft")
                {
                    if (model.Id == 0)// insert new record as draft
                    {
                        model.Status = "Save Draft";
                        _childCampaignServices.InsertChildCampaign(model);
                        return true;
                    }
                    model.Status = "Save Draft";
                    _childCampaignServices.Update(model);
                    return true;
                }
                if (isValid(model))
                {
                    if (model.Id == 0)
                    {
                        model.Status = "Complete";
                        _childCampaignServices.InsertChildCampaign(model);
                        return true;
                    }
                    else
                    {
                        model.Status = "Complete";
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
                if (model.MasterCampaignId == 0) errorCounter++;
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
                if (model.Budget == "") errorCounter++;
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
            _childCampaignServices.DeleteLastyearVisited();
            List<ChildCampaignViewModelList> childCampaignList = (from campaign in _childCampaignServices.GetChildCampaign()
                                                                  where campaign.IsActive == true
                                                                  select
                                                                  new ChildCampaignViewModelList
                                                                  {
                                                                      Id = string.Format("C{0}", campaign.Id.ToString("0000000")),
                                                                      InheritStatus = (ReturnInheritStatus(campaign.Id))=="Complete"?"Complete":(campaign.Status == "Save Draft" ? "Draft" : "Active"),
                                                                      Name = campaign.Name,
                                                                      CampaignDescription = campaign.CampaignDescription,
                                                                      Status = campaign.Status == "Save Draft" ? "Draft" : "Active",
                                                                      StartDate = String.Format("{0:dd/MM/yyyy}", campaign.StartDate),
                                                                      EndDate = String.Format("{0:dd/MM/yyyy}", campaign.EndDate)
                                                                  }
                                                                 ).ToList();
            return Json(childCampaignList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteChildCampaign(int id)
        {
            var childCampaign = _childCampaignServices.GetChildCampaignById(new ChildCampaignViewModel() { Id = id }).FirstOrDefault();
            childCampaign.IsActive = false;
            _childCampaignServices.Update(childCampaign);
            return Json(GetChildCampaignList(), JsonRequestBehavior.AllowGet);
        }

        public string ReturnInheritStatus(int Id)
        {
            List<TacticCampaign> tacticList = _tacticCampaignServices.GetTacticCampaignByChildId(Id).ToList();
            var Inheritanceflag = 1;
            string InheritanceStatus = string.Empty;
            foreach (var itemtacticList in tacticList)
            {
               // if (itemtacticList.InheritStatus == "Complete")
               if (itemtacticList.Status == "Complete" && (itemtacticList.EndDate<DateTime.Now))
                {
                    Inheritanceflag = 0;
                }
                else
                {
                    Inheritanceflag = 1;
                }
                
            }
            if (Inheritanceflag == 0) { InheritanceStatus = "Complete"; }
            else { InheritanceStatus = "Active"; }

            return InheritanceStatus;
        }


    }
}