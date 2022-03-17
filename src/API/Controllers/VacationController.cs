using Application.DTOs.VacationDTO;
using Application.Interfaces.IServices;
using Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VacationController : ControllerBase
    {
        private readonly IVacationService _vacationService;

        public VacationController(IVacationService vacationService)
        {
            _vacationService = vacationService;
        }

        [HttpPost]
        public async Task<IActionResult> AddVacation([FromBody] AddVacationDTO newVacation)
        {
            var vacatoinId = await _vacationService.AddAsync(newVacation);
            return Ok(vacatoinId);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<IActionResult> GetVacationsPaged([FromQuery] PagedQueryBase query)
        {
            var vacations = await _vacationService.GetPagedAsync(query);
            return Ok(vacations);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetVacationById([FromRoute] Guid id)
        {
            var vacation = await _vacationService.GetByIdAsync(id);
            return Ok(vacation);
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateVacation([FromRoute] Guid id, [FromBody] UpdateVacationDTO updateVacation)
        {
            var vacation = await _vacationService.UpdateAsync(updateVacation);
            return Ok(vacation);
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteVacation([FromRoute] Guid id)
        {
            var result = await _vacationService.RemoveAsync(id);
            return Ok(result);
        }
    }
}
