using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOfficeService : IDisposable
    {
        Task<IEnumerable<Office>> GetAll();
        Task<Office> GetById(Guid Id);
        Task<Office> Add(Office office);
        Task<Office> Update(Office office);
        Task<bool> Remove(Office office);
        Task<IEnumerable<Office>> Search(Guid Id);
        Task<IEnumerable<Office>> SearchOffice(string searchedValue);
    }
}
