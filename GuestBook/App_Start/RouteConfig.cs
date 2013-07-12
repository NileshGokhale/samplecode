using System.Web.Mvc;
using System.Web.Routing;

namespace GuestBook
{
    /// <summary>
    /// Register routes using this class
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //name: "Blogs",
            //url: "Blogs/{entrydate}",
            //defaults: new { controller = "Blogs", action = "EntryDate" });

            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}