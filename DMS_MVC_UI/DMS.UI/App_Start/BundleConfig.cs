using System.Web;
using System.Web.Optimization;

namespace DMS.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.IgnoreList.Clear();

            #region Common Library

            #region JS Script
            bundles.Add(new ScriptBundle("~/bundles/js/jquery").Include(
                         "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/modernizr").Include(
                        "~/JS_Script_Libraries/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/js/jqueryval").Include(
                        "~/JS_Script_Libraries/jquery.validate*",
                        "~/JS_Script_Libraries/jquery.validity.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/js/angularScripts").Include(
                            "~/JS_Script_Libraries/angular.js",
                            "~/JS_Script_Libraries/angular-animate.js",
                            "~/JS_Script_Libraries/angular-sanitize.js",
                            "~/JS_Script_Libraries/ui-bootstrap-tpls-2.5.0.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/js/angularPluginsScripts").Include(
                            "~/JS_Script_Libraries/angular-ui-router.js",
                            "~/JS_Script_Libraries/ngDialog.js",
                            "~/JS_Script_Libraries/DialogueService.js"
            ));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryBlocker").Include(
            //                "~/Scripts/jquery.blockUI.js"
            //));
            #endregion

            #region Style
            bundles.Add(new StyleBundle("~/bundles/style/bootstrap").Include(
                        "~/JS_Style_Libraries/bootstrap.css"));

            bundles.Add(new StyleBundle("~/bundles/fontawesome").Include(
                        "~/JS_Style_Libraries/font-awesome.css"));

            bundles.Add(new StyleBundle("~/bundles/style/ngDialog").Include(
                          "~/JS_Style_Libraries/ngDialog.css",
                          "~/JS_Style_Libraries/ngDialog-theme-plain.css"));
            #endregion

            #endregion





            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            //BundleTable.EnableOptimizations = true;
        }
    }
}
