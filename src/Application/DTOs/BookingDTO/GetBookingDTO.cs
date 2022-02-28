using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.BookingDTO
{
    public class GetBookingDTO
    {
        public Guid Id { get; set; }
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }
        public bool IsRecurring { get; set; }
        public string Frequancy { get; set; }
        public bool IsActive { get; set; }
        public Guid UserId { get; set; }
        public Guid? ParkingPlaceId { get; set; }
        public Guid WorkPlaceId { get; set; }
    }
}
