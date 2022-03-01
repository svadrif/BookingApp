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

        public async Task<AppUser> Add(AppUser appUser)
        {
            if (_appUserRepository.Search(a => a.Id == appUser.Id).Result.Any())
                return null;

            await _appUserRepository.Add(appUser);
            return appUser;
        }

        public async Task<IEnumerable<AppUser>> GetAll()
        {
            return await _appUserRepository.GetAll();
        }

        public async Task<AppUser> GetById(Guid Id)
        {
            return await _appUserRepository.GetById(Id);
        }

        public async Task<bool> Remove(AppUser appUser)
        {
            await _appUserRepository.Remove(appUser);
            return true;
        }

        public async Task<IEnumerable<AppUser>> Search(Guid Id)
        {
            return await _appUserRepository.Search(c => c.Id.Contains(Id));
        }

        public async Task<IEnumerable<AppUser>> SearchAppUser(string searchedValue)
        {
            return await _appUserRepository.SearchAppUser(searchedValue);
        }

        public async Task<AppUser> Update(AppUser appUser)
        {
            if (_appUserRepository.Search(a => a.TelegramId == appUser.TelegramId && a.Id != appUser.Id).Result.Any())
                return null;

            await _appUserRepository.Update(appUser);
            return appUser;
        }
    }
}
