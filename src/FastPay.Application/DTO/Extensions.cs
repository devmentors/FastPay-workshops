using System.Collections.Generic;
using FastPay.Domain.Entities;

namespace FastPay.Application.DTO
{
    public static class Extensions
    {
        public static UserDetailsDto AsDto(this User user)
            => new()
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Password = user.Password,
                Nationality = user.Nationality,
                CreatedAt = user.CreatedAt,
                VerifiedAt = user.VerifiedAt
            };

    }
}