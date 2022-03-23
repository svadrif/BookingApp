using Application.DTOs.AppUserDTO;
using Application.Interfaces.IServices;
using Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<UserController> _logger;

        public UserController(IAppUserService appUserService, IAuthenticationService authenticationService, ILogger<UserController> logger)
        {
            _appUserService = appUserService;
            _authenticationService = authenticationService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<ActionResult> SignIn([FromBody]Guid id)
        {
            var authResult = await _authenticationService.AuthenticateAsync(id);
            if (authResult == null)
                return Unauthorized();
            _logger.LogInformation("SignIn method by {id}", id);
            return Ok(authResult);
        }

        [AllowAnonymous]
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] AddAppUserDTO newUser)
        {
            var userId = await _appUserService.AddAsync(newUser);
            var authResult = await _authenticationService.AuthenticateAsync(userId);
            _logger.LogInformation("Created a user {id}", userId);
            return Ok(authResult);
        }

       // [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsersPaged([FromQuery] PagedQueryBase query)
        {
            var users = await _appUserService.GetPagedAsync(query);

            return Ok(users);
        }
    }
}
