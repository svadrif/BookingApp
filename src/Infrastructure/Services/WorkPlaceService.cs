using Application.DTOs.WorkPlaceDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class WorkPlaceService : IWorkPlaceService
    {
        private readonly IWorkPlaceRepository _workPlaceRepository;
        private readonly IMapper _mapper;
        public WorkPlaceService(IWorkPlaceRepository workPlaceRepository, IMapper mapper)
        {
            _workPlaceRepository = workPlaceRepository;
            _mapper = mapper;
        }
        public async Task<WorkPlace> AddAsync(AddWorkPlaceDTO workPlaceDTO)
        {
            WorkPlace workPlace = _mapper.Map<WorkPlace>(workPlaceDTO);
            workPlace.Id = Guid.NewGuid();
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
            if (await _workPlaceRepository.GetByIdAsync(workPlace.Id) == null)
                return false;

            await _workPlaceRepository.RemoveAsync(workPlace);
            return true;
        }

        public async Task<IEnumerable<WorkPlace>> SearchByMapIdAsync(Guid MapId)
        {
            if (MapId == Guid.Empty)
                return null;

            return await _workPlaceRepository.SearchByMapIdAsync(c => c.MapId.Contains(MapId));
        }

        public async Task<WorkPlace> UpdateAsync(WorkPlace workPlace)
        {
            if (_workPlaceRepository.GetByIdAsync(workPlace.Id) == null)
                return null;

            await _workPlaceRepository.UpdateAsync(workPlace);
            return workPlace;
        }
    }
}
