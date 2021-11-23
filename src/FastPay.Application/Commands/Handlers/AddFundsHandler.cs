using System;
using System.Threading;
using System.Threading.Tasks;
using FastPay.Application.Abstractions;
using FastPay.Application.Exceptions;
using FastPay.Domain.Repositories;

namespace FastPay.Application.Commands.Handlers
{
    internal sealed class AddFundsHandler : ICommandHandler<AddFunds>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ITransferRepository _transferRepository;
        private readonly IClock _clock;

        public AddFundsHandler(IWalletRepository walletRepository, ITransferRepository transferRepository, IClock clock)
        {
            _walletRepository = walletRepository;
            _transferRepository = transferRepository;
            _clock = clock;
        }

        public async Task HandleAsync(AddFunds command)
        {
            var (walletId, amount) = command;
            var wallet = await _walletRepository.GetAsync(walletId);
            if (wallet is null)
            {
                throw new WalletNotFoundException(walletId);
            }

            var now = _clock.GetCurrentTime();
            var transferId = Guid.NewGuid();
            var transfer = wallet.AddFunds(transferId, amount, now);
            await _transferRepository.AddAsync(transfer);
            await _walletRepository.UpdateAsync(wallet);
        }
    }
}