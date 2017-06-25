using MRM.Database.GenericUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.Database.Model;
using MRM.Business.Services;
using MRM.ViewModel;

namespace MRM.Controllers
{
    [AllowAnonymous]
    public class ChildListController : Controller
    {
        GenericUnitOfWork dbobject = new GenericUnitOfWork();
        TacticCampaignServices _tacticCampaignServices = new TacticCampaignServices();
        ChildCampaignServices _childCampaignService = new ChildCampaignServices();
        TacticCampaign objtactic = new TacticCampaign();
        public ActionResult ChildList(string Type, int id = 0)
        {
            TacticCampaignViewModel tcvm = new TacticCampaignViewModel();
            tcvm.ChildCampaign_Id = id;
            if (Type == "View")
            {
                objtactic.TacticCampaigns = _tacticCampaignServices.GetTacticBySubCampaignId(tcvm);
                return RedirectToAction("TacticList", "TacticList");
            }
            else if (Type == "Delete")
            {
                bool result = _childCampaignService.DeleteSubCampaign(id);

            }

            return View(this.GetChildCampaignList(1));

        }

        public ActionResult ChildListById(int Id = 0)
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
            int maxRows = 2;
            ChildCampaignServices obj = new ChildCampaignServices();
            int totalCount = obj.GetChildCampaign().Count();
            ChildCampaign ChildCampaignObj = new ChildCampaign();
            ChildCampaignObj.ChildCampaigns = (from Childcampaign in obj.GetChildCampaign().Where(x => x.IsActive)
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