using System.Linq;
using System.Threading.Tasks;
using FastPay.Application.Abstractions;
using FastPay.Application.Exceptions;
using FastPay.Domain.Repositories;

namespace FastPay.Application.Commands.Handlers
{
    internal sealed class TransferFundsHandler : ICommandHandler<TransferFunds>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ITransferRepository _transferRepository;
        private readonly IClock _clock;

        public TransferFundsHandler(IWalletRepository walletRepository, ITransferRepository transferRepository,
            IClock clock)
        {
            _walletRepository = walletRepository;
            _transferRepository = transferRepository;
            _clock = clock;
        }

        public async Task HandleAsync(TransferFunds command)
        {
            var (fromWalletId, toWalletId, amount) = command;
            var fromWallet = await _walletRepository.GetAsync(fromWalletId);
            if (fromWallet is null)
            {
                throw new WalletNotFoundException(fromWalletId);
            }

            var toWallet = await _walletRepository.GetAsync(toWalletId);
            if (toWallet is null)
            {
                throw new WalletNotFoundException(toWalletId);
            }

            var now = _clock.GetCurrentTime();
            var transfers = fromWallet.TransferFunds(toWallet, amount, now);
            await _transferRepository.AddAsync(transfers.ToArray());
            await _walletRepository.UpdateAsync(fromWallet);
            await _walletRepository.UpdateAsync(toWallet);
        }
    }
}