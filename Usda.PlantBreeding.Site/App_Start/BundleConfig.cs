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
                        "~/Scripts/Lib/noty/jquery.noty.js",
                        "~/Scripts/Lib/noty/top.js",
                        "~/Scripts/Lib/noty/center.js",
                        "~/Scripts/Lib/noty/default.js",
                        "~/Scripts/Lib/noty/bootstrap.js",
                        "~/Scripts/Lib/noty/bootstrap-multiselect.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery-ajax").Include(
                        "~/Scripts/Lib/jquery-ajax/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Lib/jquery/jquery.js",
                        "~/Scripts/Lib/jquery/sizzle.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/Lib/jquery-validation/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/Lib/bootstrap/bootstrap.js",
                      "~/Scripts/Lib/bootstrap/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/search").Include(
                     "~/Scripts/PlantBreedingSearch.js",
                     "~/Scripts/autocomplete/auto-complete.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").IncludeDirectory("~/Scripts/Lib/jquery-ui", "*.js", true));

            /*.Include(
            "~/node_modules/jquery-ui/ui/*.js",
            "~/node_modules/jquery-ui/ui/effects/*.js",
            "~/node_modules/jquery-ui/ui/widgets/*.js"));*/

            bundles.Add(new ScriptBundle("~/bundles/offlinejs").Include(
            "~/Scripts/Lib/offline-js/offline.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modalform").Include(
                        "~/Scripts/modalform.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap/css").Include(
                "~/Stylesheets/Lib/bootstrap/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
              "~/Stylesheets/Lib/base/core.css",
              "~/Stylesheets/Lib/base/resizable.css",
              "~/Stylesheets/Lib/base/selectable.css",
              "~/Stylesheets/Lib/base/accordion.css",
              "~/Stylesheets/Lib/base/autocomplete.css",
              "~/Stylesheets/Lib/base/button.css",
              "~/Stylesheets/Lib/base/dialog.css",
              "~/Stylesheets/Lib/base/slider.css",
              "~/Stylesheets/Lib/base/tabs.css",
              "~/Stylesheets/Lib/base/datepicker.css",
              "~/Stylesheets/Lib/base/progressbar.css",
              "~/Stylesheets/Lib/base/theme.css",
              "~/Stylesheets/Lib/base/auto-complete.css"));


            bundles.Add(new StyleBundle("~/Content/offlinecss").Include(
              "~/Stylesheets/Lib/offline/offline-theme-slide.css",
              "~/Stylesheets/Lib/offline/offline-language-english.css"));

            bundles.Add(new StyleBundle("~/Content/offlineindicatorcss").Include(
              "~/Stylesheets/Lib/offline/offline-theme-slide-indicator.css",
              "~/Stylesheets/Lib/offline/offline-language-english-indicator.css"));


            /*bundles.Add(new StyleBundle("~/Content/tablecss").Include(
                    "~/sorter/style.css"
                    ));*/
            bundles.Add(new ScriptBundle("~/bundles/table").Include(
                    "~/Scripts/Lib/table/jquery.floatThead.min.js",
                    "~/Scripts/Lib/table/jquery.tablesorter.min.js"
                    ));
            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                 "~/Scripts/Lib/datatable/jquery.dataTables.min.js",
                  "~/Scripts/Lib/datatable/dataTables.bootstrap.min.js",
                 "~/Scripts/Lib/datatable/dataTables.fixedColumns.min.js",
                 "~/Scripts/Lib/datatable/dataTables.fixedHeader.min.js",
                 "~/Scripts/dataTablesExtensions.js"



             ));
            bundles.Add(new StyleBundle("~/Content/datatablecss").Include(
                           "~/Stylesheets/Lib/datatable/dataTables.bootstrap.min.css",
                  "~/Stylesheets/Lib/datatable/fixedColumns.bootstrap.min.css",
                 "~/Stylesheets/Lib/datatable/fixedHeader.bootstrap.min.css"
                   //"~/Content/DataTables-1.10.12/extensions/Responsive/css/responsive.bootstrap.min.css"


                   ));
            bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
                    "~/Stylesheets/Lib/fontawesome/font-awesome.min.css"
                    ));
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
