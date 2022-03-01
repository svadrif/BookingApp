using Application.DTOs.AppUserDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;
        public AppUserService(IAppUserRepository appUserRepository, IMapper mapper)
        {
            _appUserRepository = appUserRepository;
            _mapper = mapper;
        }

        public async Task<AppUser> AddAsync(AddAppUserDTO appUserDTO)
        {
            AppUser appUser = _mapper.Map<AppUser>(appUserDTO);
            appUser.Id = Guid.NewGuid();
            await _appUserRepository.AddAsync(appUser);
            return appUser;
        }

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            return await _appUserRepository.GetAllAsync();
        }

        public async Task<AppUser> GetByIdAsync(Guid Id)
        {
            return await _appUserRepository.GetByIdAsync(Id);
        }

        public async Task<bool> RemoveAsync(AppUser appUser)
        {
            if (await _appUserRepository.GetByIdAsync(appUser.Id) == null)
                return false;

            await _appUserRepository.RemoveAsync(appUser);
            return true;
        }

        public async Task<IEnumerable<AppUser>> SearchAppUserAsync(string searchedValue)
        {
            if (string.IsNullOrEmpty(searchedValue))
                return null;

            return await _appUserRepository.SearchAppUserAsync(searchedValue);
        }

        public async Task<AppUser> UpdateAsync(AppUser appUser)
        {
            if (await _appUserRepository.GetByIdAsync(appUser.Id) == null)
                return null;

            await _appUserRepository.UpdateAsync(appUser);
            return appUser;
        }
    }
}
