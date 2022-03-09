using Application.Extentions;
using Application.Interfaces;
using Application.Pagination;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class OfficeRepository : GenericRepository<Office>, IOfficeRepository
    {
        public OfficeRepository(ApplicationDbContext context) : base(context) { }

        public async Task<PagedList<Office>> GetPagedAsync(PagedQueryBase query, bool tracking)
        {
            return await GetAll(tracking).ToPagedListAsync(query);
        }
    }
}
