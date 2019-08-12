using System.Web;
using System.Web.Optimization;

namespace Usda.PlantBreeding.Site
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/noty")
                .Include(
                        "~/Scripts/noty/jquery.noty.js",
                        "~/Scripts/noty/top.js",
                        "~/Scripts/noty/center.js",
                        "~/Scripts/noty/default.js",
                        "~/Scripts/noty/bootstrap.js",
                        "~/Scripts/noty/bootstrap-multiselect.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery-ajax").Include(
                        "~/Scripts/jquery-ajax/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery/jquery.js",
                        "~/Scripts/jquery/sizzle.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery-validation/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap/bootstrap.js",
                      "~/Scripts/bootstrap/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/search").Include(
                     "~/Scripts/PlantBreedingSearch.js",
                     "~/Scripts/autocomplete/auto-complete.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").IncludeDirectory("~/Scripts/jquery-ui", "*.js", true));

            /*.Include(
            "~/node_modules/jquery-ui/ui/*.js",
            "~/node_modules/jquery-ui/ui/effects/*.js",
            "~/node_modules/jquery-ui/ui/widgets/*.js"));*/

            bundles.Add(new ScriptBundle("~/bundles/offlinejs").Include(
            "~/Scripts/offline-js/offline.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modalform").Include(
                        "~/Scripts/modalform.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap/css").Include(
                "~/Stylesheets/bootstrap/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
              "~/Stylesheets/base/core.css",
              "~/Stylesheets/base/resizable.css",
              "~/Stylesheets/base/selectable.css",
              "~/Stylesheets/base/accordion.css",
              "~/Stylesheets/base/autocomplete.css",
              "~/Stylesheets/base/button.css",
              "~/Stylesheets/base/dialog.css",
              "~/Stylesheets/base/slider.css",
              "~/Stylesheets/base/tabs.css",
              "~/Stylesheets/base/datepicker.css",
              "~/Stylesheets/base/progressbar.css",
              "~/Stylesheets/base/theme.css",
              "~/Stylesheets/base/auto-complete.css"));


            bundles.Add(new StyleBundle("~/Content/offlinecss").Include(
              "~/Stylesheets/offline/offline-theme-slide.css",
              "~/Stylesheets/offline/offline-language-english.css"));

            bundles.Add(new StyleBundle("~/Content/offlineindicatorcss").Include(
              "~/Stylesheets/offline/offline-theme-slide-indicator.css",
              "~/Stylesheets/offline/offline-language-english-indicator.css"));


            /*bundles.Add(new StyleBundle("~/Content/tablecss").Include(
                    "~/sorter/style.css"
                    ));*/
            bundles.Add(new ScriptBundle("~/bundles/table").Include(
                    "~/Scripts/table/jquery.floatThead.min.js",
                    "~/Scripts/table/jquery.tablesorter.min.js"
                    ));
            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                 "~/Scripts/datatable/jquery.dataTables.min.js",
                  "~/Scripts/datatable/dataTables.bootstrap.min.js",
                 "~/Scripts/datatable/dataTables.fixedColumns.min.js",
                 "~/Scripts/datatable/dataTables.fixedHeader.min.js",
                 "~/Scripts/dataTablesExtensions.js"



             ));
            bundles.Add(new StyleBundle("~/Content/datatablecss").Include(
                           "~/Stylesheets/datatable/dataTables.bootstrap.min.css",
                  "~/Stylesheets/datatable/fixedColumns.bootstrap.min.css",
                 "~/Stylesheets/datatable/fixedHeader.bootstrap.min.css"
                   //"~/Content/DataTables-1.10.12/extensions/Responsive/css/responsive.bootstrap.min.css"


                   ));
            bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
                    "~/Stylesheets/fontawesome/font-awesome.min.css"
                    ));
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
