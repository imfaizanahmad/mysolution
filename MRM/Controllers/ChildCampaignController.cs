using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRM.Database.Model;
using MRM.Model;

namespace MRM.Controllers
{
    public class ChildCampaignController : Controller
    {

        // GET: ChildCampaign
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChildCampaign()
        {
            return View();
        }


        public JsonResult Save()
        {
            return Json("saved!", JsonRequestBehavior.AllowGet);
        }
    }
}