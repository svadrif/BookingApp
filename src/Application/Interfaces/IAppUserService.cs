using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAppUserService : IDisposable
    {
        Task<IEnumerable<AppUser>> GetAll();
        Task<AppUser> GetById(Guid Id);
        Task<AppUser> Add(AppUser appUser);
        Task<AppUser> Update(AppUser appUser);
        Task<bool> Remove(AppUser appUser);
        Task<IEnumerable<AppUser>> Search(Guid Id);
        Task<IEnumerable<AppUser>> SearchAppUser(string searchedValue);
    }
}
