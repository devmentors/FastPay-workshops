using System.Collections.Generic;
using System.Threading.Tasks;
using FastPay.Domain.Entities;
using FastPay.Domain.Repositories;

namespace FastPay.Infrastructure.DAL.InMemory
{
    internal sealed class InMemoryTransferRepository : ITransferRepository
    {
        private readonly List<Transfer> _transfers = new();

        public async Task AddAsync(params Transfer[] transfers)
        {
            await Task.CompletedTask;
            _transfers.AddRange(transfers);
        }

        public async Task<IReadOnlyList<Transfer>> BrowseAsync()
        {
            await Task.CompletedTask;
            return _transfers;
        }
    }
}