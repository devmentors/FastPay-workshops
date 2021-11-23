using System;
using FastPay.Domain.Exceptions;

namespace FastPay.Application.Exceptions
{
    public sealed class UserNotVerifiedException : FastPayException
    {
        public Guid UserId { get; }

        public UserNotVerifiedException(Guid userId) 
            : base($"User with ID: {userId} has not been verified yet.")
        {
            UserId = userId;
        }
    }
}