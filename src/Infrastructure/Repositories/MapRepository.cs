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

        public async Task<PagedList<Map>> GetPagedAsync(PagedQueryBase query, bool tracking)
        {
            return await GetAll(tracking).ToPagedListAsync(query);
        }
    }
}
