using System.Web;
using System.Web.Optimization;

namespace RALProject.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                            "~/Scripts/all.min.js",
                         "~/Scripts/jquery-{version}.js",
                         "~/Scripts/jquery.validate*",
                         "~/Scripts/bootstrap.js",
                         "~/Scripts/bootstrap.bundle.min.js",
                         "~/Scripts/jquery.easing.min..js",
                         "~/Scripts/sb-admin-2.min.js",
                         "~/Scripts/main.js",
                         "~/Scripts/wow.min.js",
                         "~/Scripts/brands.min.js",
                         "~/Scripts/fontawesome.min.js",
                         "~/Script/popper.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/all.min.css",
                      "~/Content/all.min.css",
                      "~/Content/sb-admin-2.css",
                      "~/Content/animate.min.css"));
        }
    }
}
