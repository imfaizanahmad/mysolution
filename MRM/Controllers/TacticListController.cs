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
    public class TacticListController : Controller
    {
        GenericUnitOfWork dbobject = new GenericUnitOfWork();
        public ActionResult TacticList()
        {
            return View(this.GetTacticCampaignList(1));

        }

        public ActionResult TacticListById(int Id)
        {

            return RedirectToAction("TacticList", "TacticList");

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
            TacticCampaignServices obj = new TacticCampaignServices();
            int totalCount = obj.GetTacticCampaign().Count();
            TacticCampaign TactiCampaignObj = new TacticCampaign();

            TactiCampaignObj.TacticCampaigns = (from Tacticcampaign in obj.GetTacticCampaign()
                                                select Tacticcampaign)
                            .OrderBy(Mastercampaign => Mastercampaign.Id)
                            .Skip((currentPage - 1) * maxRows)
                            .Take(maxRows).ToList();
            double pageCount = (double)((decimal)obj.GetTacticCampaign().Count() / Convert.ToDecimal(maxRows));
            TactiCampaignObj.PageCount = (int)Math.Ceiling(pageCount);
            TactiCampaignObj.CurrentPageIndex = currentPage;
            return TactiCampaignObj;

        }

    }
}