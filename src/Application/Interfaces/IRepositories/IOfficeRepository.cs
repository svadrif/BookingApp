using Application.Pagination;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IOfficeRepository : IGenericRepository<Office>, IPageable<Office>
    {
        Task<PagedList<Office>> GetPagedByCountryAsync(string country, PagedQueryBase query, bool tracking = false);
        Task<PagedList<Office>> GetPagedByCityAsync(string city, PagedQueryBase query, bool tracking = false);
        Task<Office> GetByAddressAsync(string address, PagedQueryBase query, bool tracking = false);
    }
}
