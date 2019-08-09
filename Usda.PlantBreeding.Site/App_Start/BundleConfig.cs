using System.Web;
using System.Web.Optimization;

namespace Usda.PlantBreeding.Site
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/noty").Include(
                        "~/node_modules/noty/js/noty/jquery.noty.js",
                        "~/node_modules/noty/js/noty/layouts/top.js",
                        "~/node_modules/noty/js/noty/layouts/center.js",
                        "~/node_modules/noty/js/noty/themes/default.js",
                        "~/node_modules/noty/js/noty/layouts/bootstrap.js",
                        "~/node_modules/bootstrap-multiselect/dist/js/bootstrap-multiselect.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ajax").Include(
                        "~/node_modules/jquery-ajax-unobtrusive/dist/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/node_modules/jquery/dist/jquery.js",
                        "~/node_modules/jquery/external/sizzle/dist/sizzle.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/node_modules/jquery-validation/dist/jquery.validate.js",
                        "~/node_modules/jquery-validation-unobtrusive/dist/jquery.validate.unobtrustive.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/node_modules/bootstrap/dist/js/bootstrap.js",
                      "~/node_modules/respond.js/src/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/search").Include(
                     "~/Scripts/PlantBreedingSearch.js",
                     "~/Scripts/autocomplete/auto-complete.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                "~/node_modules/jquery-ui/ui/*.js",
                "~/node_modules/jquery-ui/ui/effects/*.js",
                "~/node_modules/jquery-ui/ui/widgets/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/offlinejs").Include(
            "~/node_modules/offline-js/offline.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modalform").Include(
                        "~/Scripts/modalform.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap/css").Include(
                "~/node_modules/bootstrap/dist/css/bootstrap.min.css",
                "~/node_modules/bootstrap/dist/css/bootstrap.theme.min.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
              "~/node_modules/jquery-ui/themes/base/core.css",
              "~/node_modules/jquery-ui/themes/base/resizable.css",
              "~/node_modules/jquery-ui/themes/base/selectable.css",
              "~/node_modules/jquery-ui/themes/base/accordion.css",
              "~/node_modules/jquery-ui/themes/base/autocomplete.css",
              "~/node_modules/jquery-ui/themes/base/button.css",
              "~/node_modules/jquery-ui/themes/base/dialog.css",
              "~/node_modules/jquery-ui/themes/base/slider.css",
              "~/node_modules/jquery-ui/themes/base/tabs.css",
              "~/node_modules/jquery-ui/themes/base/datepicker.css",
              "~/node_modules/jquery-ui/themes/base/progressbar.css",
              "~/node_modules/jquery-ui/themes/base/theme.css",
              "~/node_modules/js-autocomplete/auto-complete.css"));


            bundles.Add(new StyleBundle("~/Content/offlinecss").Include(
              "~/node_modules/offline-js/themes/offline-theme-slide.css",
              "~/node_modules/offline-js/themes/offline-language-english.css"));

            bundles.Add(new StyleBundle("~/Content/offlineindicatorcss").Include(
              "~/node_modules/offline-js/themes/offline-theme-slide-indicator.css",
              "~/node_modules/offline-js/themes/offline-language-english-indicator.css"));


            /*bundles.Add(new StyleBundle("~/Content/tablecss").Include(
                    "~/sorter/style.css"
                    ));*/
            bundles.Add(new ScriptBundle("~/bundles/table").Include(
                    "~/node_modules/floatthead/dist/jquery.floatThead.min.js",
                    "~/node_modules/tablesorter/dist/js/jquery.tablesorter.min.js"
                    ));
            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                 "~/node_modules/datatables.net/js/jquery.dataTables.min.js",
                  "~/node_modules/datatables.net-bs/js/dataTables.bootstrap.min.js",
                 "~/node_modules/datatables.net-fixedcolumns/js/dataTables.fixedColumns.min.js",
                 "~/node_modules/datatables.net-fixedheader/js/dataTables.fixedHeader.min.js",
                 "~/Scripts/dataTablesExtensions.js"



             ));
            bundles.Add(new StyleBundle("~/Content/datatablecss").Include(
                           "~/node_modules/datatables.net-bs/css/dataTables.bootstrap.min.css",
                  "~/node_modules/datatables.net-fixedcolumns-bs/css/fixedColumns.bootstrap.min.css",
                 "~/node_modules/datatables.net-fixedheader-bs/css/fixedHeader.bootstrap.min.css"
                   //"~/Content/DataTables-1.10.12/extensions/Responsive/css/responsive.bootstrap.min.css"


                   ));
            bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
                    "~/node_modules/font-awesome/css/font-awesome.min.css"
                    ));
            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = false;
        }
    }
}
