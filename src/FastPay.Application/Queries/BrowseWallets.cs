using System.Collections.Generic;
using FastPay.Application.Abstractions;
using FastPay.Application.DTO;

namespace FastPay.Application.Queries
{
    public class BrowseWallets : IQuery<IReadOnlyList<WalletDto>>
    {
        public string Currency { get; set; }
    }
}