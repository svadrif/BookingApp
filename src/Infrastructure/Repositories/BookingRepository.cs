using Application.Extentions;
using Application.Interfaces;
using Application.Pagination;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context) { }

        public async Task<PagedList<Booking>> GetPagedAsync(PagedQueryBase query, bool tracking)
        {
            return await GetAll(tracking).ToPagedListAsync(query);
        }
    }
}
