using Domain.Enums;

namespace Application.DTOs.WorkPlaceDTO
{
    public class GetWorkPlaceDTO
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public SeatsType Type { get; set; }
        public bool NextToWindow { get; set; }
        public bool HasPC { get; set; }
        public bool HasMonitor { get; set; }
        public bool HasKeyboard { get; set; }
        public bool HasMouse { get; set; }
        public bool HasHeadset { get; set; }
        public Guid MapId { get; set; }

    }
}
