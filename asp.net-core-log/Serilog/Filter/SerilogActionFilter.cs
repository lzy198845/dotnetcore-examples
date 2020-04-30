using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerilogDemo
{
    public class SerilogActionFilter : Attribute, IActionFilter
    {
        private readonly ILogger _logger;

        public SerilogActionFilter(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger("SerilogActionFilter");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var path = context.HttpContext.Request.Path;
            _logger.LogDebug($"{path} 开始运行了");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        { }
    }
}
