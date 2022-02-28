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
        public async Task<Map> Add(Map map)
        {
            if (_mapRepository.Search(m => m.Id == map.Id).Result.Any())
                return null;

            await _mapRepository.Add(map);
            return map;
        }

        public void Dispose()
        {
            _mapRepository?.Dispose();
        }

        public async Task<IEnumerable<Map>> GetAll()
        {
            return await _mapRepository.GetAll();
        }

        public async Task<Map> GetById(Guid Id)
        {
            return await _mapRepository.GetById(Id);
        }

        public async Task<bool> Remove(Map map)
        {
            var offices = await _officeService.Search(map.OfficeId);
            if (offices.Any()) return false;

            await _mapRepository.Remove(map);
            return true;
        }

        public async Task<IEnumerable<Map>> Search(Guid OfficeId)
        {
            return await _mapRepository.Search(c => c.OfficeId.Contains(OfficeId));
        }

        public async Task<Map> Update(Map map)
        {
            if (_mapRepository.Search(m => m.OfficeId == map.OfficeId && m.Id != map.Id).Result.Any())
                return null;

            await _mapRepository.Update(map);
            return map;
        }
    
    }
}
