using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class MapService : IMapService
    {
        private readonly IMapRepository _mapRepository;
        private readonly IOfficeService _officeService;
        public MapService(IMapRepository mapRepository, IOfficeService officeService)
        {
            _mapRepository = mapRepository;
            _officeService = officeService;
        }
        public async Task<Map> AddAsync(Map map)
        {
            if (_mapRepository.SearchAsync(m => m.Id == map.Id).Result.Any())
                return null;

            await _mapRepository.AddAsync(map);
            return map;
        }

        public async Task<IEnumerable<Map>> GetAllAsync()
        {
            return await _mapRepository.GetAllAsync();
        }

        public async Task<Map> GetByIdAsync(Guid Id)
        {
            return await _mapRepository.GetByIdAsync(Id);
        }

        public async Task<bool> RemoveAsync(Map map)
        {
            var offices = await _officeService.SearchAsync(map.OfficeId);
            if (offices.Any()) return false;

            await _mapRepository.RemoveAsync(map);
            return true;
        }

        public async Task<IEnumerable<Map>> SearchAsync(Guid OfficeId)
        {
            return await _mapRepository.SearchAsync(c => c.OfficeId.Contains(OfficeId));
        }

        public async Task<Map> UpdateAsync(Map map)
        {
            if (_mapRepository.Search(m => m.OfficeId == map.OfficeId && m.Id != map.Id).Result.Any())
                return null;

            await _mapRepository.UpdateAsync(map);
            return map;
        }
    
    }
}
