using System;

namespace FastPay.Domain.Exceptions
{
    public class UserAlreadyVerifiedException : FastPayException
    {
        public UserAlreadyVerifiedException(Guid id) 
            : base($"User with ID: {id} was already verified.")
        {
        }
    }
}