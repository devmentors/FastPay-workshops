using System;
using FastPay.Domain.Exceptions;

namespace FastPay.Domain.ValueObjects
{
    public record Password
    {
        public string Value { get; init; }

        public Password(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidPasswordException();
            }
            if (value.Length is < 10 or > 50)
            {
                throw new InvalidPasswordException();
            }
            
            Value = value;
        }

        public static implicit operator Password(string value)
            => new(value);
        
        public static implicit operator string(Password password)
            => password.Value;
    }
}