using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.ViewModel;
using MRM.Business.Services;
using MRM.Database.Model;
using MRM.Database.GenericRepository;
using System.Web.Script.Serialization;

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
        }

        public ActionResult TacticCampaign(int Id = 0)
        {
            TacticCampaignViewModel tacticvm = new TacticCampaignViewModel();
            tacticvm.JourneyStageViewModels = _journeyStageServices.GetJourneyStage().ToList();
            var ttList = _tacticCampaignServices.GetTacticType().ToList();
            tacticvm.TacticTypeViewModels = tacticvm.TacticTypeViewModels.Concat(ttList).ToList();
            tacticvm.MasterViewModels = _masterCampaignServices.GetOrderedMasterCampaign().Where(t => t.Status == "Complete" && t.ChildCampaigns.Where(r => r.Status != "Save Draft").ToList().Count != 0).ToList();

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

                tacticvm.ChildCampaignViewModels = _childCampaignServices.GetChildCampaignByMasterId(tacticCampaign.ChildCampaigns.MasterCampaigns.Id).Where(t => t.Status == "Complete").ToList();

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
                      //  tacticvm.BusinessGroupViewModels = item.BusinessGroups;

                        tacticvm.BusinessLineViewModels = item.BusinessLines.ToList();
                       // tacticvm.SegmentViewModels = item.Segments;
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

                //TacticCampaign tacticCampaign = _tacticCampaignServices.GetTacticCampaignById(new TacticCampaignViewModel { Id = Id }).First();

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
                    tacticCampaign.InheritStatus = "Complete";
                }

                tacticvm.StatusInheritaceStamp = String.Format("{0:yy}", tacticCampaign.UpdatedDate) + "." + MasterCampaignName + "." + ChildCampaignName + " //" + ((tacticvm.Status == "Complete" && (tacticvm.EndDate < DateTime.Now)) ? "Complete" : (tacticvm.Status == "Save Draft" ? "Draft" : "Active")) +
                                                " // " + String.Format("{0:ddMMyy HH:MM}", tacticCampaign.UpdatedDate);


                tacticvm.MetricReachViewModels = _metricReachServices.GetAllMetricReach().ToList();
                tacticvm.MetricResponseViewModels = _metricResponseServices.GetAllMetricResponse().ToList();
                tacticvm.TacticCampaignReachResponseViewModels = tacticCampaign.TacticCampaignReachResponses.ToList();

                //Update visited date
                if (tacticvm.Status == "Save Draft")
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

            // model.GeographyViewModels = _geographyService.GetGeography();
            model.ThemeViewModels = _themeService.GetTheme().ToList();
            model.JourneyStageViewModels = _journeyStageServices.GetJourneyStage().ToList();
            model.MasterViewModels = _masterCampaignServices.GetOrderedMasterCampaign().Where(t => t.Status == "Complete" && t.ChildCampaigns.Where(r => r.Status != "Save Draft").ToList().Count != 0).ToList();

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
                    //  model.BusinessGroupViewModels = item.BusinessGroups;
                    //  model.SegmentViewModels = item.Segments;
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
                model.ChildCampaignViewModels =
                    _childCampaignServices.GetChildCampaignByMasterId(model.MasterCampaign_Id);
                model.ChildCampaign_Id = model.ChildCampaign_Id;
            }
            else
            {
                model.MasterCampaign_Id = model.MasterCampaign_Id;
                if (model.MasterCampaign_Id != 0)
                {
                    model.ChildCampaignViewModels = _childCampaignServices
                        .GetChildCampaignByMasterId(model.MasterCampaign_Id).Where(t => t.Status == "Complete").ToList();
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

            //   model.BusinessGroupViewModels = _businessgroupService.GetBG();
            model.JourneyStageViewModels = _journeyStageServices.GetJourneyStage().ToList();
            model.MasterViewModels = _masterCampaignServices.GetOrderedMasterCampaign().Where(t => t.Status == "Complete" && t.ChildCampaigns.Where(r => r.Status != "Save Draft").ToList().Count != 0).ToList();

            //if (model.BusinessGroups_Id == null)
            //    model.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
            //else
            //{
            //    List<BusinessLine> businesslist = _businesslineService.GetBusinessLineByBGId(model.BusinessGroups_Id);
            //    model.BusinessLineViewModels = businesslist;
            //}

            // model.SegmentViewModels = _segmentService.GetSegment();
            // model.Segments_Id = model.Segments_Id;
            //List<Industry> lst = _industryService.GetIndustryBySegmentId(model.Segments_Id);
            //model.IndustryViewModels = lst.Where(t => t.IsActive == true);

            //If sub campaign is not defined for corressponding master campaign
         //   List<MasterCampaign> mastercampaignvalues = _masterCampaignServices.GetMasterCampaignById(model.MasterCampaign_Id);
            //if (mastercampaignvalues.Count > 0)
            //{
            //    foreach (var item in mastercampaignvalues)
            //    {
            //        model.IndustryViewModels = item.Industries;
            //        model.BusinessGroupViewModels = item.BusinessGroups;
            //        model.BusinessLineViewModels = item.BusinessLines;
            //        model.SegmentViewModels = item.Segments;
            //        model.ThemeViewModels = item.Themes;
            //        model.GeographyViewModels = item.Geographys;
            //    }

            //}
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
                        .Where(t => t.Status == "Complete").ToList();
                model.ChildCampaign_Id = model.ChildCampaign_Id;
            }
            else
            {
                model.MasterCampaign_Id = model.MasterCampaign_Id;
                if (model.MasterCampaign_Id != 0)
                {
                    model.ChildCampaignViewModels = _childCampaignServices
                        .GetChildCampaignByMasterId(model.MasterCampaign_Id).Where(t => t.Status == "Complete").ToList();
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

            //If sub campaign is not defined for corressponding master campaign
            // List<MasterCampaign> mastercampaignvalues = _masterCampaignServices.GetMasterCampaignById(model.MasterCampaign_Id);
            model.JourneyStageViewModels = _journeyStageServices.GetJourneyStage().ToList();
            model.BusinessGroupViewModels = (new[] { new BusinessGroup() });
            model.SegmentViewModels = (new[] { new Segment() });

            //If sub campaign is defined for corressponding master campaign
            if (model.MasterCampaign_Id != 0)
            {
                List<ChildCampaign> masterChild = _childCampaignServices
                    .GetChildCampaignByMasterId(model.MasterCampaign_Id).Where(t => t.Status == "Complete").ToList();

                if (masterChild.Count > 0)
                {
                    model.ChildCampaignViewModels = masterChild;
                }
            }

            ManageSelectUnselect(model);

            model.MasterViewModels = _masterCampaignServices.GetOrderedMasterCampaign()
                .Where(t => t.Status == "Complete" && t.ChildCampaigns.Where(r => r.Status != "Save Draft").ToList().Count != 0)
                .ToList();
            //model.BusinessGroupViewModels = (new[] { new BusinessGroup() });
            //model.SegmentViewModels = (new[] { new Segment() });

            // model.TacticTypeViewModels = _tacticCampaignServices.GetTacticType();
            if (model.MasterCampaign_Id != 0)
            {
                model.ChildCampaignViewModels = _childCampaignServices
                    .GetChildCampaignByMasterId(model.MasterCampaign_Id).Where(t => t.Status == "Complete").ToList();
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
                    //  model.SegmentViewModels = item.Segments;
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
            model.MasterViewModels = _masterCampaignServices.GetOrderedMasterCampaign().Where(t => t.Status == "Complete" && t.ChildCampaigns.Where(r => r.Status != "Save Draft").ToList().Count != 0).ToList();

            if (model.MasterCampaign_Id != 0)
            {
                model.ChildCampaignViewModels = _childCampaignServices
                    .GetChildCampaignByMasterId(model.MasterCampaign_Id).Where(t => t.Status == "Complete").ToList();
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

            if (button == "Draft")
            {
                if (model.Id == 0) // insert new record as draft
                {
                    model.Status = "Save Draft";
                    _tacticCampaignServices.InsertTacticCampaign(model);
                    return true;
                }
                model.Status = "Save Draft";
                _tacticCampaignServices.Update(model);
                return true;
            }

            if (isValid(model))
            {
                if (model.Id == 0)
                {
                    model.Status = "Complete";
                    _tacticCampaignServices.InsertTacticCampaign(model);
                    return true;
                }
                else
                {
                    model.Status = "Complete";
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

                //if (tacticReachModel.Goal == default(int) || tacticReachModel.Low == default(int) || tacticReachModel.High == default(int))
                //    errorCounter++;
                //if (tacticResponseModel.Goal == default(int) || tacticResponseModel.Low == default(int) || tacticResponseModel.High == default(int))
                //    errorCounter++;

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
                //if (model.Vendor == "") errorCounter++;
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
            _tacticCampaignServices.DeleteLastyearVisited();
            List<TacticCampaignViewModelList> childCampaignList = (from campaign in _tacticCampaignServices.GetTacticCampaign()
                                                                   where campaign.IsActive == true
                                                                   select
                                                                   new TacticCampaignViewModelList
                                                                   {
                                                                       Id = string.Format("T{0}", campaign.Id.ToString("0000000")),
                                                                       Name = campaign.Name,
                                                                       // InheritStatus = (!string.IsNullOrEmpty(campaign.InheritStatus) ? campaign.InheritStatus : (campaign.Status == "Save Draft" ? "Draft" : "Active")),
                                                                       InheritStatus = ((campaign.Status == "Complete" && (campaign.EndDate < DateTime.Now)) ? "Complete" : (campaign.Status == "Save Draft" ? "Draft" : "Active")),
                                                                       TacticDescription = campaign.TacticDescription,
                                                                       Status = campaign.Status == "Save Draft" ? "Draft" : "Active",
                                                                       StartDate = String.Format("{0:dd MMM yyyy}", campaign.StartDate),
                                                                       EndDate = String.Format("{0:dd MMM yyyy}", campaign.EndDate)
                                                                   }
                                                                 ).ToList();
            return Json(childCampaignList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteTacticCampaign(int id)
        {
            var tacticCampaign = _tacticCampaignServices.GetTacticCampaignById(new TacticCampaignViewModel() { Id = id }).FirstOrDefault();
            tacticCampaign.IsActive = false;
            _tacticCampaignServices.Update(tacticCampaign);
            return Json(GetTacticCampaignList(), JsonRequestBehavior.AllowGet);
        }
    }
}