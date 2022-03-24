using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.Entities;
using Application.Interfaces;

namespace Infrastructure.Services
{
    public class BookingHistoryService : IBookingHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        public BookingHistoryService(IUnitOfWork unitOfWork, ILoggerManager logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task AddAsync(BookingHistory bookingHistory)
        {
            try
            {
                await _unitOfWork.BookingHistories.AddAsync(bookingHistory);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(AddAsync)} action {ex}");                
            }
        }

        public async Task<BookingHistory> GetByUserIdAsync(Guid userId, bool tracking = false)
        {
            try
            {
                var bookingHistory = await _unitOfWork.BookingHistories.GetByUserIdAsync(userId, tracking);
                return bookingHistory;
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(GetByUserIdAsync)} action {ex}");
                return null;
            }
        }

        public async Task<BookingHistory> UpdateAsync(BookingHistory bookingHistory)
        {
            try
            {
                _unitOfWork.BookingHistories.Update(bookingHistory);
                await _unitOfWork.CompleteAsync();
                return bookingHistory;
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(UpdateAsync)} action {ex}");
                return null;
            }
        }

        public async Task<bool> RemoveByUserIdAsync(Guid userId)
        {
            try
            {
                var bookingHistory = await _unitOfWork.BookingHistories.GetByUserIdAsync(userId);
                _unitOfWork.BookingHistories.Remove(bookingHistory);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(RemoveByUserIdAsync)} action {ex}");
                return false;
            }
        }
    }
}
