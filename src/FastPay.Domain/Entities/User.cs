using System;
using FastPay.Domain.Exceptions;
using FastPay.Domain.ValueObjects;

namespace FastPay.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; } 
        public string Email { get; private set; }
        public string FullName { get; private set; }
        public Password Password { get; private set; }
        public string Nationality { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? VerifiedAt { get; private set; }

        public bool IsVerified => VerifiedAt.HasValue;
        
        public User(Guid id, string email, string fullName, Password password,
            string nationality, DateTime createdAt = default)
        {
            Id = id;
            Email = email;
            FullName = fullName;
            Password = password;
            Nationality = nationality;
            CreatedAt = createdAt;
        }

        public void Verify(DateTime verifiedAt)
        {
            if (IsVerified)
            {
                throw new UserAlreadyVerifiedException(Id);
            }

            VerifiedAt = verifiedAt;
        }
    }
}