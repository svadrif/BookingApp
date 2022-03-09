using Application.DTOs.VacationDTO;

namespace Application.Interfaces
{
    public interface IVacationService  
    {
        Task<IEnumerable<GetVacationDTO>> GetAllAsync();
        Task<GetVacationDTO> GetByIdAsync(Guid Id);
        Task<Guid> AddAsync(AddVacationDTO vacationDTO);
        Task<GetVacationDTO> UpdateAsync(UpdateVacationDTO vacationDTO);
        Task<bool> RemoveAsync(Guid Id);
        Task<IEnumerable<GetVacationDTO>> SearchByUserIdAsync(Guid UserId);
    }
}
