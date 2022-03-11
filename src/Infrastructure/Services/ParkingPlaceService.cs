using Application.DTOs.ParkingPlaceDTO;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Pagination;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class ParkingPlaceService : IParkingPlaceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ParkingPlaceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(AddParkingPlaceDTO parkingPlaceDTO)
        {
            ParkingPlace parkingPlace = _mapper.Map<ParkingPlace>(parkingPlaceDTO);
            await _unitOfWork.ParkingPlaces.AddAsync(parkingPlace);
            await _unitOfWork.CompleteAsync();
            return parkingPlace.Id;
        }

        public async Task<PagedList<GetParkingPlaceDTO>> GetPagedAsync(PagedQueryBase query)
        {
            var parkingPlaces = await _unitOfWork.ParkingPlaces.GetPagedAsync(query);
            var parkingPlacesDTO = new PagedList<GetParkingPlaceDTO>(_mapper.Map<List<GetParkingPlaceDTO>>(parkingPlaces), parkingPlaces.TotalCount, parkingPlaces.CurrentPage, parkingPlaces.PageSize);
            return parkingPlacesDTO;
        }

        public async Task<GetParkingPlaceDTO> GetByIdAsync(Guid Id)
        {
            var parkingPlace = await _unitOfWork.ParkingPlaces.GetByIdAsync(Id);
            return _mapper.Map<GetParkingPlaceDTO>(parkingPlace);
        }

        public async Task<bool> RemoveAsync(Guid Id)
        {
            var parkingPlace = await _unitOfWork.ParkingPlaces.GetByIdAsync(Id);
            if (parkingPlace == null)
                return false;

            _unitOfWork.ParkingPlaces.Remove(parkingPlace);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<GetParkingPlaceDTO>> SearchByOfficeIdAsync(Guid? OfficeId)
        {
            var parkingPlaces = _unitOfWork.ParkingPlaces.Search(c => c.OfficeId.Equals(OfficeId), false);
            if (parkingPlaces == null)
                return null;

            return _mapper.Map<IEnumerable<GetParkingPlaceDTO>>(parkingPlaces);
        }

        public async Task<GetParkingPlaceDTO> UpdateAsync(UpdateParkingPlaceDTO parkingPlaceDTO)
        {
            var parkingPlace = await _unitOfWork.ParkingPlaces.GetByIdAsync(parkingPlaceDTO.Id);
            if (parkingPlace == null)
                return null;

            _mapper.Map(parkingPlaceDTO, parkingPlace);
            _unitOfWork.ParkingPlaces.Update(parkingPlace);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<GetParkingPlaceDTO>(parkingPlace);
        }
    }
}
