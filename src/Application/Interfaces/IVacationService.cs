using Domain.Entities;

namespace Application.Interfaces
{
    public interface IVacationService : IDisposable
    {
        Task<IEnumerable<Vacation>> GetAll();
        Task<Vacation> GetById(Guid Id);
        Task<Vacation> Add(Vacation vacation);
        Task<Vacation> Update(Vacation vacation);
        Task<bool> Remove(Vacation vacation);
        Task<IEnumerable<Vacation>> Search(Guid UserId);
    }
}
