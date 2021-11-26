using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FastPay.Domain.Entities;
using FastPay.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FastPay.Infrastructure.DAL.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly FastPayDbContext _dbContext;
        private readonly DbSet<User> _users;

        public UserRepository(FastPayDbContext dbContext)
        {
            _dbContext = dbContext;
            _users = dbContext.Users;
        }

        public Task<User> GetAsync(Guid id)
            => _users.SingleOrDefaultAsync(x => x.Id == id);

        public Task<User> GetAsync(string email)
            => _users.SingleOrDefaultAsync(x => x.Email.Equals(email));

        public Task<bool> ExistsAsync(string email)
            => _users.AnyAsync(x => x.Email.Equals(email));

        public async Task<IReadOnlyList<User>> BrowseAsync()
        {
            return await _users.ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}