using System.Web.Optimization;

namespace ConsumerMvc.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Bundles/Scripts").Include(
                        "~/ClientApp/node_modules/jquery/dist/jquery.min.js",
                        "~/ClientApp/node_modules/bootstrap/dist/js/bootstrap.min.js",
                        "~/ClientApp/node_modules/@aspnet/signalr/dist/browser/signalr.min.js",
                        "~/ClientApp/node_modules/angular/angular.min.js",
                        "~/ClientApp/node_modules/angular-cookies/angular-cookies.js"));

            bundles.Add(new ScriptBundle("~/Bundles/AngularJs")
                .IncludeDirectory("~/ClientApp/AngularJS", "*.js", true)
                );

            bundles.Add(new StyleBundle("~/Content/Css").Include(
                      "~/ClientApp/node_modules/bootstrap/dist/css/bootstrap.min.css",
                      "~/Content/Site.min.css"));
        }
    }
}