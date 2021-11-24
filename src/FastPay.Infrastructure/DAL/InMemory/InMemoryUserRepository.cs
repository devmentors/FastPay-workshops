using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastPay.Domain.Entities;
using FastPay.Domain.Repositories;

namespace FastPay.Infrastructure.DAL.InMemory
{
    internal sealed class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new();

        public async Task<User> GetAsync(Guid id)
        {
            await Task.CompletedTask;
            return _users.SingleOrDefault(x => x.Id == id);
        }

        public async Task<User> GetAsync(string email)
        {
            await Task.CompletedTask;
            return _users.SingleOrDefault(x => x.Email == email);
        }

        public async Task<bool> ExistsAsync(string email)
        {
            await Task.CompletedTask;
            return _users.Any(x => x.Email == email);
        }

        public async Task<IReadOnlyList<User>> BrowseAsync()
        {
            await Task.CompletedTask;
            return _users;
        }

        public async Task AddAsync(User user)
        {
            await Task.CompletedTask;
            _users.Add(user);
        }

        public async Task UpdateAsync(User user)
        {
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(User user)
        {
            await Task.CompletedTask;
            _users.Remove(user);
        }
    }
}