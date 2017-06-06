using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MRM.Controllers
{
    public class CampaignFormController : Controller
    {
        // GET: CampaignForm
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MasterCampaign()
        {
            return View();
        }
        public ActionResult ChildCampaign()
        {
            return View();
        }
        public ActionResult TacticCampaign()
        {
            return View();
        }

    }
}