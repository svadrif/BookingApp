using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.VacationDTO
{
    public class UpdateVacationDTO
    {
        [Required(ErrorMessage = "This field {0} is required")]
        public Guid Id { get; set; }
        public DateTimeOffset VacationStart { get; set; }
        public DateTimeOffset VacationEnd { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        public Guid UserId { get; set; }
    }
}
