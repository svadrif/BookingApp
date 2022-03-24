using Application.DTOs.BookingDTO;
using Application.Interfaces;
using Application.Interfaces.IServices;
using Application.Pagination;
using FluentEmail.Core;
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
        private readonly ILoggerManager _logger;
        public BookingController(IBookingService bookingService, ILoggerManager logger)
        private readonly IEmailService _emailService;

        public BookingController(IBookingService bookingService, IEmailService emailService)
        {
            _bookingService = bookingService;
            _logger = logger;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody] AddBookingDTO newBooking, [FromServices] IFluentEmail mailer)
        {
            var bookingId = await _bookingService.AddAsync(newBooking);
            if (bookingId != null)
                var email = await _emailService.SendAsync(newBooking, mailer);
            _logger.LogInfo($"called method {nameof(AddBooking)}");
            return Ok(bookingId);
        }

        [HttpGet]
        public async Task<IActionResult> GetBookingsPaged([FromQuery] PagedQueryBase query)
        {
            var bookings = await _bookingService.GetPagedAsync(query);
            _logger.LogInfo($"called method {nameof(GetBookingsPaged)}");
            return Ok(bookings);
        }

        [HttpGet("ByUserId/{userId:Guid}")]
        public async Task<IActionResult> GetBookingsByUserIdPaged([FromRoute] Guid userId, [FromQuery] PagedQueryBase query)
        {
            var bookings = await _bookingService.SearchByUserIdAsync(userId, query);
            _logger.LogInfo($"called method {nameof(GetBookingsByUserIdPaged)} by {userId}");
            return Ok(bookings);
        }

        [HttpGet("ByWorkPlaceId/{workPlaceId:Guid}")]
        public async Task<IActionResult> GetBookingsByWorkPlaceIdPaged([FromRoute] Guid workPlaceId, [FromQuery] PagedQueryBase query)
        {
            var bookings = await _bookingService.SearchByWorkPlaceIdAsync(workPlaceId, query);
            _logger.LogInfo($"called method {nameof(GetBookingsByWorkPlaceIdPaged)}");
            return Ok(bookings);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetBookingById([FromRoute] Guid id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            _logger.LogInfo($"called method {nameof(GetBookingById)} by {id}");
            return Ok(booking);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateBooking([FromRoute] Guid id, [FromBody] UpdateBookingDTO updatedBooking)
        {
            updatedBooking.Id = id;
            var booking = await _bookingService.UpdateAsync(updatedBooking);
            _logger.LogInfo($"called method {nameof(UpdateBooking)} by {id}");
            return Ok(booking);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteBooking([FromRoute] Guid id)
        {
            var result = await _bookingService.RemoveAsync(id);
            _logger.LogInfo($"called method {nameof(DeleteBooking)} by {id}");
            return Ok(result);
        }
    }
}
