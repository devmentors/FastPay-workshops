using Microsoft.AspNetCore.Mvc;

namespace FastPay.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
    }
}