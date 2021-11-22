using System;
using System.Reflection;
using FastPay.Domain.Entities;
using FastPay.Domain.Exceptions;
using Shouldly;
using Xunit;

namespace FastPay.UnitTests.Domain
{
    public class UserTests
    {
        [Fact]
        public void Verify_Throws_UserAlreadyVerifiedException_When_User_Is_Already_Verified()
        {
            // Arrange
            var user = GetValidUser();
            user.Verify(DateTime.Now);
            
            // Act
            var exception = Record.Exception(() => user.Verify(DateTime.Now));
            
            // Assert

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<UserAlreadyVerifiedException>();
        }

        [Fact]
        public void Verify_Assigns_VerifiedAt_Date_If_No_Verification_Has_Been_Performed()
        {
            var user = GetValidUser();
            var verifyDate = DateTime.Now;

            var exception = Record.Exception(() => user.Verify(verifyDate));
            
            exception.ShouldBeNull();
            user.IsVerified.ShouldBeTrue();
        }
        
        private static User GetValidUser()
            => new (Guid.NewGuid(), "test@mail.com", "Joe Doe",
        "test124567893", "polish", DateTime.Now); 
    }
}