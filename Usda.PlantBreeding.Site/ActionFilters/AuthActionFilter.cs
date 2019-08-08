using System.Linq;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using System.Web.Routing;
using BSGUtils;
using System;
using System.Web;

namespace Usda.PlantBreeding.Site.ActionFilters
{

    public class AuthActionFilters : ActionFilterAttribute
    {


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sandbox = Properties.Settings.Default.SandboxName;
            var user = HttpContext.Current.User.Identity;
            var urlparams = filterContext.HttpContext.Request.Params;
            
            var userPreference = SessionManager.UserPreference;

            if (!user.IsAuthenticated && user.Name != string.Empty && sandbox.ToLower() != "dev")
            {
                string url = filterContext.HttpContext.Request.Url.ToString();
                SessionManager.RedirectUrl = url;
                SessionManager.NewSession = true;
                filterContext.Result = new RedirectToRouteResult(
                   new RouteValueDictionary(new
                   {
                       controller = "Authorize",
                       action = "Index",
                       area = string.Empty
                   })
                   );
            }

        }
    }
}