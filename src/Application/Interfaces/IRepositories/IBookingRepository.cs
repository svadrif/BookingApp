using Application.Pagination;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IBookingRepository : IGenericRepository<Booking>, IPageable<Booking>
    {
        Task<PagedList<Booking>> GetPagedByUserIdAsync(Guid userId, PagedQueryBase query, bool tracking = false);
        Task<PagedList<Booking>> GetPagedByWorkPlaceIdAsync(Guid WorkPlaceId, PagedQueryBase query, bool tracking = false);
        Task<PagedList<Booking>> GetPagedByBookingDateAsync(DateTimeOffset bookingDate, PagedQueryBase query, bool tracking = false);
        Task<PagedList<Booking>> GetPagedByBookingDateAsync(DateTimeOffset bookingStart, DateTimeOffset bookingEnd, PagedQueryBase query, bool tracking = false);
    }
}
