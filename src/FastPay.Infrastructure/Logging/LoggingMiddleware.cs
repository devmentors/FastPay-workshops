using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FastPay.Infrastructure.Logging
{
    internal sealed class LoggingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("middleware start");
            await next(context);
            Console.WriteLine("middleware finish");
        }
    }
}