using Application.DTOs.VacationDTO;
using Application.Interfaces;
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
        private readonly ILoggerManager _logger;
        public VacationController(IVacationService vacationService, ILoggerManager logger)
        {
            _vacationService = vacationService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddVacation([FromBody] AddVacationDTO newVacation)
        {
            var vacatoinId = await _vacationService.AddAsync(newVacation);
            _logger.LogInfo($"called method {nameof(AddVacation)}");
            return Ok(vacatoinId);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<IActionResult> GetVacationsPaged([FromQuery] PagedQueryBase query)
        {
            var vacations = await _vacationService.GetPagedAsync(query);
            _logger.LogInfo($"called method {nameof(GetVacationsPaged)}");
            return Ok(vacations);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetVacationById([FromRoute] Guid id)
        {
            var vacation = await _vacationService.GetByIdAsync(id);
            _logger.LogInfo($"called method {nameof(GetVacationById)} by {id}");
            return Ok(vacation);
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateVacation([FromRoute] Guid id, [FromBody] UpdateVacationDTO updateVacation)
        {
            updateVacation.Id = id;
            var vacation = await _vacationService.UpdateAsync(updateVacation);
            _logger.LogInfo($"called method {nameof(UpdateVacation)} by {id}");
            return Ok(vacation);
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteVacation([FromRoute] Guid id)
        {
            var result = await _vacationService.RemoveAsync(id);
            _logger.LogInfo($"called method {nameof(DeleteVacation)} by {id}");
            return Ok(result);
        }
    }
}
