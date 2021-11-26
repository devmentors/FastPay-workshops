using System;
using System.Threading.Tasks;
using FastPay.Application.Abstractions;
using FastPay.Application.Commands;
using FastPay.Application.Commands.Handlers;
using FastPay.Application.Exceptions;
using FastPay.Domain.Entities;
using FastPay.Domain.Repositories;
using NSubstitute;
using Shouldly;
using Xunit;

namespace FastPay.UnitTests.Application.Handlers
{
    public class AddFundsHandlerTests
    {
        [Fact]
        public async Task HandleAsync_Throws_WalletNotFoundException_When_wallet_With_Given_Id_IsNot_Found()
        {
            var command = new AddFunds(Guid.NewGuid(), 100);
            _walletRepository.GetAsync(command.WalletId).Returns(default(Wallet));

            var exception = await Record.ExceptionAsync(() => _handler.HandleAsync(command));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<WalletNotFoundException>();
        }
        
        #region ARRANGE

        private readonly ICommandHandler<AddFunds> _handler;
        private readonly IWalletRepository _walletRepository;
        private readonly ITransferRepository _transferRepository;
        
        public AddFundsHandlerTests()
        {
            _walletRepository = Substitute.For<IWalletRepository>();
            _transferRepository = Substitute.For<ITransferRepository>();
            var clock = Substitute.For<IClock>();
            _handler = new AddFundsHandler(_walletRepository, _transferRepository, null, clock);
        }

        #endregion
    }
}