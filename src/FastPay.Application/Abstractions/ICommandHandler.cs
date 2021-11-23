using System.Threading.Tasks;

namespace FastPay.Application.Abstractions
{
    public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
    {
        Task HandleAsync(TCommand command);
    }
}