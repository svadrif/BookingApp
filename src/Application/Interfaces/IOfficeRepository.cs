using Application.Pagination;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOfficeRepository : IGenericRepository<Office>
    {
        Task<PagedList<Office>> GetPagedAsync(PagedQueryBase query, bool tracking);
    }
}
