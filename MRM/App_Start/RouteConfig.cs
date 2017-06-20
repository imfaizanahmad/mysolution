using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MRM
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapMvcAttributeRoutes();


            //routes.MapRoute(
            //        name: "MasterCampaign",
            //        url: "MasterCampaign",
            //        defaults: new { controller = "MasterCampaign", action = "MasterCampaign" }
            //);

            //routes.MapRoute(
            //        name: "SubCampaign",
            //        url: "ChildCampaign",
            //        defaults: new { controller = "ChildCampaign", action = "ChildCampaign" }
            //);

            //routes.MapRoute(
            //        name: "Tactic",
            //        url: "TacticCampaign",
            //        defaults: new { controller = "TacticCampaign", action = "TacticCampaign" }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

           

        }
    }
}
