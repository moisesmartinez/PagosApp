using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace PagosApp
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            bundles.Add(new ScriptBundle("~/Content/corejs").Include(
                        "~/Content/assets/js/core/app.js"));

            bundles.Add(new ScriptBundle("~/Content/otherjs").Include(
                        "~/Content/assets/js/core/libraries/jquery.min.js",
                        "~/Content/assets/js/core/libraries/bootstrap.min.js",
                        "~/Content/assets/js/plugins/loaders/pace.min.js",
                        "~/Content/assets/js/plugins/loaders/blockui.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/assets/css/icons/icomoon/styles.css",
                        "~/Content/assets/core.css",
                        "~/Content/assets/css/bootstrap.css",
                        "~/Content/assets/css/components.css",
                        "~/Content/assets/css/colors.css"));
        }
    }
}