using Application.DTOs.AppUserDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/appuser")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        public AppUserController(IAppUserService appUserService, IMapper mapper)
        {
            _appUserService = appUserService; 
            _mapper = mapper;
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
    }
}
