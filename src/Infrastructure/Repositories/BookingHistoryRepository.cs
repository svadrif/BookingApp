using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookingHistoryRepository : GenericRepository<BookingHistory>, IBookingHistoryRepository
    {
        private readonly ILoggerManager _logger;
        public BookingHistoryRepository(ApplicationDbContext context, ILoggerManager logger) : base(context) { }

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
                _logger.LogError($"Something went wrong in the {nameof(GetByUserIdAsync)}action {ex}");
                return null;
            }
        }
    }
}
