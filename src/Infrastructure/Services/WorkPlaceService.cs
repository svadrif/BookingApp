using Application.DTOs.WorkPlaceDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class WorkPlaceService : IWorkPlaceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public WorkPlaceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Guid> AddAsync(AddWorkPlaceDTO workPlaceDTO)
        {
            WorkPlace workPlace = _mapper.Map<WorkPlace>(workPlaceDTO);
            await _unitOfWork.WorkPlaces.AddAsync(workPlace);
            return workPlace.Id;
        }

        public async Task<IEnumerable<GetWorkPlaceDTO>> GetAllAsync()
        {
            var workPalces = await _unitOfWork.WorkPlaces.GetAllAsync();
            return _mapper.Map<GetWorkPlaceDTO>(workPalces);
        }

        public async Task<GetWorkPlaceDTO> GetByIdAsync(Guid Id)
        {
            var workPlace = _unitOfWork.WorkPlaces.GetByIdAsync(Id);
            return _mapper.Map<GetWorkPlaceDTO>(workPlace);
        }

        public async Task<bool> RemoveAsync(Guid Id)
        {
            var workPlace = await _unitOfWork.WorkPlaces.GetByIdAsync(Id);
            if (workPlace == null)
                return false;

            await _unitOfWork.WorkPlaces.RemoveAsync(workPlace);
            return true;
        }

        public async Task<IEnumerable<GetWorkPlaceDTO>> SearchByMapIdAsync(Guid MapId)
        {
            var workPlaces = await _unitOfWork.WorkPlaces.Search(c => c.MapId.Contains(MapId));
            if (workPlaces == null)
                return null;

            return _mapper.Map<IEnumerable<GetWorkPlaceDTO>>(workPlaces);
        }

        public async Task<GetWorkPlaceDTO> UpdateAsync(UpdateWorkPlaceDTO workPlaceDTO)
        {
            var workPlace = await _unitOfWork.WorkPlaces.GetByIdAsync(workPlaceDTO.Id);
            if (workPlace == null)
                return null;

            _mapper.Map(workPlaceDTO, workPlace);
            await _unitOfWork.WorkPlaces.UpdateAsync(workPlace);
            return _mapper.Map<GetWorkPlaceDTO>(workPlace);
        }
    }
}
