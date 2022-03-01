using Domain.Entities;

namespace Application.Interfaces
{
    public interface IParkingPlaceService 
    {
        Task<IEnumerable<ParkingPlace>> GetAllAsync();
        Task<ParkingPlace> GetByIdAsync(Guid Id);
        Task<ParkingPlace> AddAsync(ParkingPlace parkingPlace);
        Task<ParkingPlace> UpdateAsync(ParkingPlace parkingPlace);
        Task<bool> RemoveAsync(ParkingPlace parkingPlace);
        Task<IEnumerable<ParkingPlace>> SearchAsync(Guid? OfficeId);
    }
}
