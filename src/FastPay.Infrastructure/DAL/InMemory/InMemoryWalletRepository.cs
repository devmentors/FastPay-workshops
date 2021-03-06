using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastPay.Domain.Entities;
using FastPay.Domain.Repositories;
using FastPay.Domain.ValueObjects;

namespace FastPay.Infrastructure.DAL.InMemory
{
    internal sealed class InMemoryWalletRepository : IWalletRepository
    {
        // Not thread safe
        private readonly List<Wallet> _wallets = new();
        
        public async Task<Wallet> GetAsync(Guid id)
        {
            await Task.CompletedTask;
            return _wallets.SingleOrDefault(x => x.Id == id);
        }
        
        public async Task<IReadOnlyList<Wallet>> BrowseAsync(Currency currency = null)
        {
            await Task.CompletedTask;
            var wallets = _wallets.AsEnumerable();
            if (currency is not null)
            {
                wallets = wallets.Where(x => x.Currency == currency);
            }

            return wallets.ToList();
        }

        public async Task AddAsync(Wallet wallet)
        {
            await Task.CompletedTask;
            _wallets.Add(wallet);
        }

        public async Task UpdateAsync(Wallet wallet)
        {
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Wallet wallet)
        {
            await Task.CompletedTask;
            _wallets.Remove(wallet);
        }
    }
}