using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IBookingRepository : IGenericRepository<Booking>, IPageable<Booking>
    {
    }
}
