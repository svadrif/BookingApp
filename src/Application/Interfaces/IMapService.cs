using Application.DTOs.MapDTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMapService
    {
        Task<IEnumerable<GetMapDTO>> GetAllAsync();
        Task<GetMapDTO> GetByIdAsync(Guid Id);
        Task<Guid> AddAsync(AddMapDTO mapDTO);
        Task<GetMapDTO> UpdateAsync(UpdateMapDTO mapDTO);
        Task<bool> RemoveAsync(Guid Id);
        Task<IEnumerable<GetMapDTO>> SearchByOfficeIdAsync(Guid OfficeId);
    }
}
