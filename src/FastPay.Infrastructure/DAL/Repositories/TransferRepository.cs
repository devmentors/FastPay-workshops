using System.Collections.Generic;
using System.Threading.Tasks;
using FastPay.Domain.Entities;
using FastPay.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FastPay.Infrastructure.DAL.Repositories
{
    internal sealed class TransferRepository : ITransferRepository
    {
        private readonly FastPayDbContext _context;
        private readonly DbSet<Transfer> _transfers;
    
        public TransferRepository(FastPayDbContext context)
        {
            _context = context;
            _transfers = context.Transfers;
        }

        public async Task<IReadOnlyList<Transfer>> BrowseAsync()
            => await _transfers.ToListAsync();

        public async Task AddAsync(params Transfer[] transfers)
        {
            await _transfers.AddRangeAsync(transfers);
            await _context.SaveChangesAsync();
        }
    }
}