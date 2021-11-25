using System;
using System.ComponentModel.DataAnnotations;

namespace FastPay.Application.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? VerifiedAt { get; set; }
    }
}