using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMapRepository : IGenericRepository<Map>, IPageable<Map>
    {
    }
}
