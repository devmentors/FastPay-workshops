using System;

namespace FastPay.Application.Abstractions
{
    public interface IClock
    {
        DateTime GetCurrentTime();
    }
}