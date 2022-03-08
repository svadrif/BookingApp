using Application.Pagination;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IParkingPlaceRepository : IGenericRepository<ParkingPlace>
    {
        Task<PagedList<ParkingPlace>> GetPagedAsync(PagedQueryBase query, bool tracking);
    }
}
