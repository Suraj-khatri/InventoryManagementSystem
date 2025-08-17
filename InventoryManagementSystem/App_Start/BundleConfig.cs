using System.Web.Optimization;
namespace InventoryManagementSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/chartjs").Include(
                "~/Scripts/Chart.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/colorPick").Include(
                "~/Scripts/colorPick.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                                          "~/Scripts/bootstrap-toggle.min.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/colorPick.min.css"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/adminLte").Include(
                "~/Content/DataTables-1.10.18/js/jquery.dataTables.min.js",
                "~/Content/DataTables-1.10.18/js/dataTables.bootstrap.min.js",
                    "~/Content/admin-lte/js/adminlte.js",
                    "~/Scripts/jquery.slimscroll.js",
                    "~/Scripts/select2.min.js",
                    "~/Scripts/sweetalert.min.js",
                    "~/Scripts/printThis.js",
                    "~/Scripts/bootstrap-notify.min.js",
                    "~/Content/JQueryTreeFilter/jquery.treefilter-0.1.0.js"
                ));
            bundles.Add(new StyleBundle("~/Content/adminLte").Include(
                    "~/Content/select2.min.css",
                    "~/Content/DataTables-1.10.18/css/dataTables.bootstrap.min.css",
                    "~/Content/admin-lte/css/AdminLTE.min.css",
                    "~/Content/admin-lte/css/skins/skin-blue.css",
                    "~/Content/animate.min.css",
                    "~/Content/font-awesome.min.css",
                    "~/Content/Site.css",
                    "~/Content/bootstrap-toggle.min.css",
                    "~/Content/JQueryTreeFilter/jquery.treefilter.css"
                ));
        }
    }
}
