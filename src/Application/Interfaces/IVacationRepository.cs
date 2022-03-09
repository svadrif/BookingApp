using Domain.Entities;

namespace Application.Interfaces
{
    public interface IVacationRepository : IGenericRepository<Vacation>, IPageable<Vacation>
    {
    }
}
