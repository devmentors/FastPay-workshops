using System.Threading.Tasks;
using FastPay.Application.Abstractions;
using FastPay.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace FastPay.Api.Controllers
{
    public class FundsController : BaseController
    {
        private readonly IDispatcher _dispatcher;

        public FundsController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(AddFunds command)
        {
            await _dispatcher.SendAsync(command);
            return NoContent();
        }
        
        [HttpPost("transfer")]
        public async Task<IActionResult> Post(TransferFunds command)
        {
            await _dispatcher.SendAsync(command);
            return NoContent();
        }
    }
}