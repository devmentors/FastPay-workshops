using System.Collections.Generic;
using FastPay.Domain.Exceptions;

namespace FastPay.Domain.ValueObjects
{
    public record Currency
    {
        public static readonly HashSet<string> AllowedValues = new()
        {
            "PLN", "EUR", "GBP"
        };

        public string Value { get; }
        
        public Currency(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 3)
            {
                throw new InvalidCurrencyException(value);
            }

            value = value.ToUpperInvariant();
            if (!AllowedValues.Contains(value))
            {
                throw new UnsupportedCurrencyException(value);
            }
            
            Value = value;
        }
        
        public static implicit operator Currency(string value) => value is null ? null : new Currency(value);
        
        public static implicit operator string(Currency value) => value?.Value;
        
        public override string ToString() => Value;
    }
}