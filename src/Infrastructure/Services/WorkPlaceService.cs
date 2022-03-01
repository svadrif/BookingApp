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
        public async Task<WorkPlace> AddAsync(WorkPlace workPlace)
        {
            if (_workPlaceRepository.SearchAsync(w => w.Id == workPlace.Id).Result.Any())
                return null;

            await _workPlaceRepository.AddAsync(workPlace);
            return workPlace;
        }

        public async Task<IEnumerable<WorkPlace>> GetAllAsync()
        {
            return await _workPlaceRepository.GetAllAsync();
        }

        public async Task<WorkPlace> GetByIdAsync(Guid Id)
        {
            return await _workPlaceRepository.GetByIdAsync(Id);
        }

        public async Task<bool> RemoveAsync(WorkPlace workPlace)
        {
            var maps = await _mapService.SearchAsync(workPlace.MapId);
            if (maps.Any()) return false;

            await _workPlaceRepository.RemoveAsync(workPlace);
            return true;
        }

        public async Task<IEnumerable<WorkPlace>> SearchAsync(Guid MapId)
        {
            return await _workPlaceRepository.SearchAsync(c => c.MapId.Contains(MapId));
        }

        public async Task<WorkPlace> UpdateAsync(WorkPlace workPlace)
        {
            if (_workPlaceRepository.SearchAsync(w => w.MapId == workPlace.MapId && w.Id != workPlace.Id).Result.Any())
                return null;

            await _workPlaceRepository.UpdateAsync(workPlace);
            return workPlace;
        }
    }
}
