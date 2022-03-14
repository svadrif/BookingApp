using Application.Pagination;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IParkingPlaceRepository : IGenericRepository<ParkingPlace>, IPageable<ParkingPlace>
    {
        Task<PagedList<ParkingPlace>> GetPagedByOfficeIdAsync(Guid officeId, PagedQueryBase query, bool tracking = false);
    }
}
