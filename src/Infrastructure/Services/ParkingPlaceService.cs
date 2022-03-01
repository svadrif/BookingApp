using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class ParkingPlaceService : IParkingPlaceService
    {
        private readonly IParkingPlaceRepository _parkingPlaceRepository;
        private readonly IOfficeService _officeService;

        public ParkingPlaceService(IParkingPlaceRepository parkingPlaceRepository, IOfficeService officeService)
        {
            _parkingPlaceRepository = parkingPlaceRepository;
            _officeService = officeService;
        }
        public async Task<ParkingPlace> AddAsync(ParkingPlace parkingPlace)
        {
            if (_parkingPlaceRepository.SearchAsync(p => p.Id = parkingPlace.Id).Result.Any())
                return null;

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
            var offices = await _officeService.SearchAsync(parkingPlace.OfficeId);
            if (offices.Any()) return false;

            await _parkingPlaceRepository.RemoveAsync(parkingPlace);
            return true;
        }

        public async Task<IEnumerable<ParkingPlace>> SearchAsync(Guid? OfficeId)
        {
            return await _parkingPlaceRepository.SearchAsync(c => c.OfficeId.Contains(OfficeId));
        }

        public async Task<ParkingPlace> UpdateAsync(ParkingPlace parkingPlace)
        {
            if (_parkingPlaceRepository.SearchAsync(p => p.OfficeId == parkingPlace.OfficeId && p.Id != parkingPlace.Id).Result.Any())
                return null;

            await _parkingPlaceRepository.UpdateAsync(parkingPlace);
            return parkingPlace;
        }
    }
}
