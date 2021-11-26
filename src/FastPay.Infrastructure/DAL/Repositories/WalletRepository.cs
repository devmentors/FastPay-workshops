using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastPay.Domain.Entities;
using FastPay.Domain.Repositories;
using FastPay.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FastPay.Infrastructure.DAL.Repositories
{
    internal sealed class WalletRepository : IWalletRepository
    {
        private readonly FastPayDbContext _context;
        private readonly DbSet<Wallet> _wallets;
    
        public WalletRepository(FastPayDbContext context)
        {
            _context = context;
            _wallets = _context.Wallets;
        }

        public Task<Wallet> GetAsync(Guid id)
            => _wallets
                .Include(x => x.Transfers)
                .SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IReadOnlyList<Wallet>> BrowseAsync(Currency currency = null)
        {
            var wallets = _wallets.AsQueryable();
            if (currency is not null)
            {
                wallets = wallets.Where(x => x.Currency.Equals(currency));
            }

            return await wallets.ToListAsync();
        }

        public async Task AddAsync(Wallet wallet)
        {
            await _wallets.AddAsync(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Wallet wallet)
        {
            foreach (var transfer in wallet.Transfers)
            {
                _context.Entry(transfer).State = EntityState.Detached;
            }
        
            _wallets.Update(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Wallet wallet)
        {
            _wallets.Remove(wallet);
            await _context.SaveChangesAsync();
        }
    }
}