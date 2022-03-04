using Application.DTOs.OfficeDTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOfficeService 
    {
        Task<IEnumerable<Office>> GetAllAsync();
        Task<Office> GetByIdAsync(Guid Id);
        Task<Office> AddAsync(AddOfficeDTO officeDTO);
        Task<Office> UpdateAsync(Office office);
        Task<bool> RemoveAsync(Office office);
        Task<IEnumerable<Office>> SearchOfficeAsync(string searchedValue);
    }
}
