using Application.Extentions;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Pagination;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Infrastructure.Repositories
{
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(
            ApplicationDbContext context,
            ILoggerManager logger
            ): base(context, logger)
        { }

        public async Task<AppUser> GetByTelegramIdAsync(long telegramId, bool tracking = false)
        {
            try
            {
                return await Search(x => x.TelegramId == telegramId,
                                    tracking)
                            .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                //_logger.Error(ex, "{Repo} GetByTelegramIdAsync method has generated an error", typeof(AppUserRepository));
                return new AppUser();
            }
        }

        public async Task<AppUser> GetByEmailAsync(string email, bool tracking = false)
        {
            try
            {
                return await Search(x => x.Email == email,
                                    tracking)
                            .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                //_logger.Error(ex, "{Repo} GetByEmailAsync method has generated an error", typeof(AppUserRepository));
                return new AppUser();
            }
        }

        public async Task<PagedList<AppUser>> GetPagedAsync(PagedQueryBase query, bool tracking = false)
        {
            try
            {
                throw new Exception();

                return await GetAll(tracking)
                        .Sort(query.SortOn, query.SortDirection)
                        .ToPagedListAsync(query);
            }
            catch(Exception ex)
            {
                _logger.LogError($"GetPagedAsync method has generated an error {ex}");
                //_logger.Error(ex, "{Repo} GetPagedAsync method has generated an error", typeof(AppUserRepository));
                //_logger.LogError($"Something went wrong in the {nameof(GetPagedAsync)}action {ex}");
                return new PagedList<AppUser>(new List<AppUser>(),0,0,0);
            }
        }
    }
}
