using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.BookingDTO
{
    public class AddBookingDTO
    {
        [Required(ErrorMessage = "This field {0} is required")]
        public DateTimeOffset BookingStart { get; set; }
        public DateTimeOffset BookingEnd { get; set; }
        public bool IsRecurring { get; set; }
        public string Frequancy { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field {0} is required")]
        public Guid UserId { get; set; }
        public Guid? ParkingPlaceId { get; set; } = null;

        [Required(ErrorMessage = "This field {0} is required")]
        public Guid WorkPlaceId { get; set; }
    }
}
