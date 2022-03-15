using Application.Extentions;
using Application.Interfaces.IRepositories;
using Application.Pagination;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OfficeRepository : GenericRepository<Office>, IOfficeRepository
    {
        public OfficeRepository(ApplicationDbContext context) : base(context) { }

        public async Task<PagedList<Office>> GetPagedAsync(PagedQueryBase query, bool tracking = false)
        {
            return await GetAll(tracking)
                        .Sort(query.SortOn, query.SortDirection)
                        .ToPagedListAsync(query);
        }

        public async Task<Office> GetByAddressAsync(string address, PagedQueryBase query, bool tracking = false)
        {
            return await Search(x => x.Address == address,
                                tracking)
                        .FirstOrDefaultAsync();
        }

        public async Task<PagedList<Office>> GetPagedByCityAsync(string city, PagedQueryBase query, bool tracking = false)
        {
            return await Search(x => x.City == city,
                                tracking)
                        .Sort(query.SortOn, query.SortDirection)
                        .ToPagedListAsync(query);
        }

        public async Task<PagedList<Office>> GetPagedByCountryAsync(string country, PagedQueryBase query, bool tracking = false)
        {
            return await Search(x => x.Country == country,
                                tracking)
                        .Sort(query.SortOn, query.SortDirection)
                        .ToPagedListAsync(query);
        }
    }
}
