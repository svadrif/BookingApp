using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly IOfficeRepository _officeRepository;

        public OfficeService(IOfficeRepository officeRepository)
        {
            _officeRepository = officeRepository;
        }
        public async Task<Office> AddAsync(Office office)
        {
            if (_officeRepository.SearchAsync(o => o.Id == office.Id).Result.Any())
                return null;

            await _officeRepository.AddAsync(office);
            return office;
        }

        public async Task<IEnumerable<Office>> GetAllAsync()
        {
            return await _officeRepository.GetAllAsync();
        }

        public async Task<Office> GetByIdAsync(Guid Id)
        {
            return await _officeRepository.GetByIdAsync(Id);
        }

        public async Task<bool> RemoveAsync(Office office)
        {
            await _officeRepository.RemoveAsync(office);
            return true;
        }

        public async Task<IEnumerable<Office>> SearchAsync(Guid Id)
        {
            return await _officeRepository.SearchAsync(c => c.Id.Contains(Id));
        }

        public async Task<IEnumerable<Office>> SearchOfficeAsync(string searchedValue)
        {
            return await _officeRepository.SearchOfficeAsync(searchedValue);
        }

        public async Task<Office> UpdateAsync(Office office)
        {
            if (_officeRepository.SearchAsync(o => o.Name == office.Name && o.Id != office.Id).Result.Any())
                return null;

            await _officeRepository.UpdateAsync(office);
            return office;
        }
    }
}
