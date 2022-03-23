using Application.DTOs.MapDTO;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Pagination;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Validations;

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
            if (MapValidation.Validate(map))
            {
                await _unitOfWork.Maps.AddAsync(map);
                await _unitOfWork.CompleteAsync();
            }
            return map.Id;
        }

        public async Task<PagedList<GetMapDTO>> GetPagedAsync(PagedQueryBase query)
        {
            var maps = await _unitOfWork.Maps.GetPagedAsync(query);
            var mapMaps = _mapper.Map<List<GetMapDTO>>(maps);
            var mapsDTO = new PagedList<GetMapDTO>(mapMaps, maps.TotalCount, maps.CurrentPage, maps.PageSize);
            return mapsDTO;
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

            _unitOfWork.Maps.Remove(map);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<PagedList<GetMapDTO>> SearchByOfficeIdAsync(Guid officeId, PagedQueryBase query)
        {
            var maps = await _unitOfWork.Maps.GetPagedByOfficeIdAsync(officeId, query);
            var mapMaps = _mapper.Map<List<GetMapDTO>>(maps);
            var mapsDTO = new PagedList<GetMapDTO>(mapMaps, maps.TotalCount, maps.CurrentPage, maps.PageSize);
            return mapsDTO;
        }

        public async Task<GetMapDTO> UpdateAsync(UpdateMapDTO mapDTO)
        {
            var map = await _unitOfWork.Maps.GetByIdAsync(mapDTO.Id);
            if (map == null)
                return null;
            
            _mapper.Map(mapDTO, map);
            
            if (MapValidation.Validate(map))
            {
                _unitOfWork.Maps.Update(map);
                await _unitOfWork.CompleteAsync();
            }
            return _mapper.Map<GetMapDTO>(map);
        }
    }
}
