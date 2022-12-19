using KeyAuthenticationWithAttribute.Filters;
using Microsoft.AspNetCore.Mvc;

namespace KeyAuthenticationWithAttribute.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, ApiKeyAuthenticate]  
    public class SecretsController : ControllerBase
    {
        [HttpGet("secret")]
        public IActionResult GetSecret()
        {
            return Ok("This is my dirty secret...");
        }

        [HttpGet("known")]
        [NoApiKeyAuthenticate]
        public IActionResult GetKnown()
        {
            return Ok("It is known");
        }
    }
}
