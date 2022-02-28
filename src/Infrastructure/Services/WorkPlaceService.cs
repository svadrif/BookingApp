using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class WorkPlaceService : IWorkPlaceService
    {
        private readonly IWorkPlaceRepository _workPlaceRepository;
        private readonly IMapService _mapService;
        public WorkPlaceService(IWorkPlaceRepository workPlaceRepository, IMapService mapService)
        {
            _workPlaceRepository = workPlaceRepository;
            _mapService = mapService;
        }
        public async Task<WorkPlace> Add(WorkPlace workPlace)
        {
            if (_workPlaceRepository.Search(w => w.Id == workPlace.Id).Result.Any())
                return null;

            await _workPlaceRepository.Add(workPlace);
            return workPlace;
        }

        public async Task<IEnumerable<WorkPlace>> GetAll()
        {
            return await _workPlaceRepository.GetAll();
        }

        public async Task<WorkPlace> GetById(Guid Id)
        {
            return await _workPlaceRepository.GetById(Id);
        }

        public async Task<bool> Remove(WorkPlace workPlace)
        {
            var maps = await _mapService.Search(workPlace.MapId);
            if (maps.Any()) return false;

            await _workPlaceRepository.Remove(workPlace);
            return true;
        }

        public async Task<IEnumerable<WorkPlace>> Search(Guid MapId)
        {
            return await _workPlaceRepository.Search(c => c.MapId.Contains(MapId));
        }

        public async Task<WorkPlace> Update(WorkPlace workPlace)
        {
            if (_workPlaceRepository.Search(w => w.MapId == workPlace.MapId && w.Id != workPlace.Id).Result.Any())
                return null;

            await _workPlaceRepository.Update(workPlace);
            return workPlace;
        }

        public void Dispose()
        {
            _workPlaceRepository?.Dispose();
        }
    }
}
