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

        public async Task<Vacation> Add(Vacation vacation)
        {
            if (_vacationRepository.Search(v => v.Id == vacation.Id).Result.Any())
                return null;

            await _vacationRepository.Add(vacation);
            return vacation;
        }

        public void Dispose()
        {
            _vacationRepository?.Dispose();
        }

        public async Task<IEnumerable<Vacation>> GetAll()
        {
            return await _vacationRepository.GetAll();
        }

        public async Task<Vacation> GetById(Guid Id)
        {
            return await _vacationRepository.GetById(Id);
        }

        public async Task<bool> Remove(Vacation vacation)
        {
            var appUsers = await _appUserService.Search(vacation.UserId);
            if (appUsers.Any()) return false;

            await _vacationRepository.Remove(vacation);
            return true;
        }

        public async Task<Vacation> Update(Vacation vacation)
        {
            if (_vacationRepository.Search(v => v.UserId == vacation.UserId && v.Id != vacation.Id).Result.Any())
                return null;

            await _vacationRepository.Update(vacation);
            return vacation;
        }

        public async Task<IEnumerable<Vacation>> Search(Guid UserId)
        {
            return await _vacationRepository.Search(c => c.UserId.Contains(UserId));
        }
    }
}
