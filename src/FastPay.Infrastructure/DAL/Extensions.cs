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

            return services;
        }
    }
}