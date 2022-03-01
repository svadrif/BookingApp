using Application.DTOs.VacationDTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IVacationService  
    {
        Task<IEnumerable<Vacation>> GetAllAsync();
        Task<Vacation> GetByIdAsync(Guid Id);
        Task<Vacation> AddAsync(AddVacationDTO vacationDTO);
        Task<Vacation> UpdateAsync(Vacation vacation);
        Task<bool> RemoveAsync(Vacation vacation);
        Task<IEnumerable<Vacation>> SearchByUserIdAsync(Guid UserId);
    }
}
