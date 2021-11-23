using System;
using FastPay.Domain.Exceptions;

namespace FastPay.Application.Exceptions
{
    public sealed class WalletNotFoundException : FastPayException
    {
        public Guid WalletId { get; }

        public WalletNotFoundException(Guid walletId) : base($"Wallet with ID: '{walletId}' was not found.")
        {
            WalletId = walletId;
        }
    }
}