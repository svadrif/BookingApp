using Domain.Enums;

namespace Application.DTOs.AppUserDTO
{
    public class GetAppUserDTO
    {
        public Guid Id { get; set; }
        public long TelegramId { get; set; }
        public Guid LastCommandId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
        public DateTimeOffset EmploymentStart { get; set; }
        public DateTimeOffset? EmploymentEnd { get; set; }
        public Guid? PrefferdWorkPlaceId { get; set; }
    }
}
