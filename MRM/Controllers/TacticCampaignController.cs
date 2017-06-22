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
        private TacticCampaignServices _tacticCampaignServices = null;
        private ChildCampaignServices _childCampaignServices = null;

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
        }

        // GET: TacticCampaign
        public ActionResult Index()
        {
            return View();
        }


        //public ActionResult TacticCampaign()
        //{
        //  //  if (Session["UserInfo"] == null) { return RedirectToAction("Index", "Home"); }
        //    Tacticvm.IndustryViewModels = _industryService.GetIndustry();
        //    Tacticvm.BusinessGroupViewModels = _businessgroupService.GetBG();
        //    Tacticvm.BusinessLineViewModels = _businesslineService.GetBusinessLine();
        //    Tacticvm.SegmentViewModels = _segmentService.GetSegment();
        //    Tacticvm.GeographyViewModels = _geographyService.GetGeography();
        //    Tacticvm.ThemeViewModels = _themeService.GetTheme();
        //    Tacticvm.ChildCampaignViewModels = _childCampaignServices.GetChildCampaign();
        //    return View(Tacticvm);
        //}

      
        public ActionResult TacticCampaign(int id = 0)
        {
            TacticCampaignViewModel tacticvm = new TacticCampaignViewModel();

            if (id == 0)
            {
                tacticvm.ChildCampaignViewModels = _childCampaignServices.GetChildCampaign();

                tacticvm.BusinessGroupViewModels = (new BusinessGroup[] { new BusinessGroup() });
                tacticvm.SegmentViewModels = (new Segment[] { new Segment() });
                tacticvm.BusinessLineViewModels = (new BusinessLine[] { new BusinessLine() });
                tacticvm.ThemeViewModels = (new Theme[] { new Theme() });
                tacticvm.GeographyViewModels = (new Geography[] { new Geography() });
                tacticvm.IndustryViewModels = (new Industry[] { new Industry() });

                return View(tacticvm);
            }

            //created by Suraj
            if (id != default(int))
            {

                //Default Drop-down bind
                tacticvm.ChildCampaignViewModels = _childCampaignServices.GetChildCampaign();
                tacticvm.IndustryViewModels = _industryService.GetIndustry();
                tacticvm.BusinessGroupViewModels = _businessgroupService.GetBG();
                tacticvm.BusinessLineViewModels = _businesslineService.GetBusinessLine();
                tacticvm.SegmentViewModels = _segmentService.GetSegment();
                tacticvm.ThemeViewModels = _themeService.GetTheme();
                tacticvm.GeographyViewModels = _geographyService.GetGeography();
                List<TacticCampaign> tacticobjlist = _tacticCampaignServices.GetTacticCampaignById(new TacticCampaignViewModel { Id = id });
                foreach (var item in tacticobjlist)
                {
                    //Selected Value bind in drop-down
                    //For Theme
                    int[] SelectedThemes = new int[item.Themes.Count];
                    for (int i = 0; i < item.Themes.Count; i++)
                    {
                        SelectedThemes[i] = item.Themes.ElementAt(i).Id;
                    }
                    tacticvm.Themes_Id = SelectedThemes;

                    //For BusinessGroups

                    int[] SelectedBusinessGroup = new int[item.BusinessGroups.Count];
                    for (int i = 0; i < item.BusinessGroups.Count; i++)
                    {
                        SelectedBusinessGroup[i] = item.BusinessGroups.ElementAt(i).Id;
                    }
                    tacticvm.BusinessGroups_Id = SelectedBusinessGroup;

                    //For BusinessLines
                    int[] SelectedBusinessLine = new int[item.BusinessLines.Count];
                    for (int i = 0; i < item.BusinessLines.Count; i++)
                    {
                        SelectedBusinessLine[i] = item.BusinessLines.ElementAt(i).Id;
                    }
                    tacticvm.BusinessLines_Id = SelectedBusinessLine;
                    //For Segment
                    int[] SelectedSegment = new int[item.Segments.Count];
                    for (int i = 0; i < item.Segments.Count; i++)
                    {
                        SelectedSegment[i] = item.Segments.ElementAt(i).Id;
                    }
                    tacticvm.Segments_Id = SelectedSegment;

                    //For Geography
                    int[] SelectedGeography = new int[item.Geographys.Count];
                    for (int i = 0; i < item.Geographys.Count; i++)
                    {
                        SelectedGeography[i] = item.Geographys.ElementAt(i).Id;
                    }
                    tacticvm.Geographys_Id = SelectedGeography;
                    //For Industry
                    int[] SelectedIndustry = new int[item.Industries.Count];
                    for (int i = 0; i < item.Industries.Count; i++)
                    {
                        SelectedIndustry[i] = item.Industries.ElementAt(i).Id;
                    }
                    tacticvm.Industries_Id = SelectedIndustry;

                    tacticvm.Name = item.Name;
                    tacticvm.TacticDescription = item.TacticDescription;
                    tacticvm.StartDate = Convert.ToString(item.StartDate);
                    tacticvm.EndDate = Convert.ToString(item.EndDate);
                    tacticvm.Year = item.Year;
                    tacticvm.Status = item.Status;


                }

            }

            return View(tacticvm);
        }

        public ActionResult Save(TacticCampaignViewModel model, string button)
        {
            //  if (Session["UserInfo"] == null) { return RedirectToAction("Index", "Home");

            if (button == "Submit")
            {

            }
            else if (button == "Delete")
            { }
            else
            {

            }


            bool result;

            result = _tacticCampaignServices.CreateTacticCampaign(model);
            if (result == true)
            {
                return RedirectToAction("TacticList", "TacticList");
            }
            else
            {
                return RedirectToAction("TacticCampaign", "TacticCampaign");
            }
            return Json("saved!", JsonRequestBehavior.AllowGet);
        }
    }
}