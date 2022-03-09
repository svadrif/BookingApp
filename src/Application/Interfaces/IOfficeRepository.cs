using Application.Pagination;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOfficeRepository : IGenericRepository<Office>, IPageable<Office>
    {
    }
}
