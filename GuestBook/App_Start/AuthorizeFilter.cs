using System.Web.Mvc;

namespace GuestBook.App_Start
{
    /// <summary>
    /// Custom authorize filter
    /// </summary>
    public class AuthorizeFilter : AuthorizeAttribute//, IAuthorizationFilter
    {
        /// <summary>
        /// Processes HTTP requests that fail authorization.
        /// </summary>
        /// <param name="filterContext">Encapsulates the information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute" />. The <paramref name="filterContext" /> object contains the controller, HTTP context, request context, action result, and route data.</param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            bool skipAuth = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);

            if (skipAuth) return;
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}