using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Usda.PlantBreeding.Data.Models
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles)
            : base()
        {
            Roles = string.Join(",", roles);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            //if (filterContext.HttpContext.Request.IsAuthenticated)
            //{
            //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
            //        { "controller", "Authorize" },
            //        { "action", "Forbidden" }
            //    });
            //}
            //else 
            //{
            //    base.HandleUnauthorizedRequest(filterContext);
            //    if (filterContext.Result is HttpUnauthorizedResult && !filterContext.HttpContext.Request.IsAuthenticated)
            //    {
            //        filterContext.Result = new RedirectToRouteResult(
            //            new RouteValueDictionary {
            //                { "controller", "Authorize" },
            //                { "action", "Unauthorized" }
            //            });
            //    }
            //}
        }
    }
}
