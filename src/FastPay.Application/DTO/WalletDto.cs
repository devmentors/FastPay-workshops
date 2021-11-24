using System;

namespace FastPay.Application.DTO
{
    public class WalletDto
    {
        public Guid WalletId { get; set; }
        public Guid UserId { get; set; }
        public string Currency { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}