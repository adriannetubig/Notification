using System.Web.Optimization;

namespace ConsumerMvc.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/ClientApp/node_modules/jquery/dist/jquery.min.js",
                        "~/ClientApp/node_modules/bootstrap/dist/js/bootstrap.min.js",
                        "~/ClientApp/node_modules/@aspnet/signalr/dist/browser/signalr.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/ClientApp/node_modules/bootstrap/dist/css/bootstrap.min.css",
                      "~/Content/Site.min.css"));
        }
    }
}