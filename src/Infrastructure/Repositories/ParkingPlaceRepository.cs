using Application.Extentions;
using Application.Interfaces.IRepositories;
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
                        .ToPagedListAsync(query);
        }

        public async Task<PagedList<ParkingPlace>> GetPagedByOfficeIdAsync(Guid officeId, PagedQueryBase query, bool tracking = false)
        {
            return await Search(x => x.OfficeId == officeId,
                                tracking)
                        .ToPagedListAsync(query);
        }
    }
}
