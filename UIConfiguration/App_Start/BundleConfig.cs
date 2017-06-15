using System.Web;
using System.Web.Optimization;

namespace UIConfiguration
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"
                ));

            // Verwenden Sie die Entwicklungsversion von Modernizr zum Entwickeln und Erweitern Ihrer Kenntnisse. Wenn Sie dann
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"
                ));

            bundles.Add(new ScriptBundle("~/bundles/materialize").Include(
                    "~/Content/Javascript/custom-min.js",
                    "~/Content/Javascript/plugin-min.js",
                    "~/Content/Javascript/materialize.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/gridster").Include(
                    "~/Scripts/jquery.gridster.min.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/css/jquery.gridster.min.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/plugin-min.css",
                      "~/Content/css/custom-min.css",                      
                      "~/Content/css/materialize-min.css"
                 ));

        }
    }
}
