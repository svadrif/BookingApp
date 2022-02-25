using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.VacationDTO
{
    public class UpdateVacationDTO
    {
        [Required(ErrorMessage = "This field {0} is required")]
        public Guid Id { get; set; }
        public DateTime VacationStart { get; set; }
        public DateTime VacationEnd { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        public Guid UserId { get; set; }
    }
}
