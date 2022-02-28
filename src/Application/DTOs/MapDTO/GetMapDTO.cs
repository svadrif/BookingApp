namespace Application.DTOs.MapDTO
{
    public class GetMapDTO
    {
        public Guid Id { get; set; }
        public int Floor { get; set; }
        public bool HasKitchen { get; set; }
        public bool HasConfRoom { get; set; }
        public Guid OfficeId { get; set; }
    }
}
