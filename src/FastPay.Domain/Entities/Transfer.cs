using System;
using FastPay.Domain.ValueObjects;

namespace FastPay.Domain.Entities
{
    public class Transfer
    {
        public Guid Id { get; private set; }
        public Guid WalletId { get; private set; }
        public Currency Currency { get; private set; }
        public Amount Amount { get; private set; }
        public TransferDirection Direction { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid? ReferenceId { get; private set; }

        private Transfer()
        {
        }

        private Transfer(Guid id, Guid walletId, Currency currency, Amount amount,
            TransferDirection direction, DateTime createdAt, Guid? referenceId = null)
        {
            Id = id;
            WalletId = walletId;
            Currency = currency;
            Amount = amount;
            Direction = direction;
            CreatedAt = createdAt;
            ReferenceId = referenceId;
        }

        public static Transfer Incoming(Guid id, Guid walletId, Currency currency, Amount amount, DateTime createdAt,
            Guid? referenceId = null)
            => new(id, walletId, currency, amount, TransferDirection.In, createdAt, referenceId);

        public static Transfer Outgoing(Guid id, Guid walletId, Currency currency, Amount amount, DateTime createdAt,
            Guid? referenceId = null)
            => new(id, walletId, currency, amount, TransferDirection.Out, createdAt, referenceId);

        public enum TransferDirection
        {
            In,
            Out
        }
    }
}