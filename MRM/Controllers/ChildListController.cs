using MRM.Database.GenericUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.Database.Model;
using MRM.Business.Services;

namespace MRM.Controllers
{
    [AllowAnonymous]
    public class ChildListController : Controller
    {
        GenericUnitOfWork dbobject = new GenericUnitOfWork();
        public ActionResult ChildList()
        {
            return View(this.GetChildCampaignList(1));

        }

        public ActionResult ChildListById(int Id=0)
        {
            
            return RedirectToAction("ChildList", "ChildList");

        }


        [HttpPost]
        public ActionResult ChildList(int currentPageIndex)
        {
            return View(this.GetChildCampaignList(currentPageIndex));
        }


        // GET: MasterList

        private ChildCampaign GetChildCampaignList(int currentPage)
        {
            int maxRows = 10;
            ChildCampaignServices obj = new ChildCampaignServices();
            int totalCount = obj.GetChildCampaign().Count();
            ChildCampaign ChildCampaignObj = new ChildCampaign();
            ChildCampaignObj.ChildCampaigns = (from Childcampaign in obj.GetChildCampaign()
                                               select Childcampaign)
                            .OrderByDescending(Mastercampaign => Mastercampaign.CreatedDate)
                            .Skip((currentPage - 1) * maxRows)
                            .Take(maxRows).ToList();
            double pageCount = (double)((decimal)obj.GetChildCampaign().Count() / Convert.ToDecimal(maxRows));
            ChildCampaignObj.PageCount = (int)Math.Ceiling(pageCount);
            ChildCampaignObj.CurrentPageIndex = currentPage;
            return ChildCampaignObj;

        }


    }
}