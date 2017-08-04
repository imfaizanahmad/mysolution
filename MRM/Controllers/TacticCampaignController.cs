using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MRM.ViewModel;
using MRM.Business.Services;
using MRM.Database.Model;
using System.Web.Script.Serialization;
using DataTables.Mvc;

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

        private MetricReachServices _metricReachServices = null;
        private MetricResponseServices _metricResponseServices = null;
        private JourneyStageServices _journeyStageServices = null;

        TacticCampaignViewModel Tacticvm = new TacticCampaignViewModel();

        private DigitalTouchpoint _digitalTouchpoint;

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
            _masterCampaignServices = new MasterCampaignServices();

            _metricReachServices = new MetricReachServices();
            _metricResponseServices = new MetricResponseServices();
            _journeyStageServices = new JourneyStageServices();
            _digitalTouchpoint = new DigitalTouchpoint();
        }

        public ActionResult TacticCampaign(int Id = 0)
        {
            TacticCampaignViewModel tacticvm = new TacticCampaignViewModel();
            tacticvm.JourneyStageViewModels = _journeyStageServices.GetJourneyStage().ToList();
            var ttList = _tacticCampaignServices.GetTacticType().ToList();
            tacticvm.TacticTypeViewModels = tacticvm.TacticTypeViewModels.Concat(ttList).ToList();
            tacticvm.MasterViewModels = _masterCampaignServices.GetOrderedMasterCampaign().Where(t => t.Status == Status.Complete.ToString() && t.ChildCampaigns.Where(r => r.Status != Status.Draft.ToString()).ToList().Count != 0).ToList();

            if (Id == 0)
            {
                tacticvm.BusinessGroupViewModels = (new[] { new BusinessGroup()  });
                tacticvm.SegmentViewModels = (new[] { new Segment () });
                tacticvm.MetricReachViewModels = _metricReachServices.GetAllMetricReach().ToList();
                tacticvm.MetricResponseViewModels = _metricResponseServices.GetAllMetricResponse().ToList();

                return View(tacticvm);
            }

            if (Id != 0)
            {
                TacticCampaign tacticCampaign = _tacticCampaignServices.GetTacticCampaignById(new TacticCampaignViewModel { Id = Id }).First();

                tacticvm.ChildCampaignViewModels = _childCampaignServices.GetChildCampaignByMasterId(tacticCampaign.ChildCampaigns.MasterCampaigns.Id).Where(t => t.Status == Status.Complete.ToString()).ToList();

                if (tacticCampaign.ChildCampaigns.Id != 0)
                {
                    List<ChildCampaign> childCampaign = _childCampaignServices.GetDDLValuesByChildId(tacticCampaign.ChildCampaigns.Id);
                    foreach (var item in childCampaign)
                    {
                        if (item.CampaignType == 0)
                        {
                            tacticvm.BusinessGroupViewModels = tacticvm.BusinessGroupViewModels.Concat(item.BusinessGroups).ToList();
                            tacticvm.SegmentViewModels = item.Segments.ToList();
                        }
                        else
                        {
                            tacticvm.BusinessGroupViewModels = item.BusinessGroups.ToList();
                            tacticvm.SegmentViewModels = tacticvm.SegmentViewModels.Concat(item.Segments).ToList();
                        }
                        tacticvm.ThemeViewModels = item.Themes.ToList();
                        tacticvm.GeographyViewModels = item.Geographys.ToList();
                        tacticvm.BusinessLineViewModels = item.BusinessLines.ToList();
                        tacticvm.IndustryViewModels = item.Industries.ToList();
                        tacticvm.MCStartDate = item.StartDate;
                        tacticvm.MCEndDate = item.EndDate;
                    }
                }
                else
                {
                    tacticvm.IndustryViewModels = _industryService.GetIndustry().ToList();
                    tacticvm.BusinessGroupViewModels = _businessgroupService.GetBG().ToList();
                    tacticvm.BusinessLineViewModels = _businesslineService.GetBusinessLine().ToList();
                    tacticvm.SegmentViewModels = _segmentService.GetSegment().ToList();
                    tacticvm.ThemeViewModels = _themeService.GetTheme().ToList();
                    tacticvm.GeographyViewModels = _geographyService.GetGeography().ToList();
                }                

                if (tacticCampaign.ChildCampaigns.MasterCampaigns != null && tacticCampaign.ChildCampaigns.MasterCampaigns.Id != 0)
                {
                    tacticvm.MasterCampaign_Id = tacticCampaign.ChildCampaigns.MasterCampaigns.Id;
                }

                if (tacticCampaign.ChildCampaigns != null && tacticCampaign.ChildCampaigns.Id != 0)
                {
                    tacticvm.ChildCampaign_Id = tacticCampaign.ChildCampaigns.Id;
                }

                if (tacticCampaign.Themes != null && tacticCampaign.Themes.Count > 0)
                {
                    tacticvm.Themes_Id = tacticCampaign.Themes.Select(t => t.Id).ToArray(); ;
                }


                if (tacticCampaign.BusinessGroups != null && tacticCampaign.BusinessGroups.Count > 0)
                {
                    tacticvm.BusinessGroups_Id = tacticCampaign.BusinessGroups.Select(t => t.Id).ToArray(); ;
                }

                if (tacticCampaign.BusinessLines != null && tacticCampaign.BusinessLines.Count > 0)
                {
                    tacticvm.BusinessLines_Id = tacticCampaign.BusinessLines.Select(t => t.Id).ToArray(); ;
                }

                if (tacticCampaign.Segments != null && tacticCampaign.Segments.Count > 0)
                {
                    tacticvm.Segments_Id = tacticCampaign.Segments.Select(t => t.Id).ToArray(); ;
                }

                if (tacticCampaign.Industries != null && tacticCampaign.Industries.Count > 0)
                {
                    tacticvm.Industries_Id = tacticCampaign.Industries.Select(t => t.Id).ToArray(); ;
                }

                if (tacticCampaign.Geographys != null && tacticCampaign.Geographys.Count > 0)
                {
                    tacticvm.Geographys_Id = tacticCampaign.Geographys.Select(t => t.Id).ToArray(); ;
                }

                if (tacticCampaign.TacticTypes != null && tacticCampaign.TacticTypes.Count > 0)
                {
                    tacticvm.TacticType_Id = tacticCampaign.TacticTypes.Select(t => t.Id).ToArray(); ;
                }

                tacticvm.TacticType = tacticCampaign.TacticType;
                tacticvm.JournetStage_Id = tacticCampaign.JourneyStage_Id;
                tacticvm.Id = tacticCampaign.Id;
                tacticvm.Name = tacticCampaign.Name;
                tacticvm.TacticDescription = tacticCampaign.TacticDescription;
                tacticvm.StartDate = tacticCampaign.StartDate;
                tacticvm.EndDate = tacticCampaign.EndDate;
                tacticvm.Status = tacticCampaign.Status;
                tacticvm.Vendor = tacticCampaign.Vendor;
                tacticvm.Year = tacticCampaign.Year;
                tacticvm.Status = tacticCampaign.Status;
                tacticvm.MasterCampaign_Id = tacticCampaign.MasterCampaign_Id;

                var MasterCampaignName = string.Empty;
                var ChildCampaignName = string.Empty;
                foreach (var val in tacticvm.MasterViewModels)
                {
                    if (val.Id == tacticCampaign.MasterCampaign_Id)
                    { MasterCampaignName = val.Name; }
                }
                foreach (var val in tacticvm.ChildCampaignViewModels)
                {
                    if (val.Id == tacticCampaign.ChildCampaigns.Id)
                    { ChildCampaignName = val.Name; }
                }

                if (Tacticvm.EndDate < DateTime.Now)
                {
                    tacticCampaign.InheritStatus = Status.Complete.ToString();
                }

                tacticvm.StatusInheritaceStamp = String.Format("{0:yy}", tacticCampaign.UpdatedDate) + "." + MasterCampaignName + "." 
                                                 + ChildCampaignName + " //" + ((tacticvm.Status == Status.Complete.ToString() 
                                                 && (tacticvm.EndDate < DateTime.Now)) ? Status.Complete.ToString() : tacticvm.Status) +
                                                 " // " + String.Format("{0:ddMMyy HH:MM}", tacticCampaign.UpdatedDate);


                tacticvm.MetricReachViewModels = _metricReachServices.GetAllMetricReach().ToList();
                tacticvm.MetricResponseViewModels = _metricResponseServices.GetAllMetricResponse().ToList();
                tacticvm.TacticCampaignReachResponseViewModels = tacticCampaign.TacticCampaignReachResponses.ToList();

                //Update visited date
                if (tacticvm.Status == Status.Draft.ToString())
                {
                    tacticCampaign.VisitedDate = DateTime.Now;
                    _tacticCampaignServices.Update(tacticCampaign);
                }

            }
            return View(tacticvm);
        }

        [HttpPost]
        public bool Delete(int tacticId)
        {
            var tacticCampaign = _tacticCampaignServices.GetTacticCampaignById(new TacticCampaignViewModel() { Id = tacticId }).First();

            tacticCampaign.IsActive = false;
            _tacticCampaignServices.Update(tacticCampaign);
            return true;
        }

        public ActionResult LoadBusinessLine(TacticCampaignViewModel model)
        {
            if (model.BusinessGroups_Id == null || (model.BusinessGroups_Id!=null && model.BusinessGroups_Id[0] == -1))
            {
                model.BusinessLines_Id = null;
            }
            if (model.Segments_Id == null || (model.Segments_Id != null && model.Segments_Id[0] == -1))
            {
                model.Industries_Id = null;
            }
        
            var ttList = _tacticCampaignServices.GetTacticType();
            model.TacticTypeViewModels = model.TacticTypeViewModels.Concat(ttList).ToList();

            model.ThemeViewModels = _themeService.GetTheme().ToList();
            model.JourneyStageViewModels = _journeyStageServices.GetJourneyStage().ToList();
            model.MasterViewModels = _masterCampaignServices.GetOrderedMasterCampaign().Where(t => t.Status == Status.Complete.ToString() && t.ChildCampaigns.Where(r => r.Status != Status.Draft.ToString()).ToList().Count != 0).ToList();

            if (model.ChildCampaign_Id != 0)
            {
                List<ChildCampaign> childCampaign = _childCampaignServices.GetChildCampaignById(new ChildCampaignViewModel { Id = model.ChildCampaign_Id });
                foreach (var item in childCampaign)
                {
                    if (item.CampaignType == 0)
                    {
                        model.BusinessGroupViewModels = model.BusinessGroupViewModels.Concat(item.BusinessGroups).ToList();
                        model.SegmentViewModels = item.Segments.ToList();
                    }
                    else
                    {
                        model.BusinessGroupViewModels = item.BusinessGroups.ToList();
                        model.SegmentViewModels = model.SegmentViewModels.Concat(item.Segments).ToList();
                    }
                    model.ThemeViewModels = item.Themes.ToList();
                    model.GeographyViewModels = item.Geographys.ToList();
                    if (model.Segments_Id != null)
                    {
                        if (model.Segments_Id[0] != 0 && model.Segments_Id[0] != -1)
                        {
                            model.IndustryViewModels = item.Industries.ToList();
                            
                        }
                    }
                    else
                    {
                        model.Industries_Id = null;
                    }
                    if (model.BusinessGroups_Id != null)
                    {
                        if (model.BusinessGroups_Id[0] != 0 && model.BusinessGroups_Id[0] != -1)
                        {
                            model.BusinessLineViewModels = item.BusinessLines.ToList();
                           
                        }
                    }
                    else
                    {
                        model.BusinessLines_Id = null;
                    }

                    model.MCStartDate = item.StartDate;
                    model.MCEndDate = item.EndDate;
                    model.SubCampaignType = item.CampaignType;
                }
                model.MasterCampaign_Id = model.MasterCampaign_Id;
                model.ChildCampaignViewModels = _childCampaignServices
                    .GetChildCampaignByMasterId(model.MasterCampaign_Id).Where(t => t.Status == Status.Complete.ToString()).ToList();
                model.ChildCampaign_Id = model.ChildCampaign_Id;
            }
            else
            {
                model.MasterCampaign_Id = model.MasterCampaign_Id;
                if (model.MasterCampaign_Id != 0)
                {
                    model.ChildCampaignViewModels = _childCampaignServices
                        .GetChildCampaignByMasterId(model.MasterCampaign_Id).Where(t => t.Status == Status.Complete.ToString()).ToList();
                }

                model.ChildCampaign_Id = model.ChildCampaign_Id;
            }
            ManageSelectUnselect(model);

            model.MetricReachViewModels = _metricReachServices.GetAllMetricReach().ToList();
            model.MetricResponseViewModels = _metricResponseServices.GetAllMetricResponse().ToList();
            TacticCampaign tacticCampaign = _tacticCampaignServices.GetTacticCampaignById(new TacticCampaignViewModel { Id = model.Id }).FirstOrDefault();
            if (tacticCampaign != null)
                model.TacticCampaignReachResponseViewModels = tacticCampaign.TacticCampaignReachResponses.ToList();

            return PartialView("TacticCampaignForm", model);
        }
        public ActionResult LoadIndustry(TacticCampaignViewModel model)
        {
            if (model.BusinessGroups_Id == null || (model.BusinessGroups_Id != null && model.BusinessGroups_Id[0] == -1))
            {
                model.BusinessLines_Id = null;
            }
            if (model.Segments_Id == null || (model.Segments_Id != null && model.Segments_Id[0] == -1))
            {
                model.Industries_Id = null;
            }

            model.JourneyStageViewModels = _journeyStageServices.GetJourneyStage().ToList();
            model.MasterViewModels = _masterCampaignServices.GetOrderedMasterCampaign().Where(t => t.Status == Status.Complete.ToString() && t.ChildCampaigns.Where(r => r.Status != Status.Draft.ToString()).ToList().Count != 0).ToList();

            if (model.ChildCampaign_Id != 0)
            {
                List<ChildCampaign> childCampaign = _childCampaignServices.GetChildCampaignById(new ChildCampaignViewModel { Id = model.ChildCampaign_Id });
                foreach (var item in childCampaign)
                {
                    if (item.CampaignType == 0)
                    {
                        model.BusinessGroupViewModels = model.BusinessGroupViewModels.Concat(item.BusinessGroups).ToList();
                        model.SegmentViewModels = item.Segments.ToList();
                    }
                    else
                    {
                        model.BusinessGroupViewModels = item.BusinessGroups.ToList();
                        model.SegmentViewModels = model.SegmentViewModels.Concat(item.Segments).ToList().ToList();
                    }

                    model.ThemeViewModels = item.Themes.ToList();
                    model.GeographyViewModels = item.Geographys.ToList();
                    if (model.Segments_Id != null)
                    {
                        if (model.Segments_Id[0] != 0 && model.Segments_Id[0] != -1)
                        {
                            model.IndustryViewModels = item.Industries.ToList();
                            
                        }
                    }
                    else
                    {
                        model.Industries_Id = null;
                    }
                    if (model.BusinessGroups_Id != null)
                    {
                        if (model.BusinessGroups_Id[0] != 0 && model.BusinessGroups_Id[0] != -1)
                        {
                            model.BusinessLineViewModels = item.BusinessLines.ToList();
                            
                        }
                    }
                    else
                    {
                        model.BusinessLines_Id = null;
                    }
                    model.MCStartDate = item.StartDate;
                    model.MCEndDate = item.EndDate;
                    model.SubCampaignType = item.CampaignType;
                }
                
                model.MasterCampaign_Id = model.MasterCampaign_Id;
                model.ChildCampaignViewModels = _childCampaignServices.GetChildCampaignByMasterId(model.MasterCampaign_Id)
                        .Where(t => t.Status == Status.Complete.ToString()).ToList();
                model.ChildCampaign_Id = model.ChildCampaign_Id;
            }
            else
            {
                model.MasterCampaign_Id = model.MasterCampaign_Id;
                if (model.MasterCampaign_Id != 0)
                {
                    model.ChildCampaignViewModels = _childCampaignServices
                        .GetChildCampaignByMasterId(model.MasterCampaign_Id).Where(t => t.Status == Status.Complete.ToString()).ToList();
                }
                model.ChildCampaign_Id = model.ChildCampaign_Id;
            }

            ManageSelectUnselect(model);
            var ttList = _tacticCampaignServices.GetTacticType();
            model.TacticTypeViewModels = model.TacticTypeViewModels.Concat(ttList).ToList();

            model.MetricReachViewModels = _metricReachServices.GetAllMetricReach().ToList();
            model.MetricResponseViewModels = _metricResponseServices.GetAllMetricResponse().ToList();
            TacticCampaign tacticCampaign = _tacticCampaignServices.GetTacticCampaignById(new TacticCampaignViewModel { Id = model.Id }).FirstOrDefault();
            if (tacticCampaign != null)
                model.TacticCampaignReachResponseViewModels = tacticCampaign.TacticCampaignReachResponses.ToList();

            return PartialView("TacticCampaignForm", model);
        }

        public ActionResult LoadMasterCampaign(TacticCampaignViewModel model)
        {

            model.JourneyStageViewModels = _journeyStageServices.GetJourneyStage().ToList();
            model.BusinessGroupViewModels = (new[] { new BusinessGroup() });
            model.SegmentViewModels = (new[] { new Segment() });

            //If sub campaign is defined for corressponding master campaign
            if (model.MasterCampaign_Id != 0)
            {
                List<ChildCampaign> masterChild = _childCampaignServices
                    .GetChildCampaignByMasterId(model.MasterCampaign_Id).Where(t => t.Status == Status.Complete.ToString()).ToList();

                if (masterChild.Count > 0)
                {
                    model.ChildCampaignViewModels = masterChild;
                }
            }

            ManageSelectUnselect(model);

            model.MasterViewModels = _masterCampaignServices.GetOrderedMasterCampaign()
                .Where(t => t.Status == Status.Complete.ToString() && t.ChildCampaigns.Where(r => r.Status != Status.Draft.ToString()).ToList().Count != 0)
                .ToList();

            if (model.MasterCampaign_Id != 0)
            {
                model.ChildCampaignViewModels = _childCampaignServices
                    .GetChildCampaignByMasterId(model.MasterCampaign_Id).Where(t => t.Status == Status.Complete.ToString()).ToList();
            }

            var ttList = _tacticCampaignServices.GetTacticType();
            model.TacticTypeViewModels = model.TacticTypeViewModels.Concat(ttList).ToList();
            model.MetricReachViewModels = _metricReachServices.GetAllMetricReach().ToList();
            model.MetricResponseViewModels = _metricResponseServices.GetAllMetricResponse().ToList();

            return PartialView("TacticCampaignForm", model);
        }

        public ActionResult LoadChildCampaign(TacticCampaignViewModel model)
        {

            if (model.ChildCampaign_Id != 0)
            {
                model.Segments_Id = null;
                model.BusinessGroups_Id = null;
                model.Industries_Id = null;
                model.BusinessLines_Id = null;

                List<ChildCampaign> valueChild =
                    _childCampaignServices.GetChildCampaignById(
                        new ChildCampaignViewModel {Id = model.ChildCampaign_Id});
                foreach (var item in valueChild)
                {
                    if (item.CampaignType == 0)
                    {
                        model.BusinessGroupViewModels = model.BusinessGroupViewModels.Concat(item.BusinessGroups).ToList();
                        model.SegmentViewModels = item.Segments.ToList();
                    }
                    else
                    {
                        model.BusinessGroupViewModels = item.BusinessGroups.ToList();
                        model.SegmentViewModels = model.SegmentViewModels.Concat(item.Segments).ToList();
                    }
                    if (model.Segments_Id != null)
                    {
                        if (model.Segments_Id[0] != 0 && model.Segments_Id[0] != -1)
                        {
                            model.IndustryViewModels = item.Industries.ToList();
                           
                        }
                    }
                    else
                    {
                        model.Industries_Id = null;
                    }
                    if (model.BusinessGroups_Id != null)
                    {
                        if (model.BusinessGroups_Id[0] != 0 && model.BusinessGroups_Id[0] != -1)
                        {
                            model.BusinessLineViewModels = item.BusinessLines.ToList();
                           
                        }
                    }
                    else
                    {
                        model.BusinessLines_Id = null;
                    }
                    model.ThemeViewModels = item.Themes.ToList();
                    model.GeographyViewModels = item.Geographys.ToList();
                    model.MCStartDate = item.StartDate;
                    model.MCEndDate = item.EndDate;
                    model.SubCampaignType = item.CampaignType;
                }
            }
            else
            {
                model.BusinessGroupViewModels = (new[] { new BusinessGroup() });
                model.SegmentViewModels = (new[] { new Segment() });
            }
            model.JourneyStageViewModels = _journeyStageServices.GetJourneyStage().ToList();
            model.MasterViewModels = _masterCampaignServices.GetOrderedMasterCampaign().Where(t => t.Status == Status.Complete.ToString() && t.ChildCampaigns.Where(r => r.Status != Status.Draft.ToString()).ToList().Count != 0).ToList();

            if (model.MasterCampaign_Id != 0)
            {
                model.ChildCampaignViewModels = _childCampaignServices
                    .GetChildCampaignByMasterId(model.MasterCampaign_Id).Where(t => t.Status == Status.Complete.ToString()).ToList();
            }
          
            var ttList = _tacticCampaignServices.GetTacticType();
            model.TacticTypeViewModels = model.TacticTypeViewModels.Concat(ttList).ToList();

            ManageSelectUnselect(model);

            model.MetricReachViewModels = _metricReachServices.GetAllMetricReach().ToList();
            model.MetricResponseViewModels = _metricResponseServices.GetAllMetricResponse().ToList();

            return PartialView("TacticCampaignForm", model);
        }

        //Manage SelectUnselect
        public TacticCampaignViewModel ManageSelectUnselect(TacticCampaignViewModel model)
        {
            if ((model.BusinessGroups_Id != null && (model.BusinessGroupViewModels.ToList().Count > model.BusinessGroups_Id.Length)) || model.BusinessGroups_Id == null || model.BusinessGroups_Id[0]==0) { model.BgSelectUnselect = false; }
            else { model.BgSelectUnselect = true; }
            if ((model.BusinessLines_Id != null && (model.BusinessLineViewModels.ToList().Count > model.BusinessLines_Id.Length)) || model.BusinessLines_Id == null) { model.BlSelectUnselect = false; }
            else { model.BlSelectUnselect = true; }
            if ((model.Segments_Id != null && (model.SegmentViewModels.ToList().Count > model.Segments_Id.Length)) || model.Segments_Id == null || model.Segments_Id[0]==0) { model.SegSelectUnselect = false; }

            else { model.SegSelectUnselect = true; }
            if ((model.Industries_Id != null && (model.IndustryViewModels.ToList().Count > model.Industries_Id.Length)) || model.Industries_Id == null) { model.IndustrySelectUnselect = false; }
            else { model.IndustrySelectUnselect = true; }
            if ((model.Themes_Id != null && (model.ThemeViewModels.ToList().Count > model.Themes_Id.Length)) || model.Themes_Id == null) { model.ThemeSelectUnselect = false; }
            else { model.ThemeSelectUnselect = true; }
            if ((model.Geographys_Id != null && (model.GeographyViewModels.ToList().Count > model.Geographys_Id.Length)) || model.Geographys_Id == null) { model.GeoSelectUnselect = false; }
            else { model.GeoSelectUnselect = true; }

            return model;
        }
        public bool Save(string jsonModel, string button)
        {
            // Deserializing json model to object 
            TacticCampaignViewModel model = new JavaScriptSerializer().Deserialize<TacticCampaignViewModel>(jsonModel);

            if (button == "Save Draft")
            {
                if (model.Id == 0) // insert new record as draft
                {
                    model.Status = Status.Draft.ToString();
                    _tacticCampaignServices.InsertTacticCampaign(model);
                    return true;
                }
                model.Status = Status.Draft.ToString();
                _tacticCampaignServices.Update(model);
                //Update Inheritance
                return true;
            }

            if (isValid(model))
            {
                if (model.Id == 0)
                {
                    model.Status = Status.Complete.ToString();
                    _tacticCampaignServices.InsertTacticCampaign(model);
                    return true;
                }
                else
                {
                    model.Status = Status.Complete.ToString();
                    _tacticCampaignServices.Update(model);
                    return true;
                }
            }
            return false;
        }

        private bool isValid(TacticCampaignViewModel model)
        {
            int errorCounter = 0;
            TacticCampaignReachResponse tacticReachModel = model.TacticCampaignReachResponseViewModels.Where(x => x.MetricType == "Reach").FirstOrDefault();
            TacticCampaignReachResponse tacticResponseModel = model.TacticCampaignReachResponseViewModels.Where(x => x.MetricType == "Response").FirstOrDefault();
            if (model.Id != 0)
            {
                if (model.MasterCampaign_Id == 0) errorCounter++;
                if (model.ChildCampaign_Id == 0) errorCounter++;

                if (Convert.ToDateTime(model.StartDate) > Convert.ToDateTime(model.EndDate)) errorCounter++;
                if (model.TacticDescription == "") errorCounter++;

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
                if (model.MasterCampaign_Id == 0) errorCounter++;
                if (model.ChildCampaign_Id == 0) errorCounter++;
                if (model.BusinessGroups_Id == null) errorCounter++;
                if (model.BusinessLines_Id == null) errorCounter++;
                if (model.Segments_Id == null) errorCounter++;
                if (Convert.ToDateTime(model.StartDate) > Convert.ToDateTime(model.EndDate)) errorCounter++;
                if (model.Name == "") errorCounter++;
                if (model.TacticDescription == "") errorCounter++;

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
            return errorCounter == 0;
        }

        public ActionResult TacticCampaignList()
        {
            ChildCampaignViewModel childCampaignViewModel = new ChildCampaignViewModel();
            return View(childCampaignViewModel);
        }

        [HttpGet]
        public JsonResult GetTacticCampaignList()
        {
            List<TacticCampaignViewModelList> childCampaignList = (from campaign in _tacticCampaignServices.GetTacticCampaign()
                                                                   where campaign.IsActive == true
                                                                   select
                                                                   new TacticCampaignViewModelList
                                                                   {
                                                                       Id = string.Format("T{0}", campaign.Id.ToString("0000000")),
                                                                       Name = campaign.Name,                                                                       
                                                                       InheritStatus = campaign.InheritStatus, 
                                                                       TacticDescription = campaign.TacticDescription,
                                                                       Status = campaign.Status,
                                                                       StartDate = String.Format("{0:dd MMM yyyy}", campaign.StartDate),
                                                                       EndDate = String.Format("{0:dd MMM yyyy}", campaign.EndDate)
                                                                   }
                                                                 ).ToList();
            return Json(childCampaignList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTactciCampaignListByPage([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestmodel)
        {

            var tacticList = _tacticCampaignServices.GetOrderedTacticCampaign().Where(x => x.IsActive == true);

            //var filteredData =  tacticList.Where(_item => _item.Name.ToLower().StartsWith(requestmodel.Search.Value.ToLower()));

            //var result = tacticList.Skip(requestmodel.Start).Take(requestmodel.Length);

            var data = !String.IsNullOrEmpty(requestmodel.Search.Value) ? tacticList.Where(_item => _item.Name.ToLower().StartsWith(requestmodel.Search.Value.ToLower())) : tacticList.Skip(requestmodel.Start).Take(requestmodel.Length);
            List<TacticCampaignViewModelList> tactiCampaignList = (from campaign in data.ToList()
                                                                   where campaign.IsActive == true
                                                                   select
                                                                   new TacticCampaignViewModelList
                                                                   {
                                                                       Id = string.Format("T{0}", campaign.Id.ToString("0000000")),
                                                                       Name = campaign.Name,                                                                       
                                                                       InheritStatus = campaign.InheritStatus,
                                                                       TacticDescription = campaign.TacticDescription,
                                                                       Status = campaign.Status,
                                                                       StartDate = String.Format("{0:dd MMM yyyy}", campaign.StartDate),
                                                                       EndDate = String.Format("{0:dd MMM yyyy}", campaign.EndDate)
                                                                   }
                                                                ).ToList();

            return Json(new DataTablesResponse(requestmodel.Draw, tactiCampaignList, !String.IsNullOrEmpty(requestmodel.Search.Value) ? data.Count() : tacticList.Count(), !String.IsNullOrEmpty(requestmodel.Search.Value) ? data.Count() : tacticList.Count()), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteTacticCampaign(int id)
        {
            var tacticCampaign = _tacticCampaignServices.GetTacticCampaignById(new TacticCampaignViewModel() { Id = id }).FirstOrDefault();
            tacticCampaign.IsActive = false;
            _tacticCampaignServices.Update(tacticCampaign);
            return Json(JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetDigitalTouchpoint(int tacticId)
        {
            CommanResponse commanResponse = new CommanResponse();
            try
            {
                commanResponse.Result = _digitalTouchpoint.GetbyId(tacticId);
                commanResponse.Status = true;
            }
            catch (Exception ex)
            {
                commanResponse.Status = false;
                commanResponse.Message = ex.Message;
            }
            return Json(commanResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddDigitalTouchPoint(List<DigitalTouchPointViewModel> model)
        {
            CommanResponse commanResponse = new CommanResponse();
            try
            {
                commanResponse.Status = true;
                commanResponse.Result = _digitalTouchpoint.Insert(model);
                commanResponse.Message = "Added Successfully";
            }
            catch (Exception ex)
            {
                commanResponse.Status = false;
                commanResponse.Message = ex.Message;
            }
            return Json(commanResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteDigitalPoint(int tacticId)
        {
            CommanResponse commanResponse = new CommanResponse();
            try
            {
                commanResponse.Status = true;
                _digitalTouchpoint.Delete(tacticId);
                commanResponse.Result = _digitalTouchpoint.GetbyId(tacticId);
                commanResponse.Message = "Deleted Successfully";

            }
            catch (Exception ex)
            {
                commanResponse.Status = false;
                commanResponse.Message = ex.Message;
            }
            return Json(commanResponse, JsonRequestBehavior.AllowGet);

        }

        public JsonResult DeleteSingleDigitalPoint(int digitalId, int tacticId)
        {
            CommanResponse commanResponse = new CommanResponse();
            try
            {
                commanResponse.Status = true;
                _digitalTouchpoint.DeleteSingleDigitalPoint(digitalId);
                commanResponse.Result = _digitalTouchpoint.GetbyId(tacticId);
                commanResponse.Message = "Deleted Successfully";

            }
            catch (Exception ex)
            {
                commanResponse.Status = false;
                commanResponse.Message = ex.Message;
            }
            return Json(commanResponse, JsonRequestBehavior.AllowGet);

        }
    }
}