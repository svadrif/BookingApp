using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class BookingHistoryService : IBookingHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingHistoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(BookingHistory bookingHistory)
        {
            await _unitOfWork.BookingHistories.AddAsync(bookingHistory);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<BookingHistory> GetByUserIdAsync(Guid userId, bool tracking = false)
        {
            var bookingHistory = await _unitOfWork.BookingHistories.GetByUserIdAsync(userId, tracking);
            return bookingHistory;
        }

        public async Task<BookingHistory> UpdateAsync(BookingHistory bookingHistory)
        {
            _unitOfWork.BookingHistories.Update(bookingHistory);
            await _unitOfWork.CompleteAsync();
            return bookingHistory;
        }

        public async Task<bool> RemoveByUserIdAsync(Guid userId)
        {
            var bookingHistory = await _unitOfWork.BookingHistories.GetByUserIdAsync(userId);
            _unitOfWork.BookingHistories.Remove(bookingHistory);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
