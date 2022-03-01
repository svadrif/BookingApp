using Domain.Entities;

namespace Application.Interfaces
{
    public interface IParkingPlaceService 
    {
        Task<IEnumerable<ParkingPlace>> GetAll();
        Task<ParkingPlace> GetById(Guid Id);
        Task<ParkingPlace> Add(ParkingPlace parkingPlace);
        Task<ParkingPlace> Update(ParkingPlace parkingPlace);
        Task<bool> Remove(ParkingPlace parkingPlace);
        Task<IEnumerable<ParkingPlace>> Search(Guid OfficeId);
    }
}
