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
            System.Security.Principal.GenericIdentity id = new System.Security.Principal.GenericIdentity(user.UserName);
            var tempUser = new CustomPrincipal(user.UserName);
            tempUser.FirstName = user.FirstName;
            tempUser.LastName = user.LastName;
            tempUser.UserId = user.EntityId;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var userData = serializer.Serialize(tempUser);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                1, user.UserName, 
                DateTime.Now, DateTime.Now.AddMinutes(30), 
                false, userData);
            string ticket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticket);
            context.Response.Cookies.Add(faCookie);
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}