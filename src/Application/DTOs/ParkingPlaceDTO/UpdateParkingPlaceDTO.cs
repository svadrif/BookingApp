using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.DTOs.ParkingPlaceDTO
{
    public class UpdateParkingPlaceDTO
    {
        [Required(ErrorMessage = "This field {0} is required")]
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 1)]
        public string Number { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        public Guid OfficeId { get; set; }
    }
}
