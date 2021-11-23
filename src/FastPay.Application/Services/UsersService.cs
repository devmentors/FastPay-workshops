using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastPay.Application.Abstractions;
using FastPay.Application.DTO;
using FastPay.Application.Exceptions;
using FastPay.Domain.Entities;
using FastPay.Domain.Exceptions;
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

        public async Task<UserDetailsDto> GetAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);
            return user?.AsDto();
        }

        public async Task<UserDetailsDto> GetAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);
            return user?.AsDto();
        }

        public async Task<IEnumerable<UserDetailsDto>> BrowseAsync()
        {
            var users = await _userRepository.BrowseAsync();
            return users?.Select(u => u.AsDto());
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

        public async Task VerifyAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user is null)
            {
                throw new UserNotFoundException(userId);
            }

            user.Verify(_clock.GetCurrentTime());
            await _userRepository.UpdateAsync(user);
        }
    }
}