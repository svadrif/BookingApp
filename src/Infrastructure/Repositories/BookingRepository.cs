using Application.Extentions;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Pagination;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context, ILoggerManager logger) : base(context, logger) { }

        public async Task<PagedList<Booking>> GetPagedAsync(PagedQueryBase query, bool tracking = false)
        {
            try
            {
                return await GetAll(tracking)
                            .Sort(query.SortOn, query.SortDirection)
                            .ToPagedListAsync(query);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetPagedAsync)} action {ex}");
                return new PagedList<Booking>(new List<Booking>(), 0, 0, 0);
            }
        }
        public async Task<PagedList<Booking>> GetPagedByUserIdAsync(Guid userId, PagedQueryBase query, bool tracking = false)
        {
            try
            {
                return await Search(x => x.UserId == userId,
                                    tracking)
                            .Sort(query.SortOn, query.SortDirection)
                            .ToPagedListAsync(query);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetPagedByUserIdAsync)} action {ex}");
                return new PagedList<Booking>(new List<Booking>(), 0, 0, 0);
            }
        }
        public async Task<PagedList<Booking>> GetPagedByWorkPlaceIdAsync(Guid WorkPlaceId, PagedQueryBase query, bool tracking = false)
        {
            try
            {
                return await Search(x => x.WorkPlaceId == WorkPlaceId,
                                    tracking)
                            .Sort(query.SortOn, query.SortDirection)
                            .ToPagedListAsync(query);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetPagedByWorkPlaceIdAsync)} action {ex}");
                return new PagedList<Booking>(new List<Booking>(), 0, 0, 0);
            }
        }

        public async Task<PagedList<Booking>> GetPagedByBookingDateAsync(DateTimeOffset bookingDate, PagedQueryBase query, bool tracking = false)
        {
            try
            {
                return await Search(x => x.BookingStart > bookingDate
                                    || x.BookingEnd < bookingDate,
                                    tracking)
                            .Sort(query.SortOn, query.SortDirection)
                            .ToPagedListAsync(query);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetPagedByBookingDateAsync)} action {ex}");
                return new PagedList<Booking>(new List<Booking>(), 0, 0, 0);
            }
        }

        public async Task<PagedList<Booking>> GetPagedByBookingDateAsync(DateTimeOffset bookingStart, DateTimeOffset bookingEnd, PagedQueryBase query, bool tracking = false)
        {
            try
            {
                return await Search(x => x.BookingStart > bookingEnd
                                    || x.BookingEnd < bookingStart,
                                    tracking)
                            .Sort(query.SortOn, query.SortDirection)
                            .ToPagedListAsync(query);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetPagedByBookingDateAsync)} action {ex}");
                return new PagedList<Booking>(new List<Booking>(), 0, 0, 0);
            }
        }
    }
}
