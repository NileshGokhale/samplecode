using System;
using System.Web;
using System.Web.Security;
using System.Web.Script.Serialization;
using DataAccessObjects;

namespace GuestBook.Security
{
    /// <summary>
    /// Helper class for forms authenticatio
    /// </summary>
    public static class FormsAuthHelper
    {
        /// <summary>
        /// Sets the auth ticket.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="context">The context.</param>
        public static void SetAuthTicket(User user, HttpContextBase context)
        {
            var id = new System.Security.Principal.GenericIdentity(user.UserName);
            var tempUser = GetPrincipal(user);
            var serializer = new JavaScriptSerializer();
            var userData = serializer.Serialize(user);
            var authTicket = new FormsAuthenticationTicket(
                1, user.UserName,
                DateTime.Now, DateTime.Now.AddMinutes(30),
                false, userData);
            var ticket = FormsAuthentication.Encrypt(authTicket);
            var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticket);
            context.Response.Cookies.Add(faCookie);
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        public static void Logout()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        /// <summary>
        /// Gets the principal.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static CustomPrincipal GetPrincipal(User user)
        {
            return new CustomPrincipal(user.UserName) { FirstName = user.FirstName, LastName = user.LastName, UserId = user.EntityId };
        }
    }
}