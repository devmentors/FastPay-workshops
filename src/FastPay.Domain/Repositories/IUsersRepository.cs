using System;
using System.Threading.Tasks;
using FastPay.Domain.Entities;

namespace FastPay.Domain.Repositories
{
    public interface IUsersRepository
    {
        Task<User> GetAsync(Guid id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}