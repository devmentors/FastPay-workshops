using System.Linq;
using FastPay.Domain.Entities;

namespace FastPay.Application.DTO
{
    public static class Extensions
    {
        public static UserDetailsDto AsDto(this User user)
            => new()
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Password = user.Password,
                Nationality = user.Nationality,
                CreatedAt = user.CreatedAt,
                VerifiedAt = user.VerifiedAt
            };
        
        public static TransferDto AsDto(this Transfer transfer)
            => new()
            {
                TransferId = transfer.Id,
                ReferenceId = transfer.ReferenceId,
                WalletId = transfer.WalletId,
                Amount = transfer.Amount,
                Currency = transfer.Currency,
                Direction = transfer.Direction.ToString().ToLowerInvariant(),
                CreatedAt = transfer.CreatedAt
            };

        public static WalletDto AsDto(this Wallet wallet)
            => wallet.Map<WalletDto>();

        public static WalletDetailsDto AsDetailsDto(this Wallet wallet)
        {
            var dto = wallet.Map<WalletDetailsDto>();
            dto.Amount = wallet.CurrentAmount();
            dto.Transfers = wallet.Transfers.Select(x => x.AsDto())
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            return dto;
        }
        
        private static T Map<T>(this Wallet wallet) where T : WalletDto, new()
            => new()
            {
                WalletId = wallet.Id,
                UserId = wallet.UserId,
                Currency = wallet.Currency,
                CreatedAt = wallet.CreatedAt,
            };
    }
}