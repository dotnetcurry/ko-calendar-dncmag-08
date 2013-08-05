using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;


public class BundleConfig
{
    public static void RegisterBundles(BundleCollection bundles)
    {
        bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-{version}.js"));

        bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/html5shiv.js"));

        bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                    "~/Scripts/jquery.unobtrusive*",
                    "~/Scripts/jquery.validate*"));

        bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                    "~/Scripts/knockout-{version}.js"));

        bundles.Add(new StyleBundle("~/Styles/bootstrap/css").Include(
            "~/Content/bootstrap-responsive.css",
            "~/Content/bootstrap.css"));

        bundles.Add(new ScriptBundle("~/bundles/jquerydate").Include(
                    "~/Scripts/datepicker/bootstrap-datepicker.js",
                    "~/Scripts/timepicker/bootstrap-timepicker.min.js",
                    "~/Scripts/moment.js"));
        
        bundles.Add(new ScriptBundle("~/bundles/calendar").Include(
                   "~/Scripts/calendar/day-calendar*"));
        
        bundles.Add(new StyleBundle("~/Styles/jquerydate").Include(
            
            "~/Content/datepicker/datepicker.css",
            "~/Content/timepicker/bootstrap-timepicker.css"));
    }

}
