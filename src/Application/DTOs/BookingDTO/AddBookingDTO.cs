using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.BookingDTO
{
    public class AddBookingDTO
    {
        [Required(ErrorMessage = "This field {0} is required")]
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }
        public bool IsRecurring { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Frequancy { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        public Guid? ParkingPlaceId { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        public Guid WorkPlaceId { get; set; }
    }
}
