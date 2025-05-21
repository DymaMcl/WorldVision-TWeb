using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace WorldVision.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/font-awesome/css").Include(
                "~/Content/font-swesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/materialdesignicons/css").Include(
                "~/Vendors/materialdesignicons.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/aos/css").Include(
                "~/Vendors/aos.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/owl.carouse/css").Include(
             "~/Vendors/owl.carouse.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/owl.theme.default/css").Include(
             "~/Vendors/owl.theme.default.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/style/css").Include(
             "~/Vendors/style.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include(
                "~/Vendors/bootstrap.min.css", new CssRewriteUrlTransform()));


            bundles.Add(new ScriptBundle("~/bundles/vendor.bundle.base/js").Include(
                "~/Vendors/vendor.bundle.base.js"));

            bundles.Add(new ScriptBundle("~/bundles/owl.carousel/js").Include(
                "~/Vendors/owl.carousel.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/demo/js").Include(
                "~/Vendors/demo.js.js"));

            
        }
        
    }
}