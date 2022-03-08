using Application.Pagination;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IVacationRepository : IGenericRepository<Vacation>
    {
        Task<PagedList<Vacation>> GetPagedAsync(PagedQueryBase query, bool tracking);
    }
}
