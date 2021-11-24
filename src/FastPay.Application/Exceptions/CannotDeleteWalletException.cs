using System;
using FastPay.Domain.Exceptions;

namespace FastPay.Application.Exceptions
{
    public class CannotDeleteWalletException : FastPayException
    {
        public Guid WalletId { get; }

        public CannotDeleteWalletException(Guid walletId) : base($"Wallet with ID: '{walletId}' cannot be deleted.")
        {
            WalletId = walletId;
        }
    }
}