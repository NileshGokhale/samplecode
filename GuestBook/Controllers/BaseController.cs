using System.Web.Mvc;
using GuestBook.Helpers;

namespace GuestBook.Controllers
{
    /// <summary>
    /// Base controller class
    /// </summary>
    [Authorize]
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                SessionHelper.AjaxRequest = filterContext.ActionDescriptor.ActionName;
            }
            base.OnActionExecuting(filterContext);
        }
         
    }
}
