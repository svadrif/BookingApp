using Application.DTOs.ParkingPlaceDTO;
using Application.Pagination;

namespace Application.Interfaces.IServices
{
    public interface IParkingPlaceService
    {
        Task<PagedList<GetParkingPlaceDTO>> GetPagedAsync(PagedQueryBase query);
        Task<GetParkingPlaceDTO> GetByIdAsync(Guid Id);
        Task<Guid> AddAsync(AddParkingPlaceDTO parkingPlaceDTO);
        Task<GetParkingPlaceDTO> UpdateAsync(UpdateParkingPlaceDTO parkingPlaceDTO);
        Task<bool> RemoveAsync(Guid Id);
        Task<PagedList<GetParkingPlaceDTO>> SearchByOfficeIdAsync(Guid OfficeId, PagedQueryBase query);
    }
}
