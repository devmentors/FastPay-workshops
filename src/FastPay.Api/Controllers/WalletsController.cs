using System;
using System.Threading.Tasks;
using FastPay.Application.Abstractions;
using FastPay.Application.Commands;
using FastPay.Application.DTO;
using FastPay.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FastPay.Api.Controllers
{
    public class WalletsController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public WalletsController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        public async Task<ActionResult<WalletDetailsDto>> Get([FromQuery] BrowseWallets query)
            => Ok(await _dispatcher.QueryAsync(query));

        [HttpGet("{walletId:guid}")]
        public async Task<ActionResult<WalletDto>> Get(Guid walletId)
        {
            var wallet = await _dispatcher.QueryAsync(new GetWallet { WalletId = walletId });
            if (wallet is not null)
            {
                return Ok(wallet);
            }

            return NotFound();
        }

        [HttpPost]
        [SwaggerOperation("Add wallet to the database")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post(AddWallet command)
        {
            await _dispatcher.SendAsync(command);
            return CreatedAtAction(nameof(Get), new { walletId = command.WalletId }, null);
        }
    
        [HttpDelete("{walletId:guid}")]
        public async Task<ActionResult> Delete(Guid walletId)
        {
            await _dispatcher.SendAsync(new DeleteWallet(walletId));
            return NoContent();
        }
    }
}