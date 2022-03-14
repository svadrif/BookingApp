using Application.Pagination;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMapRepository : IGenericRepository<Map>, IPageable<Map>
    {
        Task<PagedList<Map>> GetPagedByOfficeIdAsync(Guid officeId, PagedQueryBase query, bool tracking = false);
        Task<PagedList<Map>> GetPagedByAttributesAsync(bool hasKitchen, bool hasConfRoom, PagedQueryBase query, bool tracking = false);
    }
}
