using FastPay.Domain.Exceptions;
using FastPay.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace FastPay.UnitTests.Domain
{
    public class EmailTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Email_Should_Throw_InvalidEmailException_When_Value_Is_Null_Or_Empty(string value)
        {
            var exception = Record.Exception(() => new Email(value));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidEmailException>();
        }
        
        [Theory]
        [InlineData("mail@@.test.pl")]
        [InlineData("mailtest.pl@")]
        public void Email_Should_Throw_InvalidEmailException_When_Value_Is_Invalid_Email(string value)
        {
            var exception = Record.Exception(() => new Email(value));
            
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidEmailException>();
        }

        [Fact]
        public void Email_Should_Assign_Value_When_Is_Valid()
        {
            var validEmail = "valid@email.com";
            var email = new Email(validEmail);
            
            email.Value.ShouldBe(validEmail);
        }
    }
}