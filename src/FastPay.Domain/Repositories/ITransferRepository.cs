using System.Collections.Generic;
using System.Threading.Tasks;
using FastPay.Domain.Entities;

namespace FastPay.Domain.Repositories
{
    public interface ITransferRepository
    {
        Task AddAsync(params Transfer[] transfers);
        Task<IReadOnlyList<Transfer>> BrowseAsync();
    }
}