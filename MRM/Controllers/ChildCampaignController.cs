using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.Business.Services;
using MRM.Database.Model;
using MRM.Database.GenericRepository;
using MRM.ViewModel;
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

        // GET: ChildCampaign
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult ChildCampaign()
        //{
        //    ChildCampaignViewModel Childvm = new ChildCampaignViewModel();
        //    Childvm.MasterViewModels = _masterCampaignServices.GetMasterCampaign();

        //    Childvm.BusinessGroupViewModels = (new BusinessGroup[] { new BusinessGroup() });
        //    Childvm.SegmentViewModels = (new Segment[] { new Segment() });
        //    Childvm.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
        //    Childvm.ThemeViewModels = (new Theme[] { new Theme() });
        //    Childvm.GeographyViewModels = (new Geography[] { new Geography() });
        //    Childvm.IndustryViewModels = (new Industry[] { new Industry() });

        //    return View(Childvm);
        //}

        [HttpPost]
        public ActionResult Save(ChildCampaignViewModel model, string button)
        {
            //  if (Session["UserInfo"] == null) { return RedirectToAction("Index", "Home"); }

            if (button == "Submit")
            {

            }
            else if (button == "Delete")
            { }
            else
            {

            }


            bool result;
            result=  _childCampaignServices.CreateChildCampaign(model);
            if (result == true)
            {
                return RedirectToAction("ChildList", "ChildList");
            }
            else
            {
                return RedirectToAction("ChildCampaign", "ChildCampaign");
            }
            // return Json("saved!", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChildCampaign(int[] BusinessGroups_Id, int[] Segments_Id, int id = 0)
        {
            ChildCampaignViewModel Childvm = new ChildCampaignViewModel();

            if (id == 0)
            {
                Childvm.MasterViewModels = _masterCampaignServices.GetMasterCampaign();

                Childvm.BusinessGroupViewModels = (new BusinessGroup[] { new BusinessGroup() });
                Childvm.SegmentViewModels = (new Segment[] { new Segment() });
                Childvm.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
                Childvm.ThemeViewModels = (new Theme[] { new Theme() });
                Childvm.GeographyViewModels = (new Geography[] { new Geography() });
                Childvm.IndustryViewModels = (new Industry[] { new Industry() });

                return View(Childvm);
            }
            Childvm.MasterViewModels = _masterCampaignServices.GetMasterCampaign();
           
            //Created By suraj
            if (id != default(int))
            {
                //Default Drop-down bind
                Childvm.IndustryViewModels = _industryService.GetIndustry();
                Childvm.BusinessGroupViewModels = _businessgroupService.GetBG();
                Childvm.BusinessLineViewModels = _businesslineService.GetBusinessLine();
                Childvm.SegmentViewModels = _segmentService.GetSegment();
                Childvm.ThemeViewModels = _themeService.GetTheme();
                Childvm.GeographyViewModels = _geographyService.GetGeography();
               
                List<ChildCampaign> childobjlist = _childCampaignServices.GetChildCampaignById(new ChildCampaignViewModel { Id = id });
                foreach (var item in childobjlist)
                {
                   //Selected Value bind in drop-down
                    //For Theme
                    int[] SelectedThemes = new int[item.Themes.Count];
                    for (int i = 0; i < item.Themes.Count; i++)
                    {
                        SelectedThemes[i] = item.Themes.ElementAt(i).Id;
                    }
                    Childvm.Themes_Id = SelectedThemes;

                    //For BusinessGroups

                    int[] SelectedBusinessGroup = new int[item.BusinessGroups.Count];
                    for (int i = 0; i < item.BusinessGroups.Count; i++)
                    {
                        SelectedBusinessGroup[i] = item.BusinessGroups.ElementAt(i).Id;
                    }
                    Childvm.BusinessGroups_Id = SelectedBusinessGroup;

                    //For BusinessLines
                    int[] SelectedBusinessLine = new int[item.BusinessLines.Count];
                    for (int i = 0; i < item.BusinessLines.Count; i++)
                    {
                        SelectedBusinessLine[i] = item.BusinessLines.ElementAt(i).Id;
                    }
                    Childvm.BusinessLines_Id = SelectedBusinessLine;
                    //For Segment
                    int[] SelectedSegment = new int[item.Segments.Count];
                    for (int i = 0; i < item.Segments.Count; i++)
                    {
                        SelectedSegment[i] = item.Segments.ElementAt(i).Id;
                    }
                    Childvm.Segments_Id = SelectedSegment;

                    //For Geography
                    int[] SelectedGeography = new int[item.Geographys.Count];
                    for (int i = 0; i < item.Geographys.Count; i++)
                    {
                        SelectedGeography[i] = item.Geographys.ElementAt(i).Id;
                    }
                    Childvm.Geographys_Id = SelectedGeography;
                    //For Industry
                    int[] SelectedIndustry = new int[item.Industries.Count];
                    for (int i = 0; i < item.Industries.Count; i++)
                    {
                        SelectedIndustry[i] = item.Industries.ElementAt(i).Id;
                    }
                    Childvm.Industries_Id = SelectedIndustry;


                    Childvm.Name = item.Name;
                    Childvm.CampaignDescription = item.CampaignDescription;
                    Childvm.MarketingInfluenceLeads = item.MarketingInfluenceLeads;
                    Childvm.MarketingGeneratedLeads = item.MarketingGeneratedLeads;
                    Childvm.Budget = item.Budget;
                    Childvm.StartDate = Convert.ToString(item.StartDate);
                    Childvm.EndDate = Convert.ToString(item.EndDate);
                    Childvm.MarketingInfluenceOpportunity = item.MarketingInfluenceOpportunity;
                    Childvm.MarketingGeneratedOpportunity = item.MarketingGeneratedOpportunity;
                    Childvm.Spend = item.Spend;
                    Childvm.Id = item.Id;
                    Childvm.Status = item.Status;


                }
                
               
            }


            if (BusinessGroups_Id == null)
            {
                Childvm.BusinessGroupViewModels = _businessgroupService.GetBG();
                Childvm.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
                Childvm.SegmentViewModels = _segmentService.GetSegment();
                Childvm.IndustryViewModels = (new Industry[] { new Industry() });
                Childvm.GeographyViewModels = _geographyService.GetGeography();
                Childvm.ThemeViewModels = _themeService.GetTheme();
                return View(Childvm);
            }

            if (BusinessGroups_Id != null)
            {
                List<BusinessLine> businesslist = _businesslineService.GetBusinessLineByBGId(BusinessGroups_Id);
                Childvm.BusinessGroupViewModels = _businessgroupService.GetBG();
                Childvm.BusinessGroups_Id = BusinessGroups_Id;
                Childvm.BusinessLineViewModels = businesslist;
                Childvm.SegmentViewModels = _segmentService.GetSegment();
                if (Segments_Id == null)
                    Childvm.IndustryViewModels = (new Industry[] { new Industry() });
                else
                {
                    List<Industry> Industrylst = _industryService.GetIndustryBySegmentId(Segments_Id);
                    Childvm.IndustryViewModels = Industrylst;
                }
                Childvm.GeographyViewModels = _geographyService.GetGeography();
                Childvm.ThemeViewModels = _themeService.GetTheme();
                return View(Childvm);
            }
            else if (Segments_Id != null)
            {
                Childvm.BusinessGroupViewModels = _businessgroupService.GetBG();
                if (BusinessGroups_Id == null)
                    Childvm.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
                else
                {
                    List<BusinessLine> businesslist = _businesslineService.GetBusinessLineByBGId(BusinessGroups_Id);
                    Childvm.BusinessLineViewModels = businesslist;
                }
                Childvm.SegmentViewModels = _segmentService.GetSegment();
                Childvm.Segments_Id = Segments_Id;
                List<Industry> Industrylst = _industryService.GetIndustryBySegmentId(Segments_Id);
                Childvm.IndustryViewModels = Industrylst;
                Childvm.GeographyViewModels = _geographyService.GetGeography();
                Childvm.ThemeViewModels = _themeService.GetTheme();
                return View(Childvm);
            }

            return View(Childvm);
        }
    }
}