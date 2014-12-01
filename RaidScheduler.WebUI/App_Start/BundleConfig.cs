using System.Web;
using System.Web.Optimization;

namespace RaidScheduler
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/ThirdPartyLibraries/jquery-{version}.js",
                        "~/Scripts/ThirdPartyLibraries/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/ThirdPartyLibraries/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/ThirdPartyLibraries/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/ThirdPartyLibraries/bootstrap.js",
                      "~/Scripts/ThirdPartyLibraries/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/autocomplete").Include(
                "~/Scripts/ThirdPartyLibraries/typeahead.bundle.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/ThirdPartyLibraries/knockout-3.0.0.js",
                "~/Scripts/ThirdPartyLibraries/knockout.mapping-latest.js",
                "~/Scripts/ThirdPartyLibraries/knockout.validation.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/knockout-kendo").Include(
                "~/Scripts/ThirdPartyLibraries/kendo/2013.3.1119/kendo.web.min.js",
                "~/Scripts/ThirdPartyLibraries/knockout-kendo.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-theme.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/css/kendo").Include(               
                //"~/css/kendo.common.css",
                 "~/css/kendo.bootstrap.css"
                ));
        }
    }
}
