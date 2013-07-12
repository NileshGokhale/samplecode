using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DataAccessObjects;
using System.Web.Security;
using GuestBook.Security;
using System.Web.Script.Serialization;

namespace GuestBook
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    /// <summary>
    /// App start
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Handles the AuthenticateRequest event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            //if (authCookie == null || authCookie.Value == string.Empty)
            //    return;

            //FormsAuthenticationTicket ticket;
            //try
            //{
            //    ticket = FormsAuthentication.Decrypt(authCookie.Value);
            //    JavaScriptSerializer serializer = new JavaScriptSerializer();
            //    CustomPrincipal principal = serializer.Deserialize<CustomPrincipal>(ticket.UserData);
            //    HttpContext.Current.User = principal;
            //}
            //catch
            //{
            //    return;
            //}

        }

        /// <summary>
        /// Handles the PostAuthenticateRequest event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null || authCookie.Value == string.Empty)
                return;

            FormsAuthenticationTicket ticket;
            try
            {
                ticket = FormsAuthentication.Decrypt(authCookie.Value);
                var serializer = new JavaScriptSerializer();
                var user = serializer.Deserialize<User>(ticket.UserData);
                CustomPrincipal newUser = FormsAuthHelper.GetPrincipal(user);

                HttpContext.Current.User = newUser;
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}