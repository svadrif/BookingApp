using Application.DTOs.AppUserDTO;
using Application.Pagination;

namespace Application.Interfaces.IServices
{
    public interface IAppUserService
    {
        Task<PagedList<GetAppUserDTO>> GetPagedAsync(PagedQueryBase query);
        Task<GetAppUserDTO> GetByIdAsync(Guid Id);
        Task<Guid> AddAsync(AddAppUserDTO appUserDTO);
        Task<GetAppUserDTO> UpdateAsync(UpdateAppUserDTO appUserDTO);
        Task<bool> RemoveAsync(Guid Id);
    }
}
