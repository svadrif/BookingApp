using Application.Extentions;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Pagination;
using Domain.Entities;
using Infrastructure.Context;
using Serilog;

namespace Infrastructure.Repositories
{
    public class MapRepository : GenericRepository<Map>, IMapRepository
    {
        public MapRepository(ApplicationDbContext context, ILoggerManager logger) : base(context, logger) { }

        public async Task<PagedList<Map>> GetPagedAsync(PagedQueryBase query, bool tracking = false)
        {
            return await GetAll(tracking)
                        .Sort(query.SortOn, query.SortDirection)
                        .ToPagedListAsync(query);
        }

        public async Task<PagedList<Map>> GetPagedByAttributesAsync(bool hasKitchen, bool hasConfRoom, PagedQueryBase query, bool tracking = false)
        {
            return await Search(x => x.HasKitchen == hasKitchen
                                && x.HasConfRoom == hasConfRoom,
                                tracking)
                        .Sort(query.SortOn, query.SortDirection)
                        .ToPagedListAsync(query);
        }

        public async Task<PagedList<Map>> GetPagedByOfficeIdAsync(Guid officeId, PagedQueryBase query, bool tracking = false)
        {
            return await Search(x => x.OfficeId == officeId,
                                tracking)
                        .Sort(query.SortOn, query.SortDirection)
                        .ToPagedListAsync(query);
        }
    }
}
