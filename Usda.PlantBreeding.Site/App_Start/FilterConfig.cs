using System.Web;
using System.Web.Mvc;
using Usda.PlantBreeding.Data.Models;

namespace Usda.PlantBreeding.Site
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeRolesAttribute(UserRole.Admin, UserRole.Employee));
        }
    }
}
