using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAppUserService
    {
        Task<IEnumerable<AppUser>> GetAllAsync();
        Task<AppUser> GetByIdAsync(Guid Id);
        Task<AppUser> AddAsync(AppUser appUser);
        Task<AppUser> UpdateAsync(AppUser appUser);
        Task<bool> RemoveAsync(AppUser appUser);
        Task<IEnumerable<AppUser>> SearchAsync(Guid Id);
        Task<IEnumerable<AppUser>> SearchAppUserAsync(string searchedValue);
    }
}
