using System;

namespace FastPay.Domain.Exceptions
{
    public class UserNotFoundException : FastPayException
    {
        public Guid UserId { get; }

        public UserNotFoundException(Guid userId) : base($"User with ID: '{userId}' was not found.")
        {
            UserId = userId;
        }
    }
}