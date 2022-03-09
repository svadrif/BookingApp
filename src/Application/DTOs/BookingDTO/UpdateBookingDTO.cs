using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.BookingDTO
{
    public class UpdateBookingDTO
    {
        [Required(ErrorMessage = "This field {0} is required")]
        public Guid Id { get; set; }
        public DateTimeOffset BookingStart { get; set; }
        public DateTimeOffset BookingEnd { get; set; }
        public bool IsRecurring { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Frequancy { get; set; }
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        public Guid? ParkingPlaceId { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        public Guid WorkPlaceId { get; set; }
    }
}
