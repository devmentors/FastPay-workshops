using System;
using FastPay.Domain.Exceptions;

namespace FastPay.Application.Exceptions
{
    public sealed class UserAlreadyExistsException : FastPayException
    {
        public string Email { get; }

        public UserAlreadyExistsException(string email) 
            : base($"User with email: {email} already exists.")
        {
            Email = email;
        }
    }
}