using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using DTO;
using System.Web.Script.Serialization;
namespace GuestBook.Security
{
    public static class FormsAuthHelper
    {
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

        public static void Logout()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        public static CustomPrincipal GetPrincipal(User user)
        {
            return new CustomPrincipal(user.UserName) { FirstName = user.FirstName, LastName = user.LastName, UserId = user.EntityId };
        }
    }
}