using FastPay.Application.Abstractions;
using FastPay.Application.Clients;
using FastPay.Domain.Repositories;
using FastPay.Infrastructure.Clients;
using FastPay.Infrastructure.Commands;
using FastPay.Infrastructure.DAL;
using FastPay.Infrastructure.DAL.InMemory;
using FastPay.Infrastructure.Exceptions;
using FastPay.Infrastructure.Logging;
using FastPay.Infrastructure.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastPay.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddHttpClient<IPaymentsApiClient, PaymentsApiClient>();
            
            services.AddSingleton<IDispatcher, InMemoryDispatcher>();
            services.AddSingleton<ICommandDispatcher, InMemoryCommandDispatcher>();
            services.AddSingleton<IQueryDispatcher, InMemoryQueryDispatcher>();
            services.AddSingleton<IUserRepository, InMemoryUserRepository>();
            services.AddSingleton<IWalletRepository, InMemoryWalletRepository>();
            services.AddSingleton<ITransferRepository, InMemoryTransferRepository>();
            services.AddSingleton<IClock, Clock>();
            services.AddScoped<ErrorHandlerMiddleware>();
            services.AddScoped<LoggingMiddleware>();
            
            services.Configure<ApiOptions>(configuration.GetSection("api"));
            services.Configure<DatabaseOptions>(configuration.GetSection("database"));

            services.AddDatabase(configuration);

            return services;
        }
    }
}