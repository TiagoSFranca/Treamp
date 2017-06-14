using System.Web;
using System.Web.Optimization;

namespace Presentation
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/JQueryFix.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      //"~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //bundles.Add(new ScriptBundle("~/bundles/Datatable").Include(
            //    ));

            //bundles.Add(new StyleBundle("~/bundles/DatatableCss").Include(
            //    "~/Content/template/vendor/metisMenu/metisMenu.min.css",
            //    "~/Content/template/dist/css/sb-admin-2.css",
            //    "~/Content/template/vendor/font-awesome/css/font-awesome.min.css"));

            //DATATABLE 
            bundles.Add(new StyleBundle("~/datatable/css").Include(
                "~/Content/template/vendor/datatables-plugins/dataTables.bootstrap.css",
                "~/Content/template/vendor/datatables-responsive/dataTables.responsive.css"
                ));

            bundles.Add(new ScriptBundle("~/datatable/js").Include(
                "~/Content/template/vendor/datatables/js/jquery.dataTables.min.js",
                "~/Content/template/vendor/datatables-plugins/dataTables.bootstrap.min.js",
                "~/Content/template/vendor/datatables-responsive/dataTables.responsive.js"
                ));

            //FONT AWESOME
            bundles.Add(new StyleBundle("~/font").Include(
                "~/Content/template/vendor/font-awesome/css/font-awesome.min.css"
                ));

            //JQUERY
            bundles.Add(new ScriptBundle("~/jquery").Include(
                "~/Content/template/vendor/jquery/jquery.min.js"
                ));

            //MASKED INPUT
            bundles.Add(new ScriptBundle("~/maskedInput").Include(
                      "~/Scripts/jquery.maskedinput.js"
                ));

            //BOOTSTRAP CORE
            bundles.Add(new ScriptBundle("~/bootstrap/js").Include(
                "~/Content/template/vendor/bootstrap/js/bootstrap.min.js"
                ));
            bundles.Add(new StyleBundle("~/bootstrap/css").Include(
                "~/Content/template/vendor/bootstrap/css/bootstrap.min.css"
                ));

            //METIS CORE
            bundles.Add(new ScriptBundle("~/metis/js").Include(
                "~/Content/template/vendor/metisMenu/metisMenu.min.js"
                ));
            bundles.Add(new StyleBundle("~/metis/css").Include(
                "~/Content/template/vendor/metisMenu/metisMenu.min.css"
                ));

            //CUSTOM THEME
            bundles.Add(new ScriptBundle("~/theme/custom").Include(
                "~/Content/template/dist/js/sb-admin-2.js"
                ));
            bundles.Add(new StyleBundle("~/theme/custom/css").Include(
                "~/Content/template/dist/css/sb-admin-2.css"
                ));

            //RELOAD MODAL
            bundles.Add(new ScriptBundle("~/reloadModal").Include(
                "~/Scripts/reloadModal.js"
                ));
        }
    }
}
