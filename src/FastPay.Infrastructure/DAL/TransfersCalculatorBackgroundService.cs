using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FastPay.Domain.Entities;
using FastPay.Domain.Repositories;
using FastPay.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FastPay.Infrastructure.DAL
{
    internal sealed class TransfersCalculatorBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<TransfersCalculatorBackgroundService> _logger;

        public TransfersCalculatorBackgroundService(IServiceProvider serviceProvider,
            ILogger<TransfersCalculatorBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var transferRepository = scope.ServiceProvider.GetRequiredService<ITransferRepository>();
                    var transfers = await transferRepository.BrowseAsync();
                    var currencyAmounts = new Dictionary<string, Amounts>();
                    foreach (var currency in Currency.AllowedValues)
                    {
                        currencyAmounts[currency] = new Amounts();
                    }

                    foreach (var transfer in transfers)
                    {
                        switch (transfer.Direction)
                        {
                            case Transfer.TransferDirection.In:
                                currencyAmounts[transfer.Currency].In += transfer.Amount;
                                break;
                            case Transfer.TransferDirection.Out:
                                currencyAmounts[transfer.Currency].Out += transfer.Amount;
                                break;
                        }
                    }

                    foreach (var (key, value) in currencyAmounts)
                    {
                        _logger.LogInformation($"Total amount {key} -> " + $"IN: {value.In}, OUT: {value.Out}");
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }

        private class Amounts
        {
            public decimal In { get; set; }
            public decimal Out { get; set; }
        }
    }
}