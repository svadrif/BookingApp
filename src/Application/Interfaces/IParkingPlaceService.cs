using Application.DTOs.ParkingPlaceDTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IParkingPlaceService 
    {
        Task<IEnumerable<GetParkingPlaceDTO>> GetAllAsync();
        Task<GetParkingPlaceDTO> GetByIdAsync(Guid Id);
        Task<Guid> AddAsync(AddParkingPlaceDTO parkingPlaceDTO);
        Task<GetParkingPlaceDTO> UpdateAsync(UpdateParkingPlaceDTO parkingPlaceDTO);
        Task<bool> RemoveAsync(Guid Id);
        Task<IEnumerable<GetParkingPlaceDTO>> SearchByOfficeIdAsync(Guid? OfficeId);
    }
}
