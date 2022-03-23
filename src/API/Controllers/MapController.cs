using Application.DTOs.MapDTO;
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
    public class MapController : ControllerBase
    {
        private readonly IMapService _mapService;
        private readonly ILoggerManager _logger;
        public MapController(IMapService mapService, ILoggerManager logger)
        {
            _mapService = mapService;
            _logger = logger;
        }

        [Authorize(Roles = "MapEditor")]
        [HttpPost]
        public async Task<IActionResult> AddMap([FromBody] AddMapDTO newMap)
        {
            var mapId = await _mapService.AddAsync(newMap);
            _logger.LogInfo($"called method {nameof(AddMap)}");
            return Ok(mapId);
        }

        [HttpGet]
        public async Task<IActionResult> GetMapsPaged([FromQuery] PagedQueryBase query)
        {
            var maps = await _mapService.GetPagedAsync(query);
            _logger.LogInfo($"called method {nameof(GetMapsPaged)}");
            return Ok(maps);
        }

        [HttpGet("ByOfficeId/{officeId:Guid}")]
        public async Task<IActionResult> GetMapsByOfficePaged([FromRoute] Guid officeId, [FromQuery] PagedQueryBase query)
        {
            var maps = await _mapService.SearchByOfficeIdAsync(officeId, query);
            _logger.LogInfo($"called method {nameof(GetMapsByOfficePaged)} by {officeId}");
            return Ok(maps);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetMapById([FromRoute] Guid id)
        {
            var map = await _mapService.GetByIdAsync(id);
            _logger.LogInfo($"called method {nameof(GetMapById)} by {id}");
            return Ok(map);
        }

        [Authorize(Roles = "MapEditor")]
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateMap([FromRoute] Guid id, [FromBody] UpdateMapDTO updatedMap)
        {
            updatedMap.Id = id;
            var map = await _mapService.UpdateAsync(updatedMap);
            _logger.LogInfo($"called method {nameof(UpdateMap)} by {id}");
            return Ok(map);
        }

        [Authorize(Roles = "MapEditor")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteMap([FromRoute] Guid id)
        {
            var result = await _mapService.RemoveAsync(id);
            _logger.LogInfo($"called method {nameof(DeleteMap)} by {id}");
            return Ok(result);
        }
    }
}
