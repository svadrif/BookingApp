using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAppUserService
    {
        Task<IEnumerable<AppUser>> GetAllAsync();
        Task<AppUser> GetByIdAsync(Guid Id);
        Task<AppUser> AddAsync(AddAppUserDTO appUserDTO);
        Task<AppUser> UpdateAsync(AppUser appUser);
        Task<bool> RemoveAsync(AppUser appUser);
        Task<IEnumerable<AppUser>> SearchAppUserAsync(string searchedValue);
    }
}
