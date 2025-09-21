using System.Diagnostics;

namespace Task1.MiddleWares
{
    public class RequestLoggingMiddleware
    {
        
       
            private readonly RequestDelegate _next;
            private readonly ILogger<MiddleWares.RequestLoggingMiddleware> _logger;

            public RequestLoggingMiddleware(RequestDelegate next, ILogger<MiddleWares.RequestLoggingMiddleware> logger)
            {
                _next = next;
                _logger = logger;
            }

            public async Task InvokeAsync(HttpContext context)
            {
                // Start timing
                var TimeSpent = Stopwatch.StartNew();

                // Log request info
                _logger.LogInformation($"Incoming request: {context.Request.Method} {context.Request.Path}");

                // Pass request to next middleware
                await _next(context);

                // Stop timing
                TimeSpent.Stop();

                // Log response info
                _logger.LogInformation($"Response sent: {context.Response.StatusCode} - Time: {TimeSpent.ElapsedMilliseconds}ms");
            }
        }
    }


