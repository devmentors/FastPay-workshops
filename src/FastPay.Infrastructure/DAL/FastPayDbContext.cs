using FastPay.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastPay.Infrastructure.DAL
{
    internal sealed class FastPayDbContext : DbContext
    {
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        public FastPayDbContext(DbContextOptions<FastPayDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}