using Application.DTOs.OfficeDTO;
using Application.Pagination;

namespace Application.Interfaces.IServices
{
    public interface IOfficeService
    {
        Task<PagedList<GetOfficeDTO>> GetPagedAsync(PagedQueryBase query);
        Task<GetOfficeDTO> GetByIdAsync(Guid Id);
        Task<Guid> AddAsync(AddOfficeDTO officeDTO);
        Task<GetOfficeDTO> UpdateAsync(UpdateOfficeDTO officeDTO);
        Task<bool> RemoveAsync(Guid Id);
        Task<PagedList<string>> GetCountriesAsync(PagedQueryBase query);
        Task<PagedList<string>> GetCitiesAsync(string country, PagedQueryBase query);
    }
}
