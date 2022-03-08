using Application.Pagination;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAppUserRepository : IGenericRepository<AppUser>
    {
        Task<AppUser> GetByTelegramId(long telegramId, bool tracking);
        Task<PagedList<AppUser>> GetPagedAsync(PagedQueryBase query, bool tracking);
    }
}
