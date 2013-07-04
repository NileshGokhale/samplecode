using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
namespace GuestBook.Security
{
    public static class FormsAuthHelper
    {
        public static void SetAuthTicket(string userName)
        {
            FormsAuthentication.RedirectFromLoginPage(userName, false);
            FormsAuthentication.SetAuthCookie(userName, false);
        }

        public static void Logout()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}