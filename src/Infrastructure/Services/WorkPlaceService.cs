using Application.DTOs.WorkPlaceDTO;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Pagination;
using AutoMapper;
using Domain.Entities;
using Application.Interfaces;

namespace Infrastructure.Services
{
    public class WorkPlaceService : IWorkPlaceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        public WorkPlaceService(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> AddAsync(AddWorkPlaceDTO workPlaceDTO)
        {
            try
            {
                WorkPlace workPlace = _mapper.Map<WorkPlace>(workPlaceDTO);
                await _unitOfWork.WorkPlaces.AddAsync(workPlace);
                await _unitOfWork.CompleteAsync();
                return workPlace.Id;
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(AddAsync)} action {ex}");
                return Guid.Empty;
            }
        }

        public async Task<PagedList<GetWorkPlaceDTO>> GetPagedAsync(PagedQueryBase query)
        {
            try
            {
                var workPlaces = await _unitOfWork.WorkPlaces.GetPagedAsync(query);
                var mapWorkPlaces = _mapper.Map<List<GetWorkPlaceDTO>>(workPlaces);
                var workPlacesDTO = new PagedList<GetWorkPlaceDTO>(mapWorkPlaces, workPlaces.TotalCount, workPlaces.CurrentPage, workPlaces.PageSize);
                return workPlacesDTO;
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(GetPagedAsync)} action {ex}");
                return null;
            }
        }

        public async Task<GetWorkPlaceDTO> GetByIdAsync(Guid Id)
        {
            try
            {
                var workPlace = await _unitOfWork.WorkPlaces.GetByIdAsync(Id);
                return _mapper.Map<GetWorkPlaceDTO>(workPlace);
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(GetByIdAsync)} action {ex}");
                return null;
            }
        }
        public async Task<PagedList<GetWorkPlaceDTO>> GetByAttributesAsync(bool isNextToWindow, bool hasPc, bool hasMonitor, bool hasKeyboard,
            bool hasMouse, bool hasHeadset, PagedQueryBase query)
        {
            var workPlaces = await _unitOfWork.WorkPlaces.GetPagedByAttributesAsync(isNextToWindow,  hasPc,  hasMonitor,  hasKeyboard,
                 hasMouse,  hasHeadset,  query);
            var mapWorkPlaces = _mapper.Map<List<GetWorkPlaceDTO>>(workPlaces);
            var workPlacesDTO = new PagedList<GetWorkPlaceDTO>(mapWorkPlaces, workPlaces.TotalCount, workPlaces.CurrentPage, workPlaces.PageSize);
            return workPlacesDTO;
        }
        public async Task<bool> RemoveAsync(Guid Id)
        {
            try
            {
                var workPlace = await _unitOfWork.WorkPlaces.GetByIdAsync(Id);
                if (workPlace == null)
                    return false;

                _unitOfWork.WorkPlaces.Remove(workPlace);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(RemoveAsync)} action {ex}");
                return false;
            }
        }

        public async Task<PagedList<GetWorkPlaceDTO>> SearchByMapIdAsync(Guid mapId, PagedQueryBase query)
        {
            try
            {
                var workPlaces = await _unitOfWork.WorkPlaces.GetPagedByMapIdAsync(mapId, query);
                var workPlaceMaps = _mapper.Map<List<GetWorkPlaceDTO>>(workPlaces);
                var workPlacesDTO = new PagedList<GetWorkPlaceDTO>(workPlaceMaps, workPlaces.TotalCount, workPlaces.CurrentPage, workPlaces.PageSize);
                return workPlacesDTO;
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(SearchByMapIdAsync)} action {ex}");
                return null;
            }
        }

        public async Task<GetWorkPlaceDTO> UpdateAsync(UpdateWorkPlaceDTO workPlaceDTO)
        {
            try
            {
                var workPlace = await _unitOfWork.WorkPlaces.GetByIdAsync(workPlaceDTO.Id);
                if (workPlace == null)
                    return null;

                _mapper.Map(workPlaceDTO, workPlace);
                _unitOfWork.WorkPlaces.Update(workPlace);
                await _unitOfWork.CompleteAsync();
                return _mapper.Map<GetWorkPlaceDTO>(workPlace);
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(UpdateAsync)} action {ex}");
                return null;
            }
        }
    }
}
