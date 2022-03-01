using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMapService
    {
        Task<IEnumerable<Map>> GetAll();
        Task<Map> GetById(Guid Id);
        Task<Map> Add(Map map);
        Task<Map> Update(Map map);
        Task<bool> Remove(Map map);
        Task<IEnumerable<Map>> Search(Guid OfficeId);
    }
}
