using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.Database.GenericUnitOfWork;
using MRM.Business.Services;
using MRM.Database.Model;
using MRM.ViewModel;

namespace MRM.Controllers
{
    [AllowAnonymous]
    public class MasterListController : Controller
    {

        GenericUnitOfWork dbobject = new GenericUnitOfWork();
        MasterCampaignServices _masterCampaignServices = new MasterCampaignServices();
        ChildCampaignServices _childCampaignServices = new ChildCampaignServices();
        MasterCampaign MasterCampaignObj = new MasterCampaign();
        ChildCampaign childCampaignObj = new ChildCampaign();

        public ActionResult MasterList(string Type, int id = 0)
        {

            ChildCampaignViewModel ccvm = new ChildCampaignViewModel();
            ccvm.MasterCampaignId = id;

            if (Type == "View")
            {
                childCampaignObj.ChildCampaigns = _childCampaignServices.GetChildCampaignByMasterId(ccvm.MasterCampaignId);
                return RedirectToAction("ChildList", "ChildList");
            }
            else if (Type == "Delete")
            {
                bool result = _masterCampaignServices.DeleteMasterCampaign(id);

            }
            return View(this.GetMasterCampaignList(1));

        }


        [HttpPost]
        public ActionResult MasterList(int currentPageIndex)
        {
            return View(this.GetMasterCampaignList(currentPageIndex));
        }


        // GET: MasterList

        private MasterCampaign GetMasterCampaignList(int currentPage)
        {
            MasterCampaignServices obj = new MasterCampaignServices();
            int maxRows = 10;
            int totalCount = obj.GetMasterCampaign().Count();
            //MasterCampaign MasterCampaignObj = new MasterCampaign();
            MasterCampaignObj.MasterCampaigns = (from Mastercampaign in obj.GetMasterCampaign().Where(x=>x.IsActive ).ToList()
                                                     //join fnekfw in obj.GetMasterCampaign() where (Mastercampaign.Id == fnekfw.Geographys) 


                                                 select Mastercampaign)
                            .OrderByDescending(Mastercampaign => Mastercampaign.CreatedDate)
                            .Skip((currentPage - 1) * maxRows)
                            .Take(maxRows).ToList();
            double pageCount = (double)((decimal)obj.GetMasterCampaign().Count() / Convert.ToDecimal(maxRows));
            MasterCampaignObj.PageCount = (int)Math.Ceiling(pageCount);
            MasterCampaignObj.CurrentPageIndex = currentPage;
            return MasterCampaignObj;

        }


    }
}