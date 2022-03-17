using Application.DTOs.BookingDTO;
using Application.Interfaces.IServices;
using Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody] AddBookingDTO newBooking)
        {
            var bookingId = await _bookingService.AddAsync(newBooking);
            return Ok(bookingId);
        }

        [HttpGet]
        public async Task<IActionResult> GetBookingsPaged([FromQuery] PagedQueryBase query)
        {
            var bookings = await _bookingService.GetPagedAsync(query);
            return Ok(bookings);
        }

        [HttpGet("ByUserId/{userId:Guid}")]
        public async Task<IActionResult> GetBookingsByUserIdPaged([FromRoute] Guid userId, [FromQuery] PagedQueryBase query)
        {
            var bookings = await _bookingService.SearchByUserIdAsync(userId, query);
            return Ok(bookings);
        }

        [HttpGet("ByWorkPlaceId/{workPlaceId:Guid}")]
        public async Task<IActionResult> GetBookingsByWorkPlaceIdPaged([FromRoute] Guid workPlaceId, [FromQuery] PagedQueryBase query)
        {
            var bookings = await _bookingService.SearchByWorkPlaceIdAsync(workPlaceId, query);
            return Ok(bookings);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetBookingById([FromRoute] Guid id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            return Ok(booking);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateBooking([FromRoute] Guid id, [FromBody] UpdateBookingDTO updatedBooking)
        {
            updatedBooking.Id = id;
            var booking = await _bookingService.UpdateAsync(updatedBooking);
            return Ok(booking);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteBooking([FromRoute] Guid id)
        {
            var result = await _bookingService.RemoveAsync(id);
            return Ok(result);
        }
    }
}
