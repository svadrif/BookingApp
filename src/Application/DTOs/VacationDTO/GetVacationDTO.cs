namespace Application.DTOs.VacationDTO
{
    public class GetVacationDTO
    {
        public Guid Id { get; set; }
        public DateTime VacationStart { get; set; }
        public DateTime VacationEnd { get; set; }
        public Guid UserId { get; set; }
    }
}
