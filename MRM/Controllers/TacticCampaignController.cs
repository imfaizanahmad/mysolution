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
       // private VendorServices _vendorService = null;
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
           // _vendorService = new VendorServices();
            _masterCampaignServices = new MasterCampaignServices();
        }

        public ActionResult TacticCampaign(int Id = 0)
        {
            TacticCampaignViewModel tacticvm = new TacticCampaignViewModel();

            if (Id == 0)
            {
                //tacticvm.VendorViewModels = _vendorService.GetVendor();
                tacticvm.TacticTypeViewModels = _tacticCampaignServices.GetTacticType();
                tacticvm.MasterViewModels = _masterCampaignServices.GetMasterCampaign().Where(t => t.Status == "Complete");
                tacticvm.ChildCampaignViewModels = _childCampaignServices.GetChildCampaign().Where(t => t.Status == "Complete");
                tacticvm.IndustryViewModels = _industryService.GetIndustry();
                tacticvm.BusinessGroupViewModels = _businessgroupService.GetBG();
                tacticvm.BusinessLineViewModels = _businesslineService.GetBusinessLine();
                tacticvm.SegmentViewModels = _segmentService.GetSegment();
                tacticvm.ThemeViewModels = _themeService.GetTheme();
                tacticvm.GeographyViewModels = _geographyService.GetGeography();
                return View(tacticvm);
            }

            if (Id != 0)
            {
                tacticvm.MasterViewModels = _masterCampaignServices.GetMasterCampaign().Where(t => t.Status == "Complete");
                tacticvm.ChildCampaignViewModels = _childCampaignServices.GetChildCampaign().Where(t => t.Status == "Complete");
                tacticvm.TacticTypeViewModels = _tacticCampaignServices.GetTacticType();
                //tacticvm.IndustryViewModels = _industryService.GetIndustry();
                //tacticvm.BusinessGroupViewModels = _businessgroupService.GetBG();
                //tacticvm.BusinessLineViewModels = _businesslineService.GetBusinessLine();
                //tacticvm.SegmentViewModels = _segmentService.GetSegment();
                //tacticvm.ThemeViewModels = _themeService.GetTheme();
                //tacticvm.GeographyViewModels = _geographyService.GetGeography();
                //tacticvm.VendorViewModels = _vendorService.GetVendor();


                TacticCampaign tacticCampaign = _tacticCampaignServices.GetTacticCampaignById(new TacticCampaignViewModel { Id = Id }).First();

                if (tacticCampaign.ChildCampaigns.Id != 0)
                {
                    List<ChildCampaign> childCampaign = _childCampaignServices.GetDDLValuesByChildId(tacticCampaign.ChildCampaigns.Id);
                    foreach (var item in childCampaign)
                    {
                        tacticvm.ThemeViewModels = item.Themes;
                        tacticvm.GeographyViewModels = item.Geographys;
                        tacticvm.BusinessGroupViewModels = item.BusinessGroups;
                        tacticvm.BusinessLineViewModels = item.BusinessLines;
                        tacticvm.SegmentViewModels = item.Segments;
                        tacticvm.IndustryViewModels = item.Industries;

                    }                                                          
                }
                else
                {
                    tacticvm.IndustryViewModels = _industryService.GetIndustry();
                    tacticvm.BusinessGroupViewModels = _businessgroupService.GetBG();
                    tacticvm.BusinessLineViewModels = _businesslineService.GetBusinessLine();
                    tacticvm.SegmentViewModels = _segmentService.GetSegment();
                    tacticvm.ThemeViewModels = _themeService.GetTheme();
                    tacticvm.GeographyViewModels = _geographyService.GetGeography();
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
                Tacticvm.BusinessLineViewModels = _businesslineService.GetBusinessLineByBGId(tacticvm.BusinessGroups_Id);

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
                tacticvm.IndustryViewModels = _industryService.GetIndustryBySegmentId(tacticvm.Segments_Id);

                //if (tacticCampaign.Vendors != null && tacticCampaign.Vendors.Count > 0)
                //{
                //    tacticvm.Vendor_Id = tacticCampaign.Vendors.Select(t => t.Id).ToArray(); ;
                //}

                if (tacticCampaign.TacticTypes != null && tacticCampaign.TacticTypes.Count > 0)
                {
                    tacticvm.TacticType_Id = tacticCampaign.TacticTypes.Select(t => t.Id).ToArray(); ;
                }

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
                //tacticvm.TacticTypeViewModels = tacticCampaign.TacticTypes;
                tacticvm.ReachR1Goal = tacticCampaign.ReachR1Goal;
                tacticvm.ReachR1Low = tacticCampaign.ReachR1Low;
                tacticvm.ReachR1High = tacticCampaign.ReachR1High;
                tacticvm.ReachR11Goal = tacticCampaign.ReachR11Goal;
                tacticvm.ReachR12Low = tacticCampaign.ReachR12Low;
                tacticvm.ReachR13High = tacticCampaign.ReachR13High;
                tacticvm.ResponseR1Goal = tacticCampaign.ResponseR1Goal;
                tacticvm.ResponseR1Low = tacticCampaign.ResponseR1Low;
                tacticvm.ResponseR1High = tacticCampaign.ResponseR1High;
                tacticvm.ResponseR21Goal = tacticCampaign.ResponseR21Goal;
                tacticvm.ResponseR22Low = tacticCampaign.ResponseR22Low;
                tacticvm.ResponseR23High = tacticCampaign.ResponseR23High;
                tacticvm.EfficiencyE1Goal = tacticCampaign.EfficiencyE1Goal;
                tacticvm.EfficiencyE1Low = tacticCampaign.EfficiencyE1Low;
                tacticvm.EfficiencyE1High = tacticCampaign.EfficiencyE1High;
                tacticvm.EfficiencyE11Goal = tacticCampaign.EfficiencyE11Goal;
                tacticvm.EfficiencyE12Low = tacticCampaign.EfficiencyE12Low;
                tacticvm.EfficiencyE13High = tacticCampaign.EfficiencyE13High;
                
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
            List<BusinessLine> businesslist = _businesslineService.GetBusinessLineByBGId(model.BusinessGroups_Id);
            model.BusinessGroupViewModels = _businessgroupService.GetBG();
            model.BusinessGroups_Id = model.BusinessGroups_Id;
            model.BusinessLineViewModels = businesslist;
            model.SegmentViewModels = _segmentService.GetSegment();
            model.TacticTypeViewModels = _tacticCampaignServices.GetTacticType();
            model.GeographyViewModels = _geographyService.GetGeography();
            model.ThemeViewModels = _themeService.GetTheme();
            //model.VendorViewModels = _vendorService.GetVendor();

            if (model.Segments_Id == null)
                model.IndustryViewModels = (new Industry[] { new Industry() });
            else
            {
                List<Industry> lst = _industryService.GetIndustryBySegmentId(model.Segments_Id);
                model.IndustryViewModels = lst;
            }

            //If sub campaign is not defined for corressponding master campaign
            List<MasterCampaign> mastercampaignvalues = _masterCampaignServices.GetMasterCampaignById(model.MasterCampaign_Id);
            if (mastercampaignvalues.Count > 0)
            {
                foreach (var item in mastercampaignvalues)
                {
                    model.IndustryViewModels = item.Industries;
                    model.BusinessGroupViewModels = item.BusinessGroups;
                    model.BusinessLineViewModels = item.BusinessLines;
                    model.SegmentViewModels = item.Segments;
                    model.ThemeViewModels = item.Themes;
                    model.GeographyViewModels = item.Geographys;
                }

            }

            if (model.ChildCampaign_Id != 0)
            {
                List<ChildCampaign>  childCampaign = _childCampaignServices.GetChildCampaignById(new ChildCampaignViewModel { Id = model.ChildCampaign_Id });
                foreach (var item in childCampaign)
                {
                    model.ThemeViewModels = item.Themes;
                    model.GeographyViewModels = item.Geographys;
                    model.BusinessGroupViewModels = item.BusinessGroups;
                    model.BusinessLineViewModels = item.BusinessLines;
                    model.SegmentViewModels = item.Segments;
                    model.IndustryViewModels = item.Industries;
                }
                model.MasterViewModels = _masterCampaignServices.GetMasterCampaign().Where(t => t.Status == "Complete");
                model.MasterCampaign_Id = model.MasterCampaign_Id;
                model.ChildCampaignViewModels = _childCampaignServices.GetChildCampaign().Where(t => t.Status == "Complete");
                model.ChildCampaign_Id = model.ChildCampaign_Id;
            }
            else
            {
                model.MasterViewModels = _masterCampaignServices.GetMasterCampaign().Where(t => t.Status == "Complete");
                model.MasterCampaign_Id = model.MasterCampaign_Id;
                model.ChildCampaignViewModels = model.MasterCampaign_Id != 0 ? _childCampaignServices.GetChildCampaignByMasterId(model.MasterCampaign_Id).Where(t => t.Status == "Complete") : _childCampaignServices.GetChildCampaign().Where(t => t.Status == "Complete");
                model.ChildCampaign_Id = model.ChildCampaign_Id;
            }

            return PartialView("TacticCampaignForm", model);
        }
        public ActionResult LoadIndustry(TacticCampaignViewModel model)
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

            //If sub campaign is not defined for corressponding master campaign
            List<MasterCampaign> mastercampaignvalues = _masterCampaignServices.GetMasterCampaignById(model.MasterCampaign_Id);
            if (mastercampaignvalues.Count > 0)
            {
                foreach (var item in mastercampaignvalues)
                {
                    model.IndustryViewModels = item.Industries;
                    model.BusinessGroupViewModels = item.BusinessGroups;
                    model.BusinessLineViewModels = item.BusinessLines;
                    model.SegmentViewModels = item.Segments;
                    model.ThemeViewModels = item.Themes;
                    model.GeographyViewModels = item.Geographys;
                }

            }
            if (model.ChildCampaign_Id != 0)
            {
                List<ChildCampaign> childCampaign = _childCampaignServices.GetChildCampaignById(new ChildCampaignViewModel { Id = model.ChildCampaign_Id });
                foreach (var item in childCampaign)
                {
                    model.ThemeViewModels = item.Themes;
                    model.GeographyViewModels = item.Geographys;
                    model.IndustryViewModels = item.Industries;
                    model.BusinessGroupViewModels = item.BusinessGroups;
                    model.BusinessLineViewModels = item.BusinessLines;
                    model.SegmentViewModels = item.Segments;
                }
                model.MasterViewModels = _masterCampaignServices.GetMasterCampaign().Where(t => t.Status == "Complete");
                model.MasterCampaign_Id = model.MasterCampaign_Id;
                model.ChildCampaignViewModels = _childCampaignServices.GetChildCampaign().Where(t => t.Status == "Complete");
                model.ChildCampaign_Id = model.ChildCampaign_Id;
            }
            else
            {
              //  model.GeographyViewModels = _geographyService.GetGeography();
              //  model.ThemeViewModels = _themeService.GetTheme();
                model.MasterViewModels = _masterCampaignServices.GetMasterCampaign().Where(t => t.Status == "Complete");
                model.MasterCampaign_Id = model.MasterCampaign_Id;
                model.ChildCampaignViewModels = model.MasterCampaign_Id != 0 ? _childCampaignServices.GetChildCampaignByMasterId(model.MasterCampaign_Id).Where(t => t.Status == "Complete") : _childCampaignServices.GetChildCampaign().Where(t => t.Status == "Complete");
                model.ChildCampaign_Id = model.ChildCampaign_Id;
            }

            model.TacticTypeViewModels = _tacticCampaignServices.GetTacticType();
            //model.VendorViewModels = _vendorService.GetVendor();
            return PartialView("TacticCampaignForm", model);
        }

        public ActionResult LoadMasterCampaign(TacticCampaignViewModel model)
        {

            //If sub campaign is not defined for corressponding master campaign
            List<MasterCampaign> mastercampaignvalues = _masterCampaignServices.GetMasterCampaignById(model.MasterCampaign_Id);

            //If sub campaign is defined for corressponding master campaign
            List<ChildCampaign> masterChild = _childCampaignServices.GetChildCampaignByMasterId(model.MasterCampaign_Id).Where(t=>t.Status == "Complete").ToList();
      
            if (mastercampaignvalues.Count > 0)
            {
                foreach (var item in mastercampaignvalues)
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

            }

            if (masterChild.Count > 0)
            {
                model.ChildCampaignViewModels = masterChild;
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
            }

            model.MasterViewModels = _masterCampaignServices.GetMasterCampaign().Where(t => t.Status == "Complete");
            model.TacticTypeViewModels = _tacticCampaignServices.GetTacticType();
            //model.VendorViewModels = _vendorService.GetVendor();

            return PartialView("TacticCampaignForm", model);
        }

        public ActionResult LoadChildCampaign(TacticCampaignViewModel model)
        {
            
            List<ChildCampaign> valueChild = _childCampaignServices.GetChildCampaignById(new ChildCampaignViewModel { Id = model.ChildCampaign_Id });
            foreach (var item in valueChild)
            {
                model.IndustryViewModels = item.Industries;
                model.BusinessGroupViewModels = item.BusinessGroups;
                model.BusinessLineViewModels = item.BusinessLines;
                model.SegmentViewModels = item.Segments;
                model.ThemeViewModels = item.Themes;
                model.GeographyViewModels = item.Geographys;
            }

            model.MasterViewModels = _masterCampaignServices.GetMasterCampaign().Where(t => t.Status == "Complete");
            model.TacticTypeViewModels = _tacticCampaignServices.GetTacticType();
           // model.VendorViewModels = _vendorService.GetVendor();
            model.ChildCampaignViewModels = _childCampaignServices.GetChildCampaign().Where(t => t.Status == "Complete");

            return PartialView("TacticCampaignForm", model);
        }

        public bool Save(TacticCampaignViewModel model, string button)
        {
            //todo:
            if (button == "Save Draft")
            {
                if (model.Id == 0)// insert new record as draft
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

            if (model.Id != 0)
            {

                if (Convert.ToDateTime(model.StartDate) > Convert.ToDateTime(model.EndDate)) errorCounter++;
                if (model.TacticDescription == "") errorCounter++;


                if ((model.ReachR1Goal == "" || model.ReachR1Low == "" || model.ReachR1High == "") &&
                    (model.ReachR11Goal == "" || model.ReachR12Low == "" || model.ReachR13High == ""))
                    errorCounter++;
                if ((model.ResponseR1Goal == "" || model.ResponseR1High == "" || model.ResponseR1Low == "") &&
                    (model.ResponseR21Goal == "" || model.ResponseR22Low == "" || model.ResponseR23High == ""))
                    errorCounter++;
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
                if (model.Vendor == "") errorCounter++;
                if (Convert.ToDateTime(model.StartDate) > Convert.ToDateTime(model.EndDate)) errorCounter++;
                if (model.Name == "") errorCounter++;
                if (model.TacticDescription == "") errorCounter++;
                //if ((model.ReachR1Goal == "" || model.ReachR1Low == "" || model.ReachR1High == "") &&
                //    (model.ReachR11Goal == "" || model.ReachR12Low == "" || model.ReachR13High == ""))
                //    errorCounter++;
                //if ((model.ResponseR1Goal == "" || model.ResponseR1High == "" || model.ResponseR1Low == "") &&
                //    (model.ResponseR21Goal == "" || model.ResponseR22Low == "" || model.ResponseR23High == ""))
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
                                                                      TacticDescription = campaign.TacticDescription,
                                                                      Status = campaign.Status == "Save Draft" ? "Draft" : "Active",
                                                                      StartDate = String.Format("{0:MM/dd/yyyy}", campaign.StartDate),
                                                                      EndDate = String.Format("{0:MM/dd/yyyy}", campaign.EndDate)
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