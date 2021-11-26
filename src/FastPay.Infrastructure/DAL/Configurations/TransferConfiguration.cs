using FastPay.Domain.Entities;
using FastPay.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastPay.Infrastructure.DAL.Configurations
{
    internal sealed class TransferConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.Property(x => x.WalletId)
                .IsRequired();
            
            builder.Property(x => x.Currency)
                .IsRequired()
                .HasConversion(x => x.Value, x => new Currency(x));
            
            builder.Property(x => x.Amount)
                .IsRequired()
                .HasConversion(x => x.Value, x => new Amount(x));
        }
    }
}