using System.Web;
using System.Web.Optimization;

namespace Chatin
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/chatin").Include(
                "~/Scripts/app/ChatinController.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/angular").Include(
                "~/Scripts/angular.min.js",
                "~/Scripts/angular-sanitize.min.js",
                "~/Scripts/angular-resource.min.js",
                "~/Scripts/ngOfficeUiFabric.min.js",
                "~/Scripts/moment.min.js",
                "~/Scripts/angular-moment.min.js",
                "~/Scripts/underscore.min.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/app/chatin.css"
            ));

            bundles.Add(new StyleBundle("~/Content/fabric").Include(
                "~/Content/fabric.min.css",
                "~/Content/fabric.components.min.css"
            ));
        }
    }
}
