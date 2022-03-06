using Application.DTOs.AppUserDTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAppUserService
    {
        Task<IEnumerable<GetAppUserDTO>> GetAllAsync();
        Task<GetAppUserDTO> GetByIdAsync(Guid Id);
        Task<Guid> AddAsync(AddAppUserDTO appUserDTO);
        Task<GetAppUserDTO> UpdateAsync(UpdateAppUserDTO appUserDTO);
        Task<bool> RemoveAsync(Guid Id);       
    }
}
