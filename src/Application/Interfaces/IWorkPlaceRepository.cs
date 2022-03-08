using Application.Pagination;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IWorkPlaceRepository : IGenericRepository<WorkPlace>
    {
        Task<PagedList<WorkPlace>> GetPagedAsync(PagedQueryBase query, bool tracking);
    }
}
