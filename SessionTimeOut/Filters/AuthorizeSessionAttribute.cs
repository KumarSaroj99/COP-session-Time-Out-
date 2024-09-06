using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SessionTimeOut.Filters
{
    using System.Web;
    using System.Web.Mvc;

    namespace YourNamespace.Filters
    {
        public class AuthorizeSessionAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var session = filterContext.HttpContext.Session;

                // Check if the session exists and if the user is logged in
                if (session["LoggedIn"] == null || !(bool)session["LoggedIn"])
                {
                    // If not, redirect to the login page
                    filterContext.Result = new RedirectToRouteResult(
                        new System.Web.Routing.RouteValueDictionary
                        {
                        { "controller", "Account" },
                        { "action", "Login" }
                        });
                }

                base.OnActionExecuting(filterContext);
            }
        }
    }

}