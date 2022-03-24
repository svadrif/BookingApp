using Application.DTOs.AppUserDTO;
using Application.Interfaces;
using Application.Interfaces.IServices;
using Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILoggerManager _logger;
        public UserController(IAppUserService appUserService, IAuthenticationService authenticationService, ILoggerManager logger)
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
            _logger.LogInfo($"{nameof(SignIn)} method by {id}");
            if (authResult == null)
                return Unauthorized();

            return Ok(authResult);
        }

        [AllowAnonymous]
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] AddAppUserDTO newUser)
        {
            var userId = await _appUserService.AddAsync(newUser);
            var authResult = await _authenticationService.AuthenticateAsync(userId);
            _logger.LogInfo($"called method {nameof(SignUp)} by {userId}");
            return Ok(authResult);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUsersPaged([FromQuery] PagedQueryBase query)
        {
            var users = await _appUserService.GetPagedAsync(query);
            _logger.LogInfo($"called method {nameof(GetUsersPaged)}");
            return Ok(users);
        }
    }
}
