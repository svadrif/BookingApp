using Application.DTOs.ParkingPlaceDTO;
using Application.Interfaces.IServices;
using Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingPlaceController : ControllerBase
    {
        private readonly IParkingPlaceService _parkingPlaceService;

        public ParkingPlaceController(IParkingPlaceService parkingPlaceService)
        {
            _parkingPlaceService = parkingPlaceService;
        }

        [Authorize(Roles = "MapEditor")]
        [HttpPost]
        public async Task<IActionResult> AddParkingPlace([FromBody] AddParkingPlaceDTO newParkingPlace)
        {
            var parkingPlaceId = await _parkingPlaceService.AddAsync(newParkingPlace);
            return Ok(parkingPlaceId);
        }

        [HttpGet]
        public async Task<IActionResult> GetParkingPlacesPaged([FromQuery] PagedQueryBase query)
        {
            var parkingPlaces = await _parkingPlaceService.GetPagedAsync(query);
            return Ok(parkingPlaces);
        }

        [HttpGet("ByOfficeId/{officeId:Guid}")]
        public async Task<IActionResult> GetParkingPlacesByOfficePaged([FromRoute] Guid officeId, [FromQuery] PagedQueryBase query)
        {
            var parkingPlaces = await _parkingPlaceService.SearchByOfficeIdAsync(officeId, query);
            return Ok(parkingPlaces);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetParkingPlaceById([FromRoute] Guid id)
        {
            var parkingPlace = await _parkingPlaceService.GetByIdAsync(id);
            return Ok(parkingPlace);
        }

        [Authorize(Roles = "MapEditor")]
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateParkingPlace([FromRoute] Guid id, [FromBody] UpdateParkingPlaceDTO updatedParkingPlace)
        {
            updatedParkingPlace.Id = id;
            var parkingPlace = await _parkingPlaceService.UpdateAsync(updatedParkingPlace);
            return Ok(parkingPlace);
        }

        [Authorize(Roles = "MapEditor")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteParkingPlace([FromRoute] Guid id)
        {
            var result = await _parkingPlaceService.RemoveAsync(id);
            return Ok(result);
        }
    }
}
