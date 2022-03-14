using Application.Extentions;
using Application.Interfaces;
using Application.Pagination;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class MapRepository : GenericRepository<Map>, IMapRepository
    {
        public MapRepository(ApplicationDbContext context) : base(context) { }

        public async Task<PagedList<Map>> GetPagedAsync(PagedQueryBase query, bool tracking = false)
        {
            return await GetAll(tracking)
                        .ToPagedListAsync(query);
        }

        public async Task<PagedList<Map>> GetPagedByAttributesAsync(bool hasKitchen, bool hasConfRoom, PagedQueryBase query, bool tracking = false)
        {
            return await Search(x => x.HasKitchen == hasKitchen
                                && x.HasConfRoom == hasConfRoom,
                                tracking)
                        .ToPagedListAsync(query);
        }

        public async Task<PagedList<Map>> GetPagedByOfficeIdAsync(Guid officeId, PagedQueryBase query, bool tracking = false)
        {
            return await Search(x => x.OfficeId == officeId,
                                tracking)
                        .ToPagedListAsync(query);
        }
    }
}
