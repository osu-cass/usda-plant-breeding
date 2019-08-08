using System.Web.Mvc;

namespace Usda.PlantBreeding.Site.Areas.Maps
{
    public class MapsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Maps";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Maps_default",
                "Maps/{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}