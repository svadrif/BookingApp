namespace Application.DTOs.VacationDTO
{
    public class GetVacationDTO
    {
        public Guid Id { get; set; }
        public DateTimeOffset VacationStart { get; set; }
        public DateTimeOffset VacationEnd { get; set; }
        public Guid UserId { get; set; }
    }
}
