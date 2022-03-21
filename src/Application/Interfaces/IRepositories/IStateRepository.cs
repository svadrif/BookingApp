using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IStateRepository : IGenericRepository<State>
    {
        Task<State> GetByUserIdAsync(Guid userId, bool tracking = false);
    }
}
