using System.Threading.Tasks;

namespace FastPay.Application.Abstractions
{
    public interface ICommandDispatcher
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}