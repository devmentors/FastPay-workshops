using System;
using FastPay.Application.Abstractions;

namespace FastPay.Application.Commands
{
    public record DeleteWallet(Guid WalletId) : ICommand;
}