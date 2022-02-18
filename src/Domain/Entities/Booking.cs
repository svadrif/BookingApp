using Domain.Common;
using System;

namespace Domain.Entities
{
    public class Booking : BaseEntity<Guid>
    {
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }
        public bool IsRecurring { get; set; }
        public string Frequancy { get; set; }
        public bool IsActive { get; set; }

        /* EF Relation */
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public Guid? ParkingPlaceId { get; set; }
        public ParkingPlace ParkingPlace { get; set; }

        public Guid WorkPlaceId { get; set; }
        public WorkPlace WorkPlace { get; set; }
    }
}
