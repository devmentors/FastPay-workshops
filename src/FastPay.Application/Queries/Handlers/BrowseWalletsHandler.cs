using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastPay.Application.Abstractions;
using FastPay.Application.DTO;
using FastPay.Domain.Repositories;

namespace FastPay.Application.Queries.Handlers
{
    internal sealed class BrowseWalletsHandler : IQueryHandler<BrowseWallets, IReadOnlyList<WalletDto>>
    {
        private readonly IWalletRepository _walletRepository;

        public BrowseWalletsHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }
        
        public async Task<IReadOnlyList<WalletDto>> HandleAsync(BrowseWallets query)
        {
            var wallets = await _walletRepository.BrowseAsync(query.Currency);

            return wallets.Select(x => x.AsDto()).ToList();
        }
    }
}