using MRM.Database.GenericUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.Database.Model;

namespace MRM.Controllers
{
    public class TacticListController : Controller
    {
        GenericUnitOfWork dbobject = new GenericUnitOfWork();
        public ActionResult TacticList()
        {
            return View(this.GetTacticCampaignList(1));

        }


        [HttpPost]
        public ActionResult TacticList(int currentPageIndex)
        {
            return View(this.GetTacticCampaignList(currentPageIndex));
        }


        // GET: MasterList

        private TacticCampaign GetTacticCampaignList(int currentPage)
        {
            int maxRows = 10;
            int totalCount = dbobject.GenericRepository<TacticCampaign>().GetAll().ToList().Count();
            TacticCampaign TactiCampaignObj = new TacticCampaign();

            TactiCampaignObj.TacticCampaigns = (from Tacticcampaign in dbobject.GenericRepository<TacticCampaign>().GetAll().ToList()
                                                          select Tacticcampaign)
                            .OrderBy(Mastercampaign => Mastercampaign.Id)
                            .Skip((currentPage - 1) * maxRows)
                            .Take(maxRows).ToList();

            double pageCount = totalCount / maxRows;
            TactiCampaignObj.PageCount = (int)Math.Ceiling(pageCount);
            TactiCampaignObj.CurrentPageIndex = currentPage;
            return TactiCampaignObj;

        }

    }
}