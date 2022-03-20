using Domain.Entities;

namespace Application.Interfaces.IServices
{
    public interface IBookingHistoryService
    {
        Task AddAsync(BookingHistory bookingHistory);
        Task<BookingHistory> GetByUserIdAsync(Guid userId, bool tracking = false);
        Task<bool> RemoveByUserIdAsync(Guid userId);
        Task<BookingHistory> UpdateAsync(BookingHistory bookingHistory);
    }
}
