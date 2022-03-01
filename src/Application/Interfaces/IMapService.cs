using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMapService
    {
        Task<IEnumerable<Map>> GetAllAsync();
        Task<Map> GetByIdAsync(Guid Id);
        Task<Map> AddAsync(Map map);
        Task<Map> UpdateAsync(Map map);
        Task<bool> RemoveAsync(Map map);
        Task<IEnumerable<Map>> SearchAsync(Guid OfficeId);
    }
}
