using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.DTOs.MapDTO
{
    public class UpdateMapDTO
    {
        [Required(ErrorMessage = "This field {0} is required")]
        [JsonIgnore]
        public Guid Id { get; set; }
        public int Floor { get; set; }
        public bool HasKitchen { get; set; }
        public bool HasConfRoom { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        public Guid OfficeId { get; set; }
    }
}
