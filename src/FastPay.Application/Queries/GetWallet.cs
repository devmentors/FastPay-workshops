using System;
using FastPay.Application.Abstractions;
using FastPay.Application.DTO;

namespace FastPay.Application.Queries
{
    public class GetWallet : IQuery<WalletDetailsDto>
    {
        public Guid WalletId { get; set; }
    }
}