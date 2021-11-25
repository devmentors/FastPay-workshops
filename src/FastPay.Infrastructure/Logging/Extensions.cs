using Microsoft.AspNetCore.Builder;

namespace FastPay.Infrastructure.Logging
{
    public static class Extensions
    {
        public static IApplicationBuilder UseLogging(this IApplicationBuilder app)
            => app.UseMiddleware<LoggingMiddleware>();
    }
}