using Application.DTOs.AppUserDTO;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Pagination;
using AutoMapper;
using Domain.Entities;
using Application.Interfaces;

namespace Infrastructure.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStateService _stateService;
        private readonly IBookingHistoryService _historyService;
        private readonly ILoggerManager _logger;
        public AppUserService(IUnitOfWork unitOfWork, IMapper mapper, IStateService stateService, IBookingHistoryService historyService, ILoggerManager logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _stateService = stateService;
            _historyService = historyService;
            _logger = logger;
        }

        public async Task<Guid> AddAsync(AddAppUserDTO appUserDTO)
        {
            try
            {
                AppUser appUser = _mapper.Map<AppUser>(appUserDTO);
                await _unitOfWork.AppUsers.AddAsync(appUser);
                await _unitOfWork.CompleteAsync();

                await _stateService.AddAsync(new State() { UserId = appUser.Id });
                await _historyService.AddAsync(new BookingHistory() { UserId = appUser.Id });

                return appUser.Id;
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(AddAsync)} action {ex}");
                return Guid.Empty;
            }
        }

        public async Task<PagedList<GetAppUserDTO>> GetPagedAsync(PagedQueryBase query)
        {
            try 
            { 
            var appUsers = await _unitOfWork.AppUsers.GetPagedAsync(query);
            var mapUsers = _mapper.Map<List<GetAppUserDTO>>(appUsers);
            var appUsersDTO = new PagedList<GetAppUserDTO>(mapUsers, appUsers.TotalCount, appUsers.CurrentPage, appUsers.PageSize);
            return appUsersDTO;
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(AddAsync)} action {ex}");
                return null;
            }
        }

        public async Task<GetAppUserDTO> GetByIdAsync(Guid Id)
        {
            try
            {
                var appUsers = await _unitOfWork.AppUsers.GetByIdAsync(Id);
                return _mapper.Map<GetAppUserDTO>(appUsers);
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(GetByIdAsync)} action {ex}");
                return null;
            }
        }

        public async Task<GetAppUserDTO> GetByTelegramIdAsync(long telegramId)
        {
            try
            {
                var appUsers = await _unitOfWork.AppUsers.GetByTelegramIdAsync(telegramId);
                return _mapper.Map<GetAppUserDTO>(appUsers);
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(GetByTelegramIdAsync)} action {ex}");
                return null;
            }
        }

        public async Task<bool> RemoveAsync(Guid Id)
        {
            try
            {
                var appUser = await _unitOfWork.AppUsers.GetByIdAsync(Id);
                if (appUser == null)
                    return false;

                await _stateService.RemoveByUserIdAsync(Id);
                await _historyService.RemoveByUserIdAsync(Id);

                _unitOfWork.AppUsers.Remove(appUser);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(RemoveAsync)} action {ex}");
                return false;
            }
        }

        public async Task<GetAppUserDTO> UpdateAsync(UpdateAppUserDTO appUserDTO)
        {
            try
            {
                var appUser = await _unitOfWork.AppUsers.GetByIdAsync(appUserDTO.Id);
                if (appUser == null)
                    return null;

                _mapper.Map(appUserDTO, appUser);
                _unitOfWork.AppUsers.Update(appUser);
                await _unitOfWork.CompleteAsync();
                return _mapper.Map<GetAppUserDTO>(appUser);
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(UpdateAsync)} action {ex}");
                return null;
            }
        }
    }
}
