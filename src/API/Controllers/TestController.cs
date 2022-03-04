using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult TextAdmin()
        {
            return Ok("only for Admin role");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Users()
        {
            return Ok("for everybody but must be registered");
        }
    }
}
