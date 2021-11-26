using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FastPay.Payments.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        services.AddSingleton<PaymentsService>();
                    });
                    webBuilder.Configure(app =>
                    {
                        app.UseRouting();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapPost("payments", async ctx =>
                            {
                                var request = await ctx.Request.ReadFromJsonAsync<PaymentRequest>();
                                var paymentsService = ctx.RequestServices.GetRequiredService<PaymentsService>();
                                var response = await paymentsService.ProcessPaymentAsync(request);
                                if (response is not null)
                                {
                                    await ctx.Response.WriteAsJsonAsync(response);
                                    return;
                                }

                                ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
                            });
                        });
                    });
                });
    }
}