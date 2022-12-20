using KeyAuthenticationWithAttribute.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeyAuthenticationWithAttribute.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, KeyAuthorize]  
    public class SecretsController : ControllerBase
    {
        private readonly IConfiguration _config;

        public SecretsController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("secret")]
        public IActionResult GetSecret()
        {
            return Ok("This is my dirty secret...");
        }

        [HttpGet("known")]
        [AllowAnonymous]
        public IActionResult GetKnown()
        {
            return Ok("It is known");
        }
    }
}
