using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FastPay.Application.Abstractions;
using FastPay.Application.DTO;
using FastPay.Application.Exceptions;
using FastPay.Domain.Entities;
using FastPay.Domain.Repositories;


namespace FastPay.Application.Services
{
    internal sealed class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IClock _clock;

        public UsersService(IUserRepository userRepository, IClock clock)
        {
            _userRepository = userRepository;
            _clock = clock;
        }

        public Task<UserDetailsDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDetailsDto> GetAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDetailsDto>> BrowseAsync()
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(UserDetailsDto dto)
        {
            var alreadyExists = await _userRepository.ExistsAsync(dto.Email);
            if (alreadyExists)
            {
                throw new UserAlreadyExistsException(dto.Email);
            }

            var user = new User(dto.Id, dto.Email, dto.FullName, dto.Password,
                dto.Nationality, _clock.GetCurrentTime());

            await _userRepository.AddAsync(user);
        }

        public Task VerifyAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}