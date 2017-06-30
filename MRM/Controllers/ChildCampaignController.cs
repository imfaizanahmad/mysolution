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

        public ActionResult ChildCampaign(int Id = 0)
        {
            ChildCampaignViewModel Childvm = new ChildCampaignViewModel();


            Childvm.MasterViewModels = _masterCampaignServices.GetMasterCampaign().Where(t => t.Status == "Complete");
            Childvm.BusinessGroupViewModels = _businessgroupService.GetBG();
            Childvm.SegmentViewModels = _segmentService.GetSegment();
            Childvm.ThemeViewModels = _themeService.GetTheme();
            Childvm.GeographyViewModels = _geographyService.GetGeography();

            if (Id != 0)
            {

                ChildCampaign childCampaign = _childCampaignServices.GetChildCampaignById(new ChildCampaignViewModel { Id = Id }).First();

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

                Childvm.BusinessLineViewModels = _businesslineService.GetBusinessLineByBGId(Childvm.BusinessGroups_Id);

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


                if (Childvm.MasterCampaignId != 0)
                {
                    List<MasterCampaign> masterChild =
                        _masterCampaignServices.GetMasterCampaignById(Childvm.MasterCampaignId);
                    foreach (var item in masterChild)
                    {
                        Childvm.BusinessGroupViewModels = item.BusinessGroups;
                        Childvm.SegmentViewModels = item.Segments;
                        Childvm.GeographyViewModels = item.Geographys;
                        Childvm.ThemeViewModels = item.Themes;
                    }
                }

                Childvm.IndustryViewModels = _industryService.GetIndustryBySegmentId(Childvm.Segments_Id); ;
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
            List<BusinessLine> businesslist = _businesslineService.GetBusinessLineByBGId(model.BusinessGroups_Id);
            model.BusinessGroupViewModels = _businessgroupService.GetBG();
            model.BusinessGroups_Id = model.BusinessGroups_Id;
            model.BusinessLineViewModels = businesslist;
            model.SegmentViewModels = _segmentService.GetSegment();

           

            if (model.MasterCampaignId != 0)
            {
                List<MasterCampaign> masterChild =
                    _masterCampaignServices.GetMasterCampaignById(model.MasterCampaignId);
                foreach (var item in masterChild)
                {
                   // model.BusinessLineViewModels = item.BusinessLines;
                    model.BusinessGroupViewModels = item.BusinessGroups;
                    model.SegmentViewModels = item.Segments;
                    model.GeographyViewModels = item.Geographys;
                    model.ThemeViewModels = item.Themes;
                   // model.IndustryViewModels = item.Industries;

                }
            }

            if (model.Segments_Id == null)
                model.IndustryViewModels = (new Industry[] { new Industry() });
            else
            {
                List<Industry> lst = _industryService.GetIndustryBySegmentId(model.Segments_Id);
                model.IndustryViewModels = lst;
            }

            //   model.GeographyViewModels = _geographyService.GetGeography();
            model.MasterViewModels = _masterCampaignServices.GetMasterCampaign().Where(t => t.Status == "Complete");
           // model.ThemeViewModels = _themeService.GetTheme();
            return PartialView("ChildCampaignForm", model);
        }
        public ActionResult LoadIndustry(ChildCampaignViewModel model)
        {

            model.BusinessGroupViewModels = _businessgroupService.GetBG();

            if (model.BusinessGroups_Id == null)
                model.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
            else
            {
                List<BusinessLine> businesslist = _businesslineService.GetBusinessLineByBGId(model.BusinessGroups_Id);
                model.BusinessLineViewModels = businesslist;
            }

            //model.SegmentViewModels = _segmentService.GetSegment();
            model.Segments_Id = model.Segments_Id;
            List<Industry> lst = _industryService.GetIndustryBySegmentId(model.Segments_Id);
            model.IndustryViewModels = lst;

            //model.GeographyViewModels = _geographyService.GetGeography();
            // model.ThemeViewModels = _themeService.GetTheme();

            if (model.MasterCampaignId != 0)
            {
                List<MasterCampaign> masterChild =
                    _masterCampaignServices.GetMasterCampaignById(model.MasterCampaignId);
                foreach (var item in masterChild)
                {
                    model.BusinessGroupViewModels = item.BusinessGroups;
                    model.SegmentViewModels = item.Segments;
                    model.GeographyViewModels = item.Geographys;
                    model.ThemeViewModels = item.Themes;
                   // model.IndustryViewModels = item.Industries;
                }
            }


            model.MasterViewModels = _masterCampaignServices.GetMasterCampaign().Where(t => t.Status == "Complete");
            return PartialView("ChildCampaignForm", model);
        }
        public ActionResult LoadMasterCampaign(ChildCampaignViewModel model)
        {


            //List<ChildCampaign> masterChild = _childCampaignServices.GetChildCampaignByMasterId(model.MasterCampaignId);
            List<MasterCampaign> masterChild = _masterCampaignServices.GetMasterCampaignById(model.MasterCampaignId);
            foreach (var item in masterChild)
            {
                model.IndustryViewModels = item.Industries;
                model.BusinessGroupViewModels = item.BusinessGroups;
                model.BusinessLineViewModels = item.BusinessLines;
                model.SegmentViewModels = item.Segments;
                model.ThemeViewModels = item.Themes;
                model.GeographyViewModels = item.Geographys;
                model.StartDate = item.StartDate;
                model.EndDate = item.EndDate;
                model.MCStartDate = item.StartDate;
                model.MCEndDate = item.EndDate;
            }


            model.MasterViewModels = _masterCampaignServices.GetMasterCampaign().Where(t => t.Status == "Complete");

            return PartialView("ChildCampaignForm", model);
        }

        [HttpPost]
        public bool Save(ChildCampaignViewModel model, string button)
        {

            model.MasterViewModels = _masterCampaignServices.GetMasterCampaign().Where(t => t.Status == "Complete");

            try
            {
                //todo:
                if (button == "Save Draft")
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
            List<ChildCampaignViewModelList> childCampaignList = (from campaign in _childCampaignServices.GetChildCampaign().OrderByDescending(x => x.CreatedDate)
                                                                  where campaign.IsActive == true
                                                                  select
                                                                  new ChildCampaignViewModelList
                                                                  {
                                                                      Id = string.Format("C{0}", campaign.Id.ToString("0000000")),
                                                                      Name = campaign.Name,
                                                                      CampaignDescription = campaign.CampaignDescription,
                                                                      Status = campaign.Status == "Save Draft" ? "Draft" : "Active",
                                                                      StartDate = String.Format("{0:MM/dd/yyyy}", campaign.StartDate),
                                                                      EndDate = String.Format("{0:MM/dd/yyyy}", campaign.EndDate)
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
    }
}