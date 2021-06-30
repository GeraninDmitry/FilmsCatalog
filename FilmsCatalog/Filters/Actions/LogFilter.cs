using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace FilmsCatalog.Filters.Actions
{
    public class LogFilter : IActionFilter
    {
        readonly ILogger<LogFilter> Logger;

        public LogFilter(ILogger<LogFilter> logger)
        {
            Logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var list = new List<IDisposable>();

            foreach (var item in context.ActionArguments)
            {
                list.Add(Logger.BeginScope(new Dictionary<string, object> { { item.Key, item.Value } }));
            }

            Logger.LogTrace($"{request.RouteValues["controller"]}/{request.RouteValues["action"]}");

            foreach (var item in list)
            {
                item.Dispose();
            }
        }
    }

    public class LogAttribute : TypeFilterAttribute
    {
        public LogAttribute() : base(typeof(LogFilter)) { }
    }
}
