using System.Collections.Generic;
using FastPay.Application.Abstractions;
using FastPay.Application.Commands;
using FastPay.Application.Commands.Handlers;
using FastPay.Application.DTO;
using FastPay.Application.Queries;
using FastPay.Application.Queries.Handlers;
using FastPay.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FastPay.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
            
            services.AddScoped<ICommandHandler<AddFunds>, AddFundsHandler>();
            services.AddScoped<ICommandHandler<AddWallet>, AddWalletHandler>();
            services.AddScoped<ICommandHandler<DeleteWallet>, DeleteWalletHandler>();
            services.AddScoped<ICommandHandler<TransferFunds>, TransferFundsHandler>();

            services.AddScoped<IQueryHandler<BrowseWallets, IReadOnlyList<WalletDto>>, BrowseWalletsHandler>();
            services.AddScoped<IQueryHandler<GetWallet, WalletDetailsDto>, GetWalletHandler>();

            return services;
        }
    }
}