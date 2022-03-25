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
        Task<PagedList<GetWorkPlaceDTO>> SearchByMapIdAsync(Guid mapId, PagedQueryBase query);
        Task<PagedList<GetWorkPlaceDTO>> GetByAttributesAsync(bool isNextToWindow, bool hasPc, bool hasMonitor, bool hasKeyboard, bool hasMouse, bool hasHeadset, PagedQueryBase query);
    }
}
