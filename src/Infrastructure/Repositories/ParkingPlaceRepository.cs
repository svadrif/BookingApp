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

        public async Task<PagedList<ParkingPlace>> GetPagedAsync(PagedQueryBase query, bool tracking)
        {
            return await GetAll(tracking).ToPagedListAsync(query);
        }
    }
}
