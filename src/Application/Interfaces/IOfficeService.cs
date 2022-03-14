using Application.DTOs.OfficeDTO;

namespace Application.Interfaces
{
    public interface IOfficeService
    {
        Task<IEnumerable<GetOfficeDTO>> GetAllAsync();
        Task<GetOfficeDTO> GetByIdAsync(Guid Id);
        Task<Guid> AddAsync(AddOfficeDTO officeDTO);
        Task<GetOfficeDTO> UpdateAsync(UpdateOfficeDTO officeDTO);
        Task<bool> RemoveAsync(Guid Id);
    }
}
