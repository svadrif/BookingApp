using Application.DTOs.MapDTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMapService
    {
        Task<IEnumerable<Map>> GetAllAsync();
        Task<Map> GetByIdAsync(Guid Id);
        Task<Map> AddAsync(AddMapDTO mapDTO);
        Task<Map> UpdateAsync(Map map);
        Task<bool> RemoveAsync(Map map);
        Task<IEnumerable<Map>> SearchByOfficeIdAsync(Guid OfficeId);
    }
}
