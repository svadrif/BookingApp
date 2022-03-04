using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.MapDTO
{
    public class AddMapDTO
    {
        public int Floor { get; set; }
        public bool HasKitchen { get; set; }
        public bool HasConfRoom { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        public Guid OfficeId { get; set; }
    }
}