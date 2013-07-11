using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuestBook.Helpers;

namespace GuestBook.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
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
