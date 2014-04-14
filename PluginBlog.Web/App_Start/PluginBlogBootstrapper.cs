﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using PluginBlog.Core;
using PluginBlog.Core.Repositories;
using PluginBlog.Web.Helpers;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(PluginBlog.Web.PluginBlogBootstrapper), "Start")]
[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(PluginBlog.Web.PluginBlogBootstrapper), "PostStart")]
namespace PluginBlog.Web
{
    public class PluginBlogBootstrapper
    {
        public static void Start()
        {
            //Register a custom repository by implementing IEditableRepository 
            PluginBlogConfig.RegisterRepository = () => new BlogRepository(new BlogContext());

            //Register the authorization method used to enable/disable editing on the html element
            PluginBlogConfig.RegisterAuthorization = () =>
                                                     {
                                                         var editable =
                                                             !string.IsNullOrEmpty(
                                                                 HttpContext.Current.Request.QueryString["editablemvc"]);
                                                         if (editable)
                                                         {
                                                             return
                                                                 Convert.ToBoolean(
                                                                     HttpContext.Current.Request.QueryString[
                                                                         "editablemvc"]);
                                                         }
                                                         return true;
                                                     };

        }

        //After App_Start is executed register the bundles
        public static void PostStart()
        {
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));

            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/bootstrap", "http://netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.js").Include("~/Scripts/bootstrap.js", "~/Scripts/respond.js"));

            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/tinymce", "http://tinymce.cachefly.net/4.0/tinymce.min.js").Include("~/Scripts/tinymce/tinymce.js"));

            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/pluginblog").Include("~/Scripts/pluginblog.js"));

            BundleTable.Bundles.Add(new StyleBundle("~/Content/bootstrap", "http://netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.css").Include("~/Content/bootstrap.css"));

            BundleTable.Bundles.Add(new StyleBundle("~/Content/pluginblog").Include("~/Content/pluginblog.css"));
        }
    }
}

