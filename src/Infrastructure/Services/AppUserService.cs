﻿using Application.DTOs.AppUserDTO;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Pagination;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStateService _stateService;
        public AppUserService(IUnitOfWork unitOfWork, IMapper mapper, IStateService stateService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _stateService = stateService;
        }

        public async Task<Guid> AddAsync(AddAppUserDTO appUserDTO)
        {
            AppUser appUser = _mapper.Map<AppUser>(appUserDTO);
            await _unitOfWork.AppUsers.AddAsync(appUser);
            await _unitOfWork.CompleteAsync();

            await _stateService.AddAsync(new State() { UserId = appUser.Id});
            return appUser.Id;
        }

        public async Task<PagedList<GetAppUserDTO>> GetPagedAsync(PagedQueryBase query)
        {
            var appUsers = await _unitOfWork.AppUsers.GetPagedAsync(query);
            var mapUsers = _mapper.Map<List<GetAppUserDTO>>(appUsers);
            var appUsersDTO = new PagedList<GetAppUserDTO>(mapUsers, appUsers.TotalCount, appUsers.CurrentPage, appUsers.PageSize);
            return appUsersDTO;
        }

        public async Task<GetAppUserDTO> GetByIdAsync(Guid Id)
        {
            var appUsers = await _unitOfWork.AppUsers.GetByIdAsync(Id);
            return _mapper.Map<GetAppUserDTO>(appUsers);
        }

        public async Task<GetAppUserDTO> GetByTelegramIdAsync(long telegramId)
        {
            var appUsers = await _unitOfWork.AppUsers.GetByTelegramIdAsync(telegramId);
            return _mapper.Map<GetAppUserDTO>(appUsers);
        }

        public async Task<bool> RemoveAsync(Guid Id)
        {
            var appUser = await _unitOfWork.AppUsers.GetByIdAsync(Id);
            if (appUser == null)
                return false;

            await _stateService.RemoveByUserIdAsync(Id);

            _unitOfWork.AppUsers.Remove(appUser);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<GetAppUserDTO> UpdateAsync(UpdateAppUserDTO appUserDTO)
        {
            var appUser = await _unitOfWork.AppUsers.GetByIdAsync(appUserDTO.Id);
            if (appUser == null)
                return null;

            _mapper.Map(appUserDTO, appUser);
            _unitOfWork.AppUsers.Update(appUser);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<GetAppUserDTO>(appUser);
        }

        public async Task<State> GetStateByTelegramIdAsync(long telegramId)
        {
            var user = await _unitOfWork.AppUsers.GetByTelegramIdAsync(telegramId);
            var state = await _stateService.GetByUserIdAsync(user.Id);
            return state;
        }
    }
}
