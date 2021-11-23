using System;
using System.Threading.Tasks;
using FastPay.Application.Abstractions;
using FastPay.Application.DTO;
using FastPay.Application.Exceptions;
using FastPay.Application.Services;
using FastPay.Domain.Entities;
using FastPay.Domain.Repositories;
using NSubstitute;
using Shouldly;
using Xunit;

namespace FastPay.UnitTests.Application
{
    public class UsersServiceTests
    {
        [Fact]
        public async Task AddAsync_Throws_UserAlreadyExistsException_When_User_With_Given_Email_Already_Exists()
        {
            // arrange
            var dto = GetUserDetailsDto();
            _usersRepository.ExistsAsync(dto.Email).Returns(true);
            
            // act

            var exception = await Record.ExceptionAsync(()=> _usersService.AddAsync(dto));

            //assert

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<UserAlreadyExistsException>();
        }

        [Fact]
        public async Task AddAsync_Calls_Repository_On_Success()
        {
            var dto = GetUserDetailsDto();
            _usersRepository.ExistsAsync(dto.Email).Returns(false);

            await _usersService.AddAsync(dto);
            
            await _usersRepository.Received(1).AddAsync(Arg.Is<User>(u =>
                u.Id == dto.Id && u.FullName == dto.FullName));
        }
        
        #region ARRANGE

        private readonly IUsersService _usersService;
        private readonly IUserRepository _usersRepository;
        private readonly IClock _clock;
        
        public UsersServiceTests()
        {
            _usersRepository = Substitute.For<IUserRepository>();
            _clock = Substitute.For<IClock>();
            _usersService = new UsersService(_usersRepository, _clock);
        }

        private static UserDetailsDto GetUserDetailsDto()
            => new()
            {
                Id = Guid.NewGuid(),
                Email = "user1@mail.com",
                Password = "AlaMaKota1234",
                FullName = "Joe Doe",
                Nationality = "PL"
            };

        #endregion

    }
}