using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MRM.Controllers
{
    [AllowAnonymous]
    public class MasterDataController : Controller
    {
        // GET: MasterData
        public ActionResult MasterData()
        {
            return View();
        }
    }
}