using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.VacationDTO
{
    public class AddVacationDTO
    {
        public DateTimeOffset VacationStart { get; set; }
        public DateTimeOffset VacationEnd { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        public Guid UserId { get; set; }
    }
}
