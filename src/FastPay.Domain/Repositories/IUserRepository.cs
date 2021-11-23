using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FastPay.Domain.Entities;

namespace FastPay.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task<IReadOnlyList<User>> BrowseAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}