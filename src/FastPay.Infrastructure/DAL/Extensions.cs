using FastPay.Domain.Repositories;
using FastPay.Infrastructure.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastPay.Infrastructure.DAL
{
    internal static class Extensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration[$"database:{nameof(DatabaseOptions.ConnectionString)}"];
            services.AddDbContext<FastPayDbContext>(x => x.UseNpgsql(connectionString));
            services.AddScoped<ITransferRepository, TransferRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddHostedService<AppInitializer>();
            services.AddHostedService<TransfersCalculatorBackgroundService>();

            return services;
        }
    }
}