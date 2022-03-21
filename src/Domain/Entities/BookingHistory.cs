using System;
using Domain.Common;

namespace Domain.Entities
{
    public class BookingHistory : IHasKey<Guid>
    {
        public Guid Id { get; set; }

        public string? Country { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public Guid? OfficeId { get; set; } = null;
        
        public int? Floor { get; set; } = null;
        public bool? HasKitchen { get; set; } = null;
        public bool? HasConfRoom { get; set; } = null;
        public Guid? MapId { get; set; } = null;
        
        public bool? IsNextToWindow { get; set; } = null;
        public bool? HasPC { get; set; } = null;
        public bool? HasMonitor { get; set; } = null;
        public bool? HasKeyboard { get; set; } = null;
        public bool? HasMouse { get; set; } = null;
        public bool? HasHeadset { get; set; } = null;
        public Guid? WorkPlaceId { get; set; } = null;
        
        public DateTimeOffset? BookingStart { get; set; } = null;
        public DateTimeOffset? BookingEnd { get; set; } = null;
        public bool? IsRecurring { get; set; } = null;
        public string? Frequancy { get; set; } = string.Empty;
        public Guid? ParkingPlaceId { get; set; } = null;
        
        //EF relation
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
    }
}