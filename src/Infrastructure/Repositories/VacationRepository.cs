using Application.Extentions;
using Application.Interfaces;
using Application.Pagination;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class VacationRepository : GenericRepository<Vacation>, IVacationRepository
    {
        public VacationRepository(ApplicationDbContext context) : base(context) { }

        public async Task<PagedList<Vacation>> GetPagedAsync(PagedQueryBase query, bool tracking = false)
        {
            return await GetAll(tracking)
                        .ToPagedListAsync(query);
        }

        public async Task<PagedList<Vacation>> GetPagedByUserIdAsync(Guid userId, PagedQueryBase query, bool tracking = false)
        {
            return await Search(x => x.UserId == userId,
                                tracking)
                        .ToPagedListAsync(query);
        }
    }
}
