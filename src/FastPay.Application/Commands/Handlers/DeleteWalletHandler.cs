using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FastPay.Application.Abstractions;
using FastPay.Application.Exceptions;
using FastPay.Domain.Repositories;

namespace FastPay.Application.Commands.Handlers
{
    internal sealed class DeleteWalletHandler : ICommandHandler<DeleteWallet>
    {
        private readonly IWalletRepository _walletRepository;

        public DeleteWalletHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task HandleAsync(DeleteWallet command)
        {
            var wallet = await _walletRepository.GetAsync(command.WalletId);
            if (wallet is null)
            {
                throw new WalletNotFoundException(command.WalletId);
            }

            if (wallet.Transfers.Any())
            {
                throw new CannotDeleteWalletException(command.WalletId);
            }
        
            await _walletRepository.DeleteAsync(wallet);
        }
    }
}