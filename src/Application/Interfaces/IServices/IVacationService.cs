using Application.DTOs.VacationDTO;
using Application.Pagination;

namespace Application.Interfaces.IServices
{
    public interface IVacationService
    {
        Task<PagedList<GetVacationDTO>> GetPagedAsync(PagedQueryBase query);
        Task<GetVacationDTO> GetByIdAsync(Guid Id);
        Task<Guid> AddAsync(AddVacationDTO vacationDTO);
        Task<GetVacationDTO> UpdateAsync(UpdateVacationDTO vacationDTO);
        Task<bool> RemoveAsync(Guid Id);
        Task<IEnumerable<GetVacationDTO>> SearchByUserIdAsync(Guid UserId);
    }
}
