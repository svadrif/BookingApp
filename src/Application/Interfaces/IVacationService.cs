using Domain.Entities;

namespace Application.Interfaces
{
    public interface IVacationService : IDisposable
    {
        Task<IEnumerable<Vacation>> GetAll();
        Task<Vacation> GetById(Guid id);
        Task<Vacation> Add(Vacation vacation);
        Task<Vacation> Update(Vacation vacation);
        Task<bool> Remove(Vacation vacation);
        Task<IEnumerable<Vacation>> Search(Guid userId);
    }
}
