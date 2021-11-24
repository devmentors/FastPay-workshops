using System;
using System.Collections.Generic;
using System.Linq;
using FastPay.Domain.Exceptions;
using FastPay.Domain.ValueObjects;

namespace FastPay.Domain.Entities
{
    public class Wallet
    {
        private bool _versionIncremented;
        private HashSet<Transfer> _transfers = new();

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Currency Currency { get; private set; }

        public IEnumerable<Transfer> Transfers
        {
            get => _transfers;
            set => _transfers = new HashSet<Transfer>(value);
        }

        public DateTime CreatedAt { get; private set; }
        public int Version { get; private set; }

        private Wallet()
        {
        }

        private Wallet(Guid id, Guid userId, Currency currency, DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            Currency = currency;
            CreatedAt = createdAt;
            Version = 1;
        }

        public static Wallet Create(Guid userId, Currency currency, DateTime createdAt)
            => Create(Guid.NewGuid(), userId, currency, createdAt);

        public static Wallet Create(Guid id, Guid userId, Currency currency, DateTime createdAt)
            => new(id, userId, currency, createdAt);

        public IReadOnlyCollection<Transfer> TransferFunds(Wallet receiver, Amount amount, DateTime createdAt)
        {
            if (Id == receiver.Id)
            {
                throw new CannotMakeTransferForSameWalletException();
            }

            var outTransferId = Guid.NewGuid();
            var inTransferId = Guid.NewGuid();

            var outTransfer = DeductFunds(outTransferId, amount, createdAt, inTransferId);
            var inTransfer = receiver.AddFunds(inTransferId, amount, createdAt, outTransferId);

            return new[] { outTransfer, inTransfer };
        }

        public Transfer AddFunds(Guid transferId, Amount amount, DateTime createdAt, Guid? referenceId = null)
        {
            var transfer = Transfer.Incoming(transferId, Id, Currency, amount, createdAt, referenceId);
            _transfers.Add(transfer);
            IncrementVersion();

            return transfer;
        }

        public Transfer DeductFunds(Guid transferId, Amount amount, DateTime createdAt, Guid? referenceId = null)
        {
            if (CurrentAmount() < amount)
            {
                throw new InsufficientWalletFundsException(Id);
            }

            var transfer = Transfer.Outgoing(transferId, Id, Currency, amount, createdAt, referenceId);
            _transfers.Add(transfer);
            IncrementVersion();

            return transfer;
        }

        public Amount CurrentAmount() => SumIncomingAmount() - SumOutgoingAmount();

        private Amount SumIncomingAmount()
            => _transfers.Where(x => x.Direction == Transfer.TransferDirection.In).Sum(x => x.Amount);

        private Amount SumOutgoingAmount()
            => _transfers.Where(x => x.Direction == Transfer.TransferDirection.Out).Sum(x => x.Amount);

        private void IncrementVersion()
        {
            if (_versionIncremented)
            {
                return;
            }

            Version++;
            _versionIncremented = true;
        }
    }
}