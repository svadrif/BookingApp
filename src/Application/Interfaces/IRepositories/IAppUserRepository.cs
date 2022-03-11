using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IAppUserRepository : IGenericRepository<AppUser>, IPageable<AppUser>
    {
        Task<AppUser> GetByTelegramId(long telegramId, bool tracking = false);
    }
}
