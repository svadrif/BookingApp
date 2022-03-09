using Application.Extentions;
using Application.Interfaces;
using Application.Pagination;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(ApplicationDbContext context) : base(context) { }

        public async Task<AppUser> GetByTelegramId(long telegramId, bool tracking = false)
        {
            return await base.Search(x => x.TelegramId == telegramId, tracking).FirstOrDefaultAsync();
        }

        public async Task<PagedList<AppUser>> GetPagedAsync(PagedQueryBase query, bool tracking)
        {
            return await GetAll(tracking).ToPagedListAsync(query);
        }
    }
}
