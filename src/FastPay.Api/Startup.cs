using System;
using System.Collections.Generic;
using System.Linq;
using FastPay.Application;
using FastPay.Application.DTO;
using FastPay.Application.Services;
using FastPay.Domain.Entities;
using FastPay.Domain.ValueObjects;
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
            services.AddApplication();
            services.AddInfrastructure(_configuration);
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
                    var usersService = ctx.RequestServices.GetRequiredService<IUsersService>();
                    var users = await usersService.BrowseAsync();
                    await ctx.Response.WriteAsJsonAsync(users);
                });
                
                endpoints.MapGet("api/users/{userId:guid}", async ctx =>
                {
                    var userId = Guid.Parse(ctx.Request.RouteValues["userId"].ToString());
                    var usersService = ctx.RequestServices.GetRequiredService<IUsersService>();
                    var user = await usersService.GetAsync(userId);
                    if (user is null)
                    {
                        ctx.Response.StatusCode = StatusCodes.Status404NotFound;
                        return;
                    }

                    await ctx.Response.WriteAsJsonAsync(user);
                });

                endpoints.MapPost("api/users", async ctx =>
                {
                    var dto = await ctx.Request.ReadFromJsonAsync<UserDetailsDto>();
                    dto.Id = Guid.NewGuid();
                    var usersService = ctx.RequestServices.GetRequiredService<IUsersService>();
                    await usersService.AddAsync(dto);
                    ctx.Response.StatusCode = StatusCodes.Status201Created;
                    ctx.Response.Headers.Add("Location", $"api/users/{dto.Id}");
                });

                endpoints.MapPut("api/users/{userId:guid}", async ctx =>
                {
                    var userId = Guid.Parse(ctx.Request.RouteValues["userId"].ToString());
                    var usersService = ctx.RequestServices.GetRequiredService<IUsersService>();
                    await usersService.VerifyAsync(userId);
                });
            });
        }
    }

    internal class Wallet
    {
        public long Id { get; set; }
        public string Currency { get; set; }
        public Email Email { get; set; }
    }
}
