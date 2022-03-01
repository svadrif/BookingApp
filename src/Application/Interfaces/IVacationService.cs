using Domain.Entities;

namespace Application.Interfaces
{
    public interface IVacationService  
    {
        Task<IEnumerable<Vacation>> GetAllAsync();
        Task<Vacation> GetByIdAsync(Guid Id);
        Task<Vacation> AddAsync(Vacation vacation);
        Task<Vacation> UpdateAsync(Vacation vacation);
        Task<bool> RemoveAsync(Vacation vacation);
        Task<IEnumerable<Vacation>> SearchAsync(Guid UserId);
    }
}
