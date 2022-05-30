using KeyAuthenticationWithAttribute.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KeyAuthenticationWithAttribute.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SecretsController : ControllerBase
    {
        [HttpGet]
        [ApiKeyAuthenticate]
        public IActionResult GetSecret()
        {
            return Ok("This is my dirty secret...");
        }
    }
}
