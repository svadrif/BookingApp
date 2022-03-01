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
        public async Task<Office> Add(Office office)
        {
            if (_officeRepository.Search(o => o.Id == office.Id).Result.Any())
                return null;

            await _officeRepository.Add(office);
            return office;
        }

        public async Task<IEnumerable<Office>> GetAll()
        {
            return await _officeRepository.GetAll();
        }

        public async Task<Office> GetById(Guid Id)
        {
            return await _officeRepository.GetById(Id);
        }

        public async Task<bool> Remove(Office office)
        {
            await _officeRepository.Remove(office);
            return true;
        }

        public async Task<IEnumerable<Office>> Search(Guid Id)
        {
            return await _officeRepository.Search(c => c.Id.Contains(Id));
        }

        public async Task<IEnumerable<Office>> SearchOffice(string searchedValue)
        {
            return await _officeRepository.SearchOffice(searchedValue);
        }

        public async Task<Office> Update(Office office)
        {
            if (_officeRepository.Search(o => o.Name == office.Name && o.Id != office.Id).Result.Any())
                return null;

            await _officeRepository.Update(office);
            return office;
        }
    }
}
