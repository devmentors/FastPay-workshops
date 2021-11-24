﻿using FastPay.Application.Abstractions;
using FastPay.Domain.Repositories;
using FastPay.Infrastructure.DAL;
using FastPay.Infrastructure.DAL.InMemory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastPay.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IUserRepository, InMemoryUserRepository>();
            services.AddSingleton<IClock, Clock>();
            
            services.Configure<ApiOptions>(configuration.GetSection("api"));
            services.Configure<DatabaseOptions>(configuration.GetSection("database"));

            return services;
        }
    }
}