using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MiddlewareFlowDemo.MyMiddlewares
{
    public class MyMiddleware
    {
        RequestDelegate _next;
        ILogger _logger;
        public MyMiddleware(RequestDelegate next, ILogger<MyMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (_logger.BeginScope("TraceIdentifier:{TraceIdentifier}", context.TraceIdentifier))
            {
                _logger.LogDebug("开始执行");
                _logger.LogInformation("---- 真的开始了");

                await _next(context);

                _logger.LogDebug("执行结束");
            }
        }
    }
}
