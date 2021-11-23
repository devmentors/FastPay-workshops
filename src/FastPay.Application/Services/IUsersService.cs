using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FastPay.Application.DTO;

namespace FastPay.Application.Services
{
    public interface IUsersService
    {
        Task<UserDetailsDto> GetAsync(Guid id);
        Task<UserDetailsDto> GetAsync(string email);
        Task<IEnumerable<UserDetailsDto>> BrowseAsync();
        Task AddAsync(UserDetailsDto dto);
        Task VerifyAsync(Guid userId);
    }
}