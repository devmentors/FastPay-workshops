using System;
using System.Linq;
using FastPay.Domain.Entities;
using FastPay.Domain.Exceptions;
using FastPay.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace FastPay.UnitTests.Domain
{
    public class WalletTests
    {
        [Fact]
        public void DeductFunds_Throws_InsufficientWalletFundsException_When_Transfer_Amount_Is_Higher_Than_Wallet_Amount()
        {
            var wallet = GetValidWallet();

            var exception = Record.Exception(() =>
                wallet.DeductFunds(Guid.NewGuid(), 100, DateTime.Now));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InsufficientWalletFundsException>();
        }
        
        [Fact]
        public void DeductFunds_Adds_Transfer_When_Amount_Is_Less_Than_Wallet_Amount()
        {
            var transferId = Guid.NewGuid();
            var amount = 100;
            
            var wallet = GetValidWallet();
            wallet.AddFunds(Guid.NewGuid(), 200, DateTime.Now);

            var exception = Record.Exception(() =>
                wallet.DeductFunds(transferId, amount, DateTime.Now));

            exception.ShouldBeNull();
            wallet.Transfers.Count().ShouldBe(2);

            var transfer = wallet.Transfers.SingleOrDefault(t => t.Direction == Transfer.TransferDirection.Out);
            
            transfer.Id.ShouldBe(transferId);
            transfer.Amount.ShouldBe(amount);
        }
        
        private static Wallet GetValidWallet()
            => Wallet.Create(Guid.NewGuid(), new Currency("PLN"), DateTime.Now);
    }
}