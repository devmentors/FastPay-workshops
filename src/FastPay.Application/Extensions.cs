using FastPay.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FastPay.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IUsersService, UsersService>();

            return services;
        }
    }
}