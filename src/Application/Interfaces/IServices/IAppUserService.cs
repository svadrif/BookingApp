using Application.DTOs.AppUserDTO;
using Application.Pagination;
using Domain.Entities;

namespace Application.Interfaces.IServices
{
    public interface IAppUserService
    {
        Task<PagedList<GetAppUserDTO>> GetPagedAsync(PagedQueryBase query);
        Task<GetAppUserDTO> GetByIdAsync(Guid Id);
        Task<GetAppUserDTO> GetByTelegramIdAsync(long telegramId);
        Task<Guid> AddAsync(AddAppUserDTO appUserDTO);
        Task<GetAppUserDTO> UpdateAsync(UpdateAppUserDTO appUserDTO);
        Task<bool> RemoveAsync(Guid Id);
        Task<State> GetStateByTelegramIdAsync(long telegramId);
    }
}
