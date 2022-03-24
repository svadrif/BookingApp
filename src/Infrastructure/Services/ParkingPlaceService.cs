using Application.DTOs.ParkingPlaceDTO;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Pagination;
using AutoMapper;
using Domain.Entities;
using Application.Interfaces;

namespace Infrastructure.Services
{
    public class ParkingPlaceService : IParkingPlaceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        public ParkingPlaceService(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> AddAsync(AddParkingPlaceDTO parkingPlaceDTO)
        {
            try
            {
                ParkingPlace parkingPlace = _mapper.Map<ParkingPlace>(parkingPlaceDTO);
                await _unitOfWork.ParkingPlaces.AddAsync(parkingPlace);
                await _unitOfWork.CompleteAsync();
                return parkingPlace.Id;
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(AddAsync)} action {ex}");
                return Guid.Empty;
            }
        }

        public async Task<PagedList<GetParkingPlaceDTO>> GetPagedAsync(PagedQueryBase query)
        {
            try
            {
                var parkingPlaces = await _unitOfWork.ParkingPlaces.GetPagedAsync(query);
                var mapParkingPlaces = _mapper.Map<List<GetParkingPlaceDTO>>(parkingPlaces);
                var parkingPlacesDTO = new PagedList<GetParkingPlaceDTO>(mapParkingPlaces, parkingPlaces.TotalCount, parkingPlaces.CurrentPage, parkingPlaces.PageSize);
                return parkingPlacesDTO;
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(GetPagedAsync)} action {ex}");
                return null;
            }
        }

        public async Task<GetParkingPlaceDTO> GetByIdAsync(Guid Id)
        {
            try
            {
                var parkingPlace = await _unitOfWork.ParkingPlaces.GetByIdAsync(Id);
                return _mapper.Map<GetParkingPlaceDTO>(parkingPlace);
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(GetByIdAsync)} action {ex}");
                return null;
            }
        }

        public async Task<bool> RemoveAsync(Guid Id)
        {
            try
            {
                var parkingPlace = await _unitOfWork.ParkingPlaces.GetByIdAsync(Id);
                if (parkingPlace == null)
                    return false;

                _unitOfWork.ParkingPlaces.Remove(parkingPlace);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(RemoveAsync)} action {ex}");
                return false;
            }
        }

        public async Task<PagedList<GetParkingPlaceDTO>> SearchByOfficeIdAsync(Guid officeId, PagedQueryBase query)
        {
            try
            {
                var parkingPlaces = await _unitOfWork.ParkingPlaces.GetPagedByOfficeIdAsync(officeId, query);
                var parkingPlaceMaps = _mapper.Map<List<GetParkingPlaceDTO>>(parkingPlaces);
                var parkingPlacesDTO = new PagedList<GetParkingPlaceDTO>(parkingPlaceMaps, parkingPlaces.TotalCount, parkingPlaces.CurrentPage, parkingPlaces.PageSize);
                return parkingPlacesDTO;
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(SearchByOfficeIdAsync)} action {ex}");
                return null;
            }
        }

        public async Task<GetParkingPlaceDTO> UpdateAsync(UpdateParkingPlaceDTO parkingPlaceDTO)
        {
            try
            {
                var parkingPlace = await _unitOfWork.ParkingPlaces.GetByIdAsync(parkingPlaceDTO.Id);
                if (parkingPlace == null)
                    return null;

                _mapper.Map(parkingPlaceDTO, parkingPlace);
                _unitOfWork.ParkingPlaces.Update(parkingPlace);
                await _unitOfWork.CompleteAsync();
                return _mapper.Map<GetParkingPlaceDTO>(parkingPlace);
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(UpdateAsync)} action {ex}");
                return null;
            }
        }
    }
}
