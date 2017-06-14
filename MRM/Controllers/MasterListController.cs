﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.Model;
using MRM.Database.GenericUnitOfWork;
using MRM.Business.Services;
using MRM.Database.Model;

namespace MRM.Controllers
{
    public class MasterListController : Controller
    {

        GenericUnitOfWork dbobject = new GenericUnitOfWork();
        public ActionResult MasterList()
        {
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
            MasterCampaign MasterCampaignObj = new MasterCampaign();
            MasterCampaignObj.MasterCampaigns = (from Mastercampaign in obj.GetMasterCampaign()
                                                 select Mastercampaign)
                            .OrderBy(Mastercampaign => Mastercampaign.Id)
                            .Skip((currentPage - 1) * maxRows)
                            .Take(maxRows).ToList();
            double pageCount = totalCount / maxRows;
            MasterCampaignObj.PageCount = (int)Math.Ceiling(pageCount);
            MasterCampaignObj.CurrentPageIndex = currentPage;
            return MasterCampaignObj;
          
        }


    }
}