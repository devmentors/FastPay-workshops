using System;
using FastPay.Application.Abstractions;

namespace FastPay.Application.Commands
{
    public record TransferFunds(Guid FromWalletId, Guid ToWalletId, decimal Amount) : ICommand;
}