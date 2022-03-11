using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IOfficeRepository : IGenericRepository<Office>, IPageable<Office>
    {
    }
}
