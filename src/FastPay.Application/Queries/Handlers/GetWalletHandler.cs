using System.Threading.Tasks;
using FastPay.Application.Abstractions;
using FastPay.Application.DTO;
using FastPay.Domain.Repositories;

namespace FastPay.Application.Queries.Handlers
{
    internal sealed class GetWalletHandler : IQueryHandler<GetWallet, WalletDetailsDto>
    {
        private readonly IWalletRepository _walletRepository;

        public GetWalletHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }   
        
        public async Task<WalletDetailsDto> HandleAsync(GetWallet query)
        {
            var wallet = await _walletRepository.GetAsync(query.WalletId);
            
            return wallet?.AsDetailsDto();
        }
    }
}