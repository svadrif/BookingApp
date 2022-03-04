using Application.DTOs.OfficeDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IMapper _mapper;
        public OfficeService(IOfficeRepository officeRepository, IMapper mapper)
        {
            _officeRepository = officeRepository;
            _mapper = mapper;
        }

        public async Task<Office> AddAsync(AddOfficeDTO officeDTO)
        {
            Office office = _mapper.Map<Office>(officeDTO);
            office.Id = Guid.NewGuid();
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
            if (await _officeRepository.GetByIdAsync(office.Id) == null)
                return false;

            await _officeRepository.RemoveAsync(office);
            return true;
        }

        public async Task<IEnumerable<Office>> SearchOfficeAsync(string searchedValue)
        {
            if (string.IsNullOrEmpty(searchedValue))
                return null;

            return await _officeRepository.SearchOfficeAsync(searchedValue);
        }

        public async Task<Office> UpdateAsync(Office office)
        {
            if (await _officeRepository.GetByIdAsync(office.Id) == null)
                return null;

            await _officeRepository.UpdateAsync(office);
            return office;
        }
    }
}
