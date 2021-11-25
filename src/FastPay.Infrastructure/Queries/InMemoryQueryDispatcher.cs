using System;
using System.Threading.Tasks;
using FastPay.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace FastPay.Infrastructure.Queries
{
    internal sealed class InMemoryQueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public InMemoryQueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            using var scope = _serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService(handlerType);
            var method = handlerType.GetMethod(nameof(IQueryHandler<IQuery<TResult>, TResult>.HandleAsync));
            if (method is null)
            {
                throw new InvalidOperationException($"Query handler for '{typeof(TResult).Name}' is invalid.");
            }

            // ReSharper disable once PossibleNullReferenceException
            return await (Task<TResult>)method.Invoke(handler, new object[] {query});
        }
    }
}