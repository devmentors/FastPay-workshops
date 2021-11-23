using System;
using System.Threading.Tasks;
using FastPay.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;


namespace FastPay.Infrastructure.Commands
{
    internal sealed class InMemoryCommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemoryCommandDispatcher(IServiceProvider serviceProvider)
             => _serviceProvider = serviceProvider;

        public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            var handler = _serviceProvider.GetService<ICommandHandler<TCommand>>();
            await handler.HandleAsync(command);
        }
    }
}