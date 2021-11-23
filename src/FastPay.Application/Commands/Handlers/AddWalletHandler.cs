using System.Threading.Tasks;
using FastPay.Application.Abstractions;
using FastPay.Application.Exceptions;
using FastPay.Domain.Entities;
using FastPay.Domain.Exceptions;
using FastPay.Domain.Repositories;

namespace FastPay.Application.Commands.Handlers
{
    internal sealed class AddWalletHandler : ICommandHandler<AddWallet>
    {
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IClock _clock;

        public AddWalletHandler(IUserRepository userRepository, IWalletRepository walletRepository, 
            IClock clock)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
            _clock = clock;
        }

        public async Task HandleAsync(AddWallet command)
        {
            var (userId, currency) = command;

            var user = await _userRepository.GetAsync(userId);
            if (user is null)
            {
                throw new UserNotFoundException(userId);
            }
            if (!user.IsVerified)
            {
                throw new UserNotVerifiedException(userId);
            }

            var now = _clock.GetCurrentTime();
            var wallet = Wallet.Create(userId, currency, now);

            await _walletRepository.AddAsync(wallet);
        }
    }
}