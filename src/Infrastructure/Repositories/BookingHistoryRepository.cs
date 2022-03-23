using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Infrastructure.Repositories
{
    public class BookingHistoryRepository : GenericRepository<BookingHistory>, IBookingHistoryRepository
    {
        public BookingHistoryRepository(
            ApplicationDbContext context,
            ILoggerManager logger
            ) : base(context, logger) { }

        public async Task<BookingHistory> GetByUserIdAsync(Guid userId, bool tracking = false)
        {
            try
            {
                return await Search(x => x.UserId == userId,
                                    tracking)
                            .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetByUserIdAsync)} action {ex}");
                return new BookingHistory();
            }
        }
    }
}
