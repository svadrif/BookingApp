using Application.Pagination;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IVacationRepository : IGenericRepository<Vacation>, IPageable<Vacation>
    {
        Task<PagedList<Vacation>> GetPagedByUserIdAsync(Guid userId, PagedQueryBase query, bool tracking = false);
    }
}
