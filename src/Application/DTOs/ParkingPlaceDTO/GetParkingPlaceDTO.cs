namespace Application.DTOs.ParkingPlaceDTO
{
    public class GetParkingPlaceDTO
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public Guid OfficeId { get; set; }
    }
}
