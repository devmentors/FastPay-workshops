using System;
using FastPay.Application.Abstractions;

namespace FastPay.Application.Commands
{
    public record AddFunds(Guid WalletId, decimal Amount) : ICommand;
}