using Application.DTOs.WorkPlaceDTO;
using Application.Pagination;

namespace Application.Interfaces.IServices
{
    public interface IWorkPlaceService
    {
        Task<PagedList<GetWorkPlaceDTO>> GetPagedAsync(PagedQueryBase query);
        Task<GetWorkPlaceDTO> GetByIdAsync(Guid Id);
        Task<Guid> AddAsync(AddWorkPlaceDTO workPlaceDTO);
        Task<GetWorkPlaceDTO> UpdateAsync(UpdateWorkPlaceDTO workPlaceDTO);
        Task<bool> RemoveAsync(Guid Id);
        Task<IEnumerable<GetWorkPlaceDTO>> SearchByMapIdAsync(Guid MapId);
    }
}
