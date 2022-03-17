using Application.DTOs.MapDTO;
using Application.Interfaces.IServices;
using Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly IMapService _mapService;

        public MapController(IMapService mapService)
        {
            _mapService = mapService;
        }

        [Authorize(Roles = "MapEditor")]
        [HttpPost]
        public async Task<IActionResult> AddMap([FromBody] AddMapDTO newMap)
        {
            var mapId = await _mapService.AddAsync(newMap);
            return Ok(mapId);
        }

        [HttpGet]
        public async Task<IActionResult> GetMapsPaged([FromQuery] PagedQueryBase query)
        {
            var maps = await _mapService.GetPagedAsync(query);
            return Ok(maps);
        }

        [HttpGet("ByOfficeId/{officeId:Guid}")]
        public async Task<IActionResult> GetMapsByOfficePaged([FromRoute] Guid officeId, [FromQuery] PagedQueryBase query)
        {
            var maps = await _mapService.SearchByOfficeIdAsync(officeId, query);
            return Ok(maps);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetMapById([FromRoute] Guid id)
        {
            var map = await _mapService.GetByIdAsync(id);
            return Ok(map);
        }

        [Authorize(Roles = "MapEditor")]
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateMap([FromRoute] Guid id, [FromBody] UpdateMapDTO updatedMap)
        {
            updatedMap.Id = id;
            var map = await _mapService.UpdateAsync(updatedMap);
            return Ok(map);
        }

        [Authorize(Roles = "MapEditor")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteMap([FromRoute] Guid id)
        {
            var result = await _mapService.RemoveAsync(id);
            return Ok(result);
        }
    }
}
