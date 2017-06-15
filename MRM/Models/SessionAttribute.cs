using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace MRM.Models
{
        public class SessionAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {


                if (HttpContext.Current.Session["UserInfo"] == null)
                {
                    FormsAuthentication.SignOut();
                    filterContext.Result =
                   new RedirectToRouteResult(new RouteValueDictionary
                     {
                            { "action", "Index" },
                            { "controller", "Login" },
                            { "returnUrl", filterContext.HttpContext.Request.RawUrl}
                      });

                    return;
                }
            }

        }

 
}