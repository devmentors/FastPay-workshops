using System.ComponentModel.DataAnnotations;
using FastPay.Domain.Exceptions;

namespace FastPay.Domain.ValueObjects
{
    public record Email
    {
        public string Value { get; init; }

        public Email(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidEmailException(value);
            }
            if (!new EmailAddressAttribute().IsValid(value))
            {
                throw new InvalidEmailException(value);
            }
            
            Value = value;
        }

        public static implicit operator Email(string value)
            => new(value);
        
        public static implicit operator string(Email email)
            => email.Value;
    }
}