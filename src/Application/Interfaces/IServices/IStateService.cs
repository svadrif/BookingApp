using Domain.Entities;

namespace Application.Interfaces.IServices
{
    public interface IStateService
    {
        Task AddAsync(State state);
        Task<State> GetByUserIdAsync(Guid userId, bool tracking = false);
        Task<bool> RemoveByUserIdAsync(Guid userId);
        Task<State> UpdateAsync(State state);
    }
}