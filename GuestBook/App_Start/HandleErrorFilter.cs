using System.Web.Mvc;

namespace GuestBook.App_Start
{
    /// <summary>
    /// 
    /// </summary>
    public class HandleErrorFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }
    }
}