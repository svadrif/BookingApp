using Application.Extentions;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Pagination;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Infrastructure.Repositories
{
    public class OfficeRepository : GenericRepository<Office>, IOfficeRepository
    {
        public OfficeRepository(ApplicationDbContext context, ILoggerManager logger) : base(context, logger) { }

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

        public async Task<PagedList<string>> GetPagedCountriesAsync(PagedQueryBase query, bool tracking = false)
        {
            return await GetAll(tracking)
                        .Sort(query.SortOn, query.SortDirection)
                        .Select(x => x.Country)
                        .Distinct()
                        .ToPagedListAsync(query);
        }

        public async Task<PagedList<string>> GetPagedCitiesByCountryAsync(string country, PagedQueryBase query, bool tracking = false)
        {
            return await Search(x => x.Country == country,
                                tracking)
                        .Sort(query.SortOn, query.SortDirection)
                        .Select(x => x.City)
                        .Distinct()
                        .ToPagedListAsync(query);
        }
    }
}
