using Application.Pagination;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMapRepository : IGenericRepository<Map>
    {
        Task<PagedList<Map>> GetPagedAsync(PagedQueryBase query, bool tracking);
    }
}
