using FastPay.Infrastructure;
using FastPay.Infrastructure.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FastPay.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly string _apiName;
        private readonly string _apiVersion;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiName = _configuration[$"api:{nameof(ApiOptions.Name)}"];
            _apiVersion = _configuration[$"api:{nameof(ApiOptions.Version)}"];
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApiOptions>(_configuration.GetSection("api"));
            services.Configure<DatabaseOptions>(_configuration.GetSection("database"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"{_apiName} {_apiVersion}");
                });
            });
        }
    }
}
