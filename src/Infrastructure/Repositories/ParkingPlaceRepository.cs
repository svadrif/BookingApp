using Application.Extentions;
using Application.Interfaces;
using Application.Pagination;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class ParkingPlaceRepository : GenericRepository<ParkingPlace>, IParkingPlaceRepository
    {
        public ParkingPlaceRepository(ApplicationDbContext context) : base(context) { }

        public async Task<PagedList<ParkingPlace>> GetPagedAsync(PagedQueryBase query, bool tracking = false)
        {
            return await GetAll(tracking)
                        .Sort(query.SortOn, query.SortDirection)
                        .ToPagedListAsync(query);
        }

        public async Task<PagedList<ParkingPlace>> GetPagedByOfficeIdAsync(Guid officeId, PagedQueryBase query, bool tracking = false)
        {
            return await Search(x => x.OfficeId == officeId,
                                tracking)
                        .Sort(query.SortOn, query.SortDirection)
                        .ToPagedListAsync(query);
        }
    }
}
