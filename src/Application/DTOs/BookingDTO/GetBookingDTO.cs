namespace Application.DTOs.BookingDTO
{
    public class GetBookingDTO
    {
        public Guid Id { get; set; }
        public DateTimeOffset BookingStart { get; set; }
        public DateTimeOffset BookingEnd { get; set; }
        public bool IsRecurring { get; set; }
        public string Frequancy { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Guid? ParkingPlaceId { get; set; }
        public Guid WorkPlaceId { get; set; }
    }
}
