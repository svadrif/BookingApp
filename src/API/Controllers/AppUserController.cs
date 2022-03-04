using Application.DTOs.AppUserDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/appuser")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        private readonly IOptions<AppSettings> _appSettings;

        public AppUserController(IAppUserService appUserService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _appUserService = appUserService; 
            _mapper = mapper;
            _appSettings = appSettings;
        }

        [HttpGet("get-all-appusers")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _appUserService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<GetAppUserDTO>>(users));
        }

        [HttpGet("get-appuser-by-id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            AppUser user = await _appUserService.GetByIdAsync(id);

            return Ok(_mapper.Map<GetAppUserDTO>(user));
        }

        [HttpPost("add-appuser")]
        public async Task<IActionResult> Add(AddAppUserDTO addAppUserDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Data is not Valid");

            var user = await _appUserService.AddAsync(addAppUserDTO);
            
            if (user == null) 
                return BadRequest("null");

            return Ok(user);
        }
        
        [HttpPut("update-appuser")]
        public async Task<IActionResult> Update(Guid id, UpdateAppUserDTO updateAppUserDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Data is not Valid");

            await _appUserService.UpdateAsync(_mapper.Map<AppUser>(updateAppUserDTO));

            return Ok(updateAppUserDTO);
        }
        
        [HttpDelete("delete-appuser")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var user = await _appUserService.GetByIdAsync(id);

            await _appUserService.RemoveAsync(user);

            return Ok("removed");
        }

        [HttpGet("search-by-cradentials")]
        public async Task<IActionResult> SearchUser(string anyUsersCradential)
        {
            IEnumerable<AppUser> user = await _appUserService.SearchAppUserAsync(anyUsersCradential);

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("get-token")]
        public async Task<ActionResult<AppUserAuthInfo>> Login(Guid id)
        {
            AppUserAuthInfo userAuth = new AppUserAuthInfo();

            if (id == null) return BadRequest("null");

            AppUser appUser = await _appUserService.GetByIdAsync(id);

            if(appUser != null)
            {
                var key = Encoding.ASCII.GetBytes(_appSettings.Value.SecretKey);

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                            new Claim(ClaimTypes.GivenName, appUser.FirstName),
                            new Claim(ClaimTypes.Name, appUser.LastName),
                            new Claim(ClaimTypes.Role, appUser.Role.ToString())
                        }),
                    Expires = DateTime.UtcNow.AddDays(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token  = handler.CreateToken(descriptor);
                userAuth.Token = handler.WriteToken(token);
                userAuth.GetAppUserDTO = _mapper.Map<GetAppUserDTO>(appUser);
            }
            if (string.IsNullOrEmpty(userAuth.Token)) return Unauthorized("UnAuthorized");

            return Ok(userAuth);
        }
    }
}