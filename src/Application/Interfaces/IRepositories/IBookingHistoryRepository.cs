using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IBookingHistoryRepository : IGenericRepository<BookingHistory>
    {
        Task<BookingHistory> GetByUserIdAsync(Guid userId, bool tracking = false);
    }
}