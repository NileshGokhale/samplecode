using System.Web;
using System.Web.Mvc;
using GuestBook.App_Start;

namespace GuestBook
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AuthorizeFilter());
            filters.Add(new HandleErrorFilter());
        }
    }
}