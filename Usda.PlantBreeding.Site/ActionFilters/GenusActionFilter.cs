using System.Linq;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.DataAccess;
using Usda.PlantBreeding.Data.Models;
using System.Web.Routing;
using BSGUtils;

namespace Usda.PlantBreeding.Site.ActionFilters
{
    public class GenusActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (SessionManager.RedirectUrl.IsNullOrWhiteSpace()) {
                string url = filterContext.HttpContext.Request.Url.AbsolutePath.ToString();
                SessionManager.RedirectUrl = url;
            }
           
            if (SessionManager.Genus == null && !SessionManager.NewSession.HasValue)
            {
                UrlHelper helper = new UrlHelper(filterContext.RequestContext);
                filterContext.Result = new RedirectToRouteResult(
                 new RouteValueDictionary(new
                        {
                            controller = "Home",
                            action = "Index",
                            firstLoad = "false",
                            area = string.Empty
                        })
                 );

            }
            else
            {
                base.OnActionExecuting(filterContext);
            }

        }
    }
}