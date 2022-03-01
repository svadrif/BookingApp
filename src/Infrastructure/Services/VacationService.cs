using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class VacationService : IVacationService
    {

        private readonly IVacationRepository _vacationRepository;
        private readonly IAppUserService _appUserService;

        public VacationService(IVacationRepository vacationRepository, IAppUserService appUserService)
        {
            _vacationRepository = vacationRepository;
            _appUserService = appUserService;
        }

        public async Task<Vacation> AddAsync(Vacation vacation)
        {
            if (_vacationRepository.Search(v => v.Id == vacation.Id).Result.Any())
                return null;

            await _vacationRepository.Add(vacation);
            return vacation;
        }

        public async Task<IEnumerable<Vacation>> GetAllAsync()
        {
            return await _vacationRepository.GetAll();
        }

        public async Task<Vacation> GetByIdAsync(Guid Id)
        {
            return await _vacationRepository.GetById(Id);
        }

        public async Task<bool> RemoveAsync(Vacation vacation)
        {
            var appUsers = await _appUserService.Search(vacation.UserId);
            if (appUsers.Any()) return false;

            await _vacationRepository.Remove(vacation);
            return true;
        }

        public async Task<Vacation> UpdateAsync(Vacation vacation)
        {
            if (_vacationRepository.Search(v => v.UserId == vacation.UserId && v.Id != vacation.Id).Result.Any())
                return null;

            await _vacationRepository.Update(vacation);
            return vacation;
        }

        public async Task<IEnumerable<Vacation>> SearchAsync(Guid UserId)
        {
            return await _vacationRepository.Search(c => c.UserId.Contains(UserId));
        }
    }
}
