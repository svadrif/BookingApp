using Domain.Entities;

namespace Application.Interfaces
{
    public interface IWorkPlaceRepository : IGenericRepository<WorkPlace>, IPageable<WorkPlace>
    {
    }
}
