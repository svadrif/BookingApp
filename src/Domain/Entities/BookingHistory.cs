using System;
using Domain.Common;

namespace Domain.Entities
{
    public class BookingHistory : BaseEntity<Guid>
    {
        public Guid Id { get; set; }
        
        public string? Country { get; set; }
        public string? City { get; set; }
        public Guid? OfficeId { get; set; }
        
        public int? Floor { get; set; }
        public bool? HasKitchen { get; set; }
        public bool? HasConfRoom { get; set; }
        public Guid? MapId { get; set; }
        
        public bool? IsNextToWindow { get; set; }
        public bool? HasPC { get; set; }
        public bool? HasMonitor { get; set; }
        public bool? HasKeyboard { get; set; }
        public bool? HasMouse { get; set; }
        public bool? HasHeadset { get; set; }
        public Guid? WorkPlaceId { get; set; }
        
        public DateTimeOffset? BookingStart { get; set; }
        public DateTimeOffset? BookingEnd { get; set; }
        public bool? IsRecurring { get; set; }
        public string? Frequancy { get; set; } = string.Empty;
        public Guid? ParkingPlaceId { get; set; }
        
        //EF relation
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
    }
}