using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IVacationRepository : IGenericRepository<Vacation>, IPageable<Vacation>
    {
    }
}
