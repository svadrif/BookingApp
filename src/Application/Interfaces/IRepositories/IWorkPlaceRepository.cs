using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IWorkPlaceRepository : IGenericRepository<WorkPlace>, IPageable<WorkPlace>
    {
    }
}
