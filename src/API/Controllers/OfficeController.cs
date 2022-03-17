using Application.DTOs.OfficeDTO;
using Application.Interfaces.IServices;
using Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private readonly IOfficeService _officeService;

        public OfficeController(IOfficeService officeService)
        {
            _officeService = officeService;
        }

        [Authorize(Roles = "MapEditor")]
        [HttpPost]
        public async Task<IActionResult> AddOffice([FromBody] AddOfficeDTO newOffice)
        {
            var officeId = await _officeService.AddAsync(newOffice);
            return Ok(officeId);
        }

        [HttpGet]
        public async Task<IActionResult> GetOfficesPaged([FromQuery] PagedQueryBase query)
        {
            var offices = await _officeService.GetPagedAsync(query);
            return Ok(offices);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetOfficeById([FromRoute] Guid id)
        {
            var office = await _officeService.GetByIdAsync(id);
            return Ok(office);
        }

        [Authorize(Roles = "MapEditor")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteOffice([FromRoute] Guid id)
        {
            var result = await _officeService.RemoveAsync(id);
            return Ok(result);
        }
    }
}
