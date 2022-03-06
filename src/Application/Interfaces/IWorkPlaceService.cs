using Application.DTOs.WorkPlaceDTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IWorkPlaceService 
    {
        Task<IEnumerable<GetWorkPlaceDTO>> GetAllAsync();
        Task<GetWorkPlaceDTO> GetByIdAsync(Guid Id);
        Task<Guid> AddAsync(AddWorkPlaceDTO workPlaceDTO);
        Task<GetWorkPlaceDTO> UpdateAsync(UpdateWorkPlaceDTO workPlaceDTO);
        Task<bool> RemoveAsync(Guid Id);
        Task<IEnumerable<GetWorkPlaceDTO>> SearchByMapIdAsync(Guid MapId);
    }
}
