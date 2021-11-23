using System;
using System.Threading.Tasks;
using FastPay.Domain.Entities;

namespace FastPay.Domain.Repositories
{
    public interface IWalletRepository
    {
        Task<Wallet> GetAsync(Guid id);
        Task AddAsync(Wallet wallet);
        Task UpdateAsync(Wallet wallet);
        Task DeleteAsync(Wallet wallet);
    }
}