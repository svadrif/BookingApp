using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Booking>, IPageable<Booking>
    {
    }
}
