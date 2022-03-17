using Application.DTOs.WorkPlaceDTO;
using Application.Interfaces.IServices;
using Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkPlaceController : ControllerBase
    {
        private readonly IWorkPlaceService _workPlaceService;

        public WorkPlaceController(IWorkPlaceService workPlaceService)
        {
            _workPlaceService = workPlaceService;
        }

        [Authorize(Roles = "MapEditor")]
        [HttpPost]
        public async Task<IActionResult> AddWorkPlace([FromBody] AddWorkPlaceDTO newWorkPlace)
        {
            var workPlaceId = await _workPlaceService.AddAsync(newWorkPlace);
            return Ok(workPlaceId);
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkPlacesPaged([FromQuery] PagedQueryBase query)
        {
            var workPlaces = await _workPlaceService.GetPagedAsync(query);
            return Ok(workPlaces);
        }

        [HttpGet("ByMapId/{mapId:Guid}")]
        public async Task<IActionResult> GetMapsByOfficePaged([FromRoute] Guid mapId, [FromQuery] PagedQueryBase query)
        {
            var workPlaces = await _workPlaceService.SearchByMapIdAsync(mapId, query);
            return Ok(workPlaces);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetWorkPlaceById([FromRoute] Guid id)
        {
            var workPlace = await _workPlaceService.GetByIdAsync(id);
            return Ok(workPlace);
        }

        [Authorize(Roles = "MapEditor")]
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateWorkPlace([FromRoute] Guid id, [FromBody] UpdateWorkPlaceDTO updatedWorkPlace)
        {
            updatedWorkPlace.Id = id;
            var workPlace = await _workPlaceService.UpdateAsync(updatedWorkPlace);
            return Ok(workPlace);
        }

        [Authorize(Roles = "MapEditor")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteWorkPlace([FromRoute] Guid id)
        {
            var result = await _workPlaceService.RemoveAsync(id);
            return Ok(result);
        }
    }
}
