using Application.Extentions;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Pagination;
using Domain.Entities;
using Infrastructure.Context;
using Serilog;

namespace Infrastructure.Repositories
{
    public class ParkingPlaceRepository : GenericRepository<ParkingPlace>, IParkingPlaceRepository
    {
        public ParkingPlaceRepository(ApplicationDbContext context, ILoggerManager logger) : base(context, logger) { }

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
