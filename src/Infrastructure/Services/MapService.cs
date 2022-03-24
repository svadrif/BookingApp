using Application.DTOs.MapDTO;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Pagination;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Validations;
using Application.Interfaces;

namespace Infrastructure.Services {
    public class MapService: IMapService {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        public MapService(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager _logger) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = _logger;
        }

        public async Task < Guid > AddAsync(AddMapDTO mapDTO) {
           
            try
            {
                Map map = _mapper.Map < Map > (mapDTO);
                if (MapValidation.Validate(map)) {
                    await _unitOfWork.Maps.AddAsync(map);
                    await _unitOfWork.CompleteAsync();
                    return map.Id;
                }
                else
                {
                    throw new Exception();
                }
            } catch (Exception ex) {
                _logger.LogWarn($"Non correct values in the {nameof(AddAsync)} action {ex}");
                return Guid.Empty;
            } 
        }

        public async Task < PagedList < GetMapDTO >> GetPagedAsync(PagedQueryBase query) {
            try {
                var maps = await _unitOfWork.Maps.GetPagedAsync(query);
                var mapMaps = _mapper.Map < List < GetMapDTO >> (maps);
                var mapsDTO = new PagedList < GetMapDTO > (mapMaps, maps.TotalCount, maps.CurrentPage, maps.PageSize);
                return mapsDTO;
            } catch (Exception ex) {
                _logger.LogWarn($"Non correct values in the {nameof(GetPagedAsync)} action {ex}");
                return null;
            }
        }

        public async Task < GetMapDTO > GetByIdAsync(Guid Id) {
            try {
                var map = await _unitOfWork.Maps.GetByIdAsync(Id);
                return _mapper.Map < GetMapDTO > (map);
            } catch (Exception ex) {
                _logger.LogWarn($"Non correct values in the {nameof(GetByIdAsync)} action {ex}");
                return null;
            }
        }

        public async Task < bool > RemoveAsync(Guid Id) {
            try {
                var map = await _unitOfWork.Maps.GetByIdAsync(Id);
                if (map == null)
                    return false;

                _unitOfWork.Maps.Remove(map);
                await _unitOfWork.CompleteAsync();
                return true;
            } catch (Exception ex) {
                _logger.LogWarn($"Non correct values in the {nameof(RemoveAsync)} action {ex}");
                return false;
            }
        }

        public async Task < PagedList < GetMapDTO >> SearchByOfficeIdAsync(Guid officeId, PagedQueryBase query) {
            try {
                var maps = await _unitOfWork.Maps.GetPagedByOfficeIdAsync(officeId, query);
                var mapMaps = _mapper.Map < List < GetMapDTO >> (maps);
                var mapsDTO = new PagedList < GetMapDTO > (mapMaps, maps.TotalCount, maps.CurrentPage, maps.PageSize);
                return mapsDTO;
            } catch (Exception ex) {
                _logger.LogWarn($"Non correct values in the {nameof(SearchByOfficeIdAsync)} action {ex}");
                return null;
            }
        }

        public async Task < GetMapDTO > UpdateAsync(UpdateMapDTO mapDTO) {
            try {
                var map = await _unitOfWork.Maps.GetByIdAsync(mapDTO.Id);
                if (map == null)
                    return null;

                _mapper.Map(mapDTO, map);

                if (MapValidation.Validate(map)) {
                    _unitOfWork.Maps.Update(map);
                    await _unitOfWork.CompleteAsync();
                    return _mapper.Map < GetMapDTO > (map);

                } else {
                    throw new Exception();
                }
            } catch (Exception ex) {
                _logger.LogWarn($"Non correct values in the {nameof(UpdateAsync)} action {ex}");
                return null;
            }
        }
    }
}