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


        public ActionResult ChildCampaign(int id=0)
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
            //foreach (var a in Childvm.MasterViewModels)
            //{

            //    a.First(x => x.Id == id).Selected = true;

            //}
            

            List<MasterCampaign> lst = _masterCampaignServices.GetMasterCampaignById(new MasterCampaignViewModel { Id = id });            
            foreach (var item in lst)
            {
                Childvm.IndustryViewModels = item.Industries;
                Childvm.BusinessGroupViewModels = item.BusinessGroups;
                Childvm.BusinessLineViewModels = item.BusinessLines;
                Childvm.SegmentViewModels = item.Segments;
                Childvm.ThemeViewModels = item.Themes;
                Childvm.GeographyViewModels = item.Geographys;
            }

           
            return View(Childvm);
        }
    }
}