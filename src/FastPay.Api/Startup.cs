using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FastPay.Domain.Entities;
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
        private static readonly List<User> Users = new()
        {
            new User(Guid.NewGuid(), "user1@fastpay.io", "John Doe", "secret1234", "PL", DateTime.UtcNow),
            new User(Guid.NewGuid(), "user2@fastpay.io", "John Doe", "secret1234", "PL", DateTime.UtcNow),
            new User(Guid.NewGuid(), "user3@fastpay.io", "John Doe", "secret1234", "PL", DateTime.UtcNow)
        };
        
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
                
                endpoints.MapGet("api/users", async ctx =>
                {
                    await ctx.Response.WriteAsJsonAsync(Users);
                });
                
                endpoints.MapGet("api/users/{userId:guid}", async ctx =>
                {
                    var userId = Guid.Parse(ctx.Request.RouteValues["userId"].ToString());
                    var user = Users.SingleOrDefault(x => x.Id == userId);
                    if (user is null)
                    {
                        ctx.Response.StatusCode = StatusCodes.Status404NotFound;
                        return;
                    }

                    await ctx.Response.WriteAsJsonAsync(user);
                });
            });
        }
    }
}
