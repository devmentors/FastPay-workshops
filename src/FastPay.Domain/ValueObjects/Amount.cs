using System;
using FastPay.Domain.Exceptions;

namespace FastPay.Domain.ValueObjects
{
    public sealed record Amount
    {
        public decimal Value { get; }

        public Amount(decimal value)
        {
            if (value is <= 0 or > 1_000_000)
            {
                throw new InvalidAmountException(value);
            }

            Value = value;
        }

        public static Amount Zero => new(0m);

        public static implicit operator Amount(decimal value) => new(value);

        public static implicit operator decimal(Amount value) => value.Value;

        public static bool operator >(Amount a, Amount b) => a.Value > b.Value;

        public static bool operator <(Amount a, Amount b) => a.Value < b.Value;

        public static bool operator >=(Amount a, Amount b) => a.Value >= b.Value;

        public static bool operator <=(Amount a, Amount b) => a.Value <= b.Value;

        public static Amount operator +(Amount a, Amount b) => a.Value + b.Value;

        public static Amount operator -(Amount a, Amount b) => a.Value - b.Value;
    }
}