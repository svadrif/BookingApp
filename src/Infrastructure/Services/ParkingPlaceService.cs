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
        public async Task<ParkingPlace> Add(ParkingPlace parkingPlace)
        {
            if (_parkingPlaceRepository.Search(p => p.Id = parkingPlace.Id).Result.Any())
                return null;

            await _parkingPlaceRepository.Add(parkingPlace);
            return parkingPlace;
        }

        public void Dispose()
        {
            _parkingPlaceRepository?.Dispose();
        }

        public async Task<IEnumerable<ParkingPlace>> GetAll()
        {
            return await _parkingPlaceRepository.GetAll();
        }

        public async Task<ParkingPlace> GetById(Guid Id)
        {
            return await _parkingPlaceRepository.GetById(Id);
        }

        public async Task<bool> Remove(ParkingPlace parkingPlace)
        {
            var offices = await _officeService.Search(parkingPlace.OfficeId);
            if (offices.Any()) return false;

            await _parkingPlaceRepository.Remove(parkingPlace);
            return true;
        }

        public async Task<IEnumerable<ParkingPlace>> Search(Guid OfficeId)
        {
            return await _parkingPlaceRepository.Search(c => c.OfficeId.Contains(OfficeId));
        }

        public async Task<ParkingPlace> Update(ParkingPlace parkingPlace)
        {
            if (_parkingPlaceRepository.Search(p => p.OfficeId == parkingPlace.OfficeId && p.Id != parkingPlace.Id).Result.Any())
                return null;

            await _parkingPlaceRepository.Update(parkingPlace);
            return parkingPlace;
        }
    }
}
