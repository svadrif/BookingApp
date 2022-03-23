using Application.Extentions;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Pagination;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class VacationRepository : GenericRepository<Vacation>, IVacationRepository
    {
        public VacationRepository(ApplicationDbContext context, ILoggerManager logger) : base(context, logger) { }

        public async Task<PagedList<Vacation>> GetPagedAsync(PagedQueryBase query, bool tracking = false)
        {
            try
            {
                return await GetAll(tracking)
                            .Sort(query.SortOn, query.SortDirection)
                            .ToPagedListAsync(query);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetPagedAsync)} action {ex}");
                return new PagedList<Vacation>(new List<Vacation>(), 0, 0, 0);
            }
        }

        public async Task<PagedList<Vacation>> GetPagedByUserIdAsync(Guid userId, PagedQueryBase query, bool tracking = false)
        {
            try
            {
                return await Search(x => x.UserId == userId,
                                    tracking)
                            .Sort(query.SortOn, query.SortDirection)
                            .ToPagedListAsync(query);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetPagedByUserIdAsync)} action {ex}");
                return new PagedList<Vacation>(new List<Vacation>(), 0, 0, 0);
            }
        }
    }
}
