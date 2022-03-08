using Application.Pagination;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<PagedList<Booking>> GetPagedAsync(PagedQueryBase query, bool tracking);
    }
}
