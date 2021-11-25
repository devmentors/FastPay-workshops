using System.Threading.Tasks;
using FastPay.Application.Abstractions;

namespace FastPay.Infrastructure
{
    internal sealed class InMemoryDispatcher : IDispatcher
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public InMemoryDispatcher(ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        public Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand
            => _commandDispatcher.SendAsync(command);

        public Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
            => _queryDispatcher.QueryAsync(query);
    }
}