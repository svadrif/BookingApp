using Application.DTOs.OfficeDTO;
using Application.Interfaces.IServices;
using Application.Pagination;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace API.Controllers
{
    [Authorize]
    [Route("api/report")]
    public class ReportController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IMapService _mapService;
        private readonly IWorkPlaceService _workPlaceService;
       
        public ReportController(IBookingService bookingService,IMapService mapService,IWorkPlaceService workPlaceService)
        {
            _bookingService = bookingService;
            _mapService = mapService;
            _workPlaceService = workPlaceService;
        }

        [Authorize(Roles = "Manager")]
        [Route("ByOfficeOnRange/{officeId:Guid}/{start}/{end}")]
        [HttpGet]
        public IActionResult GetReportOnOfficeByRange([FromQuery]PagedQueryBase bookingQuery,PagedQueryBase mapQuery,PagedQueryBase workPlaceQuery,[FromRoute] Guid officeId,int start,int end)
        {
            DateTimeOffset startDate = new DateTimeOffset(DateTimeOffset.UtcNow.Year, start, 1, 12, 0, 0,
                DateTimeOffset.Now.Offset);
            DateTimeOffset endDate = new DateTimeOffset(DateTimeOffset.UtcNow.Year, end, 1, 12, 0, 0,
                DateTimeOffset.Now.Offset);

            var maps = _mapService.SearchByOfficeIdAsync(officeId, mapQuery).Result;
            var bookings = _bookingService.GetPagedAsync(bookingQuery).Result;
            var workPlaces = _workPlaceService.GetPagedAsync(workPlaceQuery).Result;
            var res1 = maps.Join(workPlaces, m => m.Id, w => w.MapId, (m, w) => new
            {
                w.Id
            }).ToList();
            var res2 = bookings.Join(res1, b => b.WorkPlaceId, r1 => r1.Id, (b, r1) => new
            {
                b.Id,
                b.BookingStart,
                b.BookingEnd
            }).ToList().Where(b=>b.BookingStart>=startDate  && b.BookingEnd<=endDate);
            return Ok(res2);
        }
    }
}
