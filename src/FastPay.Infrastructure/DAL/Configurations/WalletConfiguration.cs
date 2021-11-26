using FastPay.Domain.Entities;
using FastPay.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastPay.Infrastructure.DAL.Configurations
{
    internal sealed class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);
            builder.Property(x => x.Version).IsConcurrencyToken();
            builder.Property(x => x.Currency)
                .IsRequired()
                .HasConversion(x => x.Value, x => new Currency(x));
        }
    }
}