using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class VacationService : IVacationService
    {

        private readonly IVacationRepository _vacationRepository;

        public VacationService(IVacationRepository vacationRepository)
        {
            _vacationRepository = vacationRepository;
        }

        public async Task<Vacation> AddAsync(Vacation vacation)
        {
            await _vacationRepository.AddAsync(vacation);
            return vacation;
        }

        public async Task<IEnumerable<Vacation>> GetAllAsync()
        {
            return await _vacationRepository.GetAllAsync();
        }

        public async Task<Vacation> GetByIdAsync(Guid Id)
        {
            return await _vacationRepository.GetByIdAsync(Id);
        }

        public async Task<bool> RemoveAsync(Vacation vacation)
        {
            await _vacationRepository.RemoveAsync(vacation);
            return true;
        }

        public async Task<Vacation> UpdateAsync(Vacation vacation)
        {
            if (await _vacationRepository.GetByIdAsync(vacation.Id) == null)
                return null;

            await _vacationRepository.UpdateAsync(vacation);
            return vacation;
        }

        public async Task<IEnumerable<Vacation>> SearchByUserIdAsync(Guid UserId)
        {
            return await _vacationRepository.SearchByUserIdAsync(c => c.UserId.Contains(UserId));
        }
    }
}
