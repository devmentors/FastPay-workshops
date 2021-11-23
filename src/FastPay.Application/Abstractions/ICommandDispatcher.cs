using System.Threading.Tasks;

namespace FastPay.Application.Abstractions
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}