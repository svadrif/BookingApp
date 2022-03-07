using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.WorkPlaceDTO
{
    public class UpdateWorkPlaceDTO
    {
        [Required(ErrorMessage = "This field {0} is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Number { get; set; }
        public SeatsType Type { get; set; }
        public bool IsNextToWindow { get; set; }
        public bool HasPC { get; set; }
        public bool HasMonitor { get; set; }
        public bool HasKeyboard { get; set; }
        public bool HasMouse { get; set; }
        public bool HasHeadset { get; set; }
        public bool IsBlocked { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        public Guid MapId { get; set; }
    }
}
