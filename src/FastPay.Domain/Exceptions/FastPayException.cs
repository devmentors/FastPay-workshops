using System;

namespace FastPay.Domain.Exceptions
{
    public abstract class FastPayException : Exception
    {
        protected FastPayException(string message) : base(message)
        {
        }
    }
}