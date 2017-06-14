using MRM.Database.GenericUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.Database.Model;

namespace MRM.Controllers
{
    public class ChildListController : Controller
    {
        GenericUnitOfWork dbobject = new GenericUnitOfWork();
        public ActionResult ChildList()
        {
            return View(this.GetChildCampaignList(1));

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
            int totalCount = dbobject.GenericRepository<MasterCampaign>().GetAll().ToList().Count();
            // MasterCampaignServices obj = new MasterCampaignServices();
            ChildCampaign ChildCampaignObj = new ChildCampaign();

            ChildCampaignObj.ChildCampaigns = (from Childcampaign in dbobject.GenericRepository<ChildCampaign>().GetAll().ToList()
                                                          select Childcampaign)
                            .OrderBy(Mastercampaign => Mastercampaign.Id)
                            .Skip((currentPage - 1) * maxRows)
                            .Take(maxRows).ToList();

            double pageCount = totalCount / maxRows;
            //(double)((decimal)MasterCampaignObj.MasterCampaignViewModels.Count() / Convert.ToDecimal(2));
            ChildCampaignObj.PageCount = (int)Math.Ceiling(pageCount);
            ChildCampaignObj.CurrentPageIndex = currentPage;
            return ChildCampaignObj;

        }


    }
}