using System.Web;
using System.Web.Optimization;

namespace ProductCatalogTask
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/twitter-bootstrap/js/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                      "~/Content/popper.js/umd/popper.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrapselect").Include(
                      "~/Content/bootstrap-select/js/bootstrap-select.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/twitter-bootstrap/css/bootstrap.css",
                      "~/Content/font-awesome/css/fontawesome.css",
                      "~/Content/bootstrap-select/css/bootstrap-select.css",
                      "~/Content/site.css"));
        }
    }
}
