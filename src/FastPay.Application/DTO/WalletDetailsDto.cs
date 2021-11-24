using System.Collections.Generic;

namespace FastPay.Application.DTO
{
    public class WalletDetailsDto : WalletDto
    {
        public decimal Amount { get; set; }
        public List<TransferDto> Transfers { get; set; } 
    }
}