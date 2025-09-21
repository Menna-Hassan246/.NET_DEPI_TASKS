using Microsoft.AspNetCore.Builder;
using Task1.MiddleWares;
using static Task1.MiddleWares.RequestLoggingMiddleware;

namespace Task1.ExtensionMethods
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UserequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MiddleWares.RequestLoggingMiddleware>();
        }
    }
}
