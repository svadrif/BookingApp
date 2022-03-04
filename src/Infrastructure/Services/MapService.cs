using Application.DTOs.MapDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class MapService : IMapService
    {
        private readonly IMapRepository _mapRepository;
        private readonly IMapper _mapper;
        public MapService(IMapRepository mapRepository, IMapper mapper)
        {
            _mapRepository = mapRepository;
            _mapper = mapper;
        }
        public async Task<Map> AddAsync(AddMapDTO mapDTO)
        {
            Map map = _mapper.Map<Map>(mapDTO);
            map.Id = Guid.NewGuid();
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
            if (await _mapRepository.GetByIdAsync(map.Id) == null)
                return false;

            await _mapRepository.RemoveAsync(map);
            return true;
        }

        public async Task<IEnumerable<Map>> SearchByOfficeIdAsync(Guid OfficeId)
        {
            if (OfficeId == Guid.Empty)
                return null;

            return await _mapRepository.SearchByOfficeIdAsync(c => c.OfficeId.Contains(OfficeId));
        }

        public async Task<Map> UpdateAsync(Map map)
        {
            if (_mapRepository.GetByIdAsync(map.Id) == null)
                return null;

            await _mapRepository.UpdateAsync(map);
            return map;
        }
    
    }
}
