using Application.DTOs.MapDTO;
using Application.Pagination;

namespace Application.Interfaces.IServices
{
    public interface IMapService
    {
        Task<PagedList<GetMapDTO>> GetPagedAsync(PagedQueryBase query);
        Task<GetMapDTO> GetByIdAsync(Guid Id);
        Task<Guid> AddAsync(AddMapDTO mapDTO);
        Task<GetMapDTO> UpdateAsync(UpdateMapDTO mapDTO);
        Task<bool> RemoveAsync(Guid Id);
        Task<IEnumerable<GetMapDTO>> SearchByOfficeIdAsync(Guid OfficeId);
    }
}
