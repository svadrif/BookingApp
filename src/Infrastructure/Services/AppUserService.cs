using Application.Interfaces;
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
        public AppUserService(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public async Task<AppUser> AddAsync(AppUser appUser)
        {
            if (_appUserRepository.SearchAsync(a => a.Id == appUser.Id).Result.Any())
                return null;

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
            await _appUserRepository.RemoveAsync(appUser);
            return true;
        }

        public async Task<IEnumerable<AppUser>> SearchAsync(Guid Id)
        {
            return await _appUserRepository.SearchAsync(c => c.Id.Contains(Id));
        }

        public async Task<IEnumerable<AppUser>> SearchAppUserAsync(string searchedValue)
        {
            return await _appUserRepository.SearchAppUserAsync(searchedValue);
        }

        public async Task<AppUser> UpdateAsync(AppUser appUser)
        {
            if (_appUserRepository.SearchAsync(a => a.TelegramId == appUser.TelegramId && a.Id != appUser.Id).Result.Any())
                return null;

            await _appUserRepository.Update(appUser);
            return appUser;
        }
    }
}
