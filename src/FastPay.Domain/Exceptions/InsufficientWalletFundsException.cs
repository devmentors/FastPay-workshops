using System;

namespace FastPay.Domain.Exceptions
{
    public sealed class InsufficientWalletFundsException : FastPayException
    {
        public Guid WalletId { get; }

        public InsufficientWalletFundsException(Guid walletId)
            : base($"Insufficient funds for wallet with ID: '{walletId}'.")
        {
            WalletId = walletId;
        }
    }
}