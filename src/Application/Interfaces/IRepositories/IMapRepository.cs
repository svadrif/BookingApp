using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IMapRepository : IGenericRepository<Map>, IPageable<Map>
    {
    }
}
