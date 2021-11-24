using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FastPay.Domain.Entities;
using FastPay.Domain.ValueObjects;

namespace FastPay.Domain.Repositories
{
    public interface IWalletRepository
    {
        Task<Wallet> GetAsync(Guid id);
        Task<IReadOnlyList<Wallet>> BrowseAsync(Currency currency = null);
        Task AddAsync(Wallet wallet);
        Task UpdateAsync(Wallet wallet);
        Task DeleteAsync(Wallet wallet);
    }
}