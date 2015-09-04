using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bazik
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("IndexRanger", "bazik", new { controller = "IndexRanger", action = "ShowDashboard" });
            routes.MapRoute("IndexRanger-log", "bazik/loguj", new { controller = "IndexRanger", action = "Log" });
            routes.MapRoute("IndexRanger-request", "bazik/request", new { controller = "IndexRanger", action = "Request" });
            routes.MapRoute("IndexRanger-index", "bazik/index-stats", new { controller = "IndexRanger", action = "IndexStats" });
            routes.MapRoute("IndexRanger-top", "bazik/top", new { controller = "IndexRanger", action = "TopQueries" });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}", // URL with parameters
                new { controller = "IndexRanger", action = "ShowDashboard" } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}
