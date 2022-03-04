using Application.DTOs.ParkingPlaceDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class ParkingPlaceService : IParkingPlaceService
    {
        private readonly IParkingPlaceRepository _parkingPlaceRepository;
        private readonly IMapper _mapper;

        public ParkingPlaceService(IParkingPlaceRepository parkingPlaceRepository, IMapper mapper)
        {
            _parkingPlaceRepository = parkingPlaceRepository;
            _mapper = mapper;
        }
        public async Task<ParkingPlace> AddAsync(AddParkingPlaceDTO parkingPlaceDTO)
        {
            ParkingPlace parkingPlace = _mapper.Map<ParkingPlace>(parkingPlaceDTO);
            parkingPlace.Id = Guid.NewGuid();
            await _parkingPlaceRepository.AddAsync(parkingPlace);
            return parkingPlace;
        }

        public async Task<IEnumerable<ParkingPlace>> GetAllAsync()
        {
            return await _parkingPlaceRepository.GetAllAsync();
        }

        public async Task<ParkingPlace> GetByIdAsync(Guid Id)
        {
            return await _parkingPlaceRepository.GetByIdAsync(Id);
        }

        public async Task<bool> RemoveAsync(ParkingPlace parkingPlace)
        {
            if (await _parkingPlaceRepository.GetByIdAsync(parkingPlace.Id) == null)
                return false;

            await _parkingPlaceRepository.RemoveAsync(parkingPlace);
            return true;
        }

        public async Task<IEnumerable<ParkingPlace>> SearchByOfficeIdAsync(Guid? OfficeId)
        {
            if (OfficeId == Guid.Empty)
                return null;

            return await _parkingPlaceRepository.SearchBYOfficeIdAsync(c => c.OfficeId.Contains(OfficeId));
        }

        public async Task<ParkingPlace> UpdateAsync(ParkingPlace parkingPlace)
        {
            if (_parkingPlaceRepository.GetByIdAsync(parkingPlace.Id) == null)
                return null;

            await _parkingPlaceRepository.UpdateAsync(parkingPlace);
            return parkingPlace;
        }
    }
}
