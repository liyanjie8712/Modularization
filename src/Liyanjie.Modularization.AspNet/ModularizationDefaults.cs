﻿using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

using Liyanjie.TemplateMatching;

namespace Liyanjie.Modularization.AspNet
{
    /// <summary>
    /// 
    /// </summary>
    public class ModularizationDefaults
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestPath"></param>
        /// <param name="routeTemplate"></param>
        /// <returns></returns>
        public static bool TryMatchTemplate(string requestPath, string routeTemplate)
        {
            var routeValues = new RouteValueDictionary();
            var templateMatcher = new TemplateMatcher(TemplateParser.Parse(routeTemplate), routeValues);
            return templateMatcher.TryMatch(requestPath, routeValues);
        }

        /// <summary>
        /// (HttpContext,ModuleName)=>Task&lt;bool&gt;
        /// </summary>
        public static Func<HttpContext, string, Task<bool>> AuthorizeAsync { get; set; } = async (httpContext, moduleName) => await Task.FromResult(true);

        /// <summary>
        /// (HttpContext,ModuleName)=>Task
        /// </summary>
        public static Func<HttpContext, string, Task> HandleUnauthorizeAsync { get; set; } = async (httpContext, moduleName) =>
        {
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = 403;
            await Task.FromResult(0);
        };

        /// <summary>
        /// 
        /// </summary>
        public static Func<HttpResponse, object, Task> SerializeToResponseAsync { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static Func<HttpRequest, Type, Task<object>> DeserializeFromRequestAsync { get; set; }
    }
}
