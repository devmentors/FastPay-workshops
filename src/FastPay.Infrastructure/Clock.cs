using System;
using FastPay.Application.Abstractions;

namespace FastPay.Infrastructure
{
    internal sealed class Clock : IClock
    {
        public DateTime GetCurrentTime() => DateTime.UtcNow;
    }
}