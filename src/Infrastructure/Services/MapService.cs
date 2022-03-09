using Application.DTOs.MapDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class MapService : IMapService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MapService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Guid> AddAsync(AddMapDTO mapDTO)
        {
            Map map = _mapper.Map<Map>(mapDTO);
            await _unitOfWork.Maps.AddAsync(map);
            return map.Id;
        }

        public async Task<IEnumerable<GetMapDTO>> GetAllAsync()
        {
            var maps = await _unitOfWork.Maps.GetAllAsync();
            return _mapper.Map<IEnumerable<GetMapDTO>>(maps);
        }

        public async Task<GetMapDTO> GetByIdAsync(Guid Id)
        {
            var map = await _unitOfWork.Maps.GetByIdAsync(Id);
            return _mapper.Map<GetMapDTO>(map);
        }

        public async Task<bool> RemoveAsync(Guid Id)
        {
            var map = await _unitOfWork.Maps.GetByIdAsync(Id);
            if (map == null)
                return false;

            await _unitOfWork.Maps.RemoveAsync(map);
            return true;
        }

        public async Task<IEnumerable<GetMapDTO>> SearchByOfficeIdAsync(Guid OfficeId)
        {
            var maps = await _unitOfWork.Maps.Search(c => c.OfficeId.Contains(OfficeId));
            if (maps == null)
                return null;

            return _mapper.Map<IEnumerable<GetMapDTO>>(maps);
        }

        public async Task<GetMapDTO> UpdateAsync(UpdateMapDTO mapDTO)
        {
            var map = await _unitOfWork.Maps.GetByIdAsync(mapDTO.Id);
            if (map == null)
                return null;

            _mapper.Map(mapDTO, map);
            await _unitOfWork.Maps.UpdateAsync(map);
            return _mapper.Map<GetMapDTO>(map);
        }
    
    }
}
