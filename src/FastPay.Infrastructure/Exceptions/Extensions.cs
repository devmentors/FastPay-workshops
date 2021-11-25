using FastPay.Infrastructure.Logging;
using Microsoft.AspNetCore.Builder;

namespace FastPay.Infrastructure.Exceptions
{
    public static class Extensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
            => app.UseMiddleware<ErrorHandlerMiddleware>();
    }
}