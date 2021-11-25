using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FastPay.Application.DTO;
using FastPay.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FastPay.Api.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetailsDto>>> Get()
            => Ok(await _usersService.BrowseAsync());

        [HttpGet("{userId:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserDetailsDto>> Get(Guid userId)
        {
            var user = await _usersService.GetAsync(userId);
            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Post(UserDetailsDto dto)
        {
            dto.Id = Guid.NewGuid();
            await _usersService.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new { userId = dto.Id }, null);
        }
        
        [HttpPut("{userId:guid}/verify")]
        public async Task<ActionResult> Put(Guid userId)
        {
            await _usersService.VerifyAsync(userId);
            return NoContent();
        }
    }
}