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
            await _unitOfWork.CompleteAsync();
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

            _unitOfWork.WorkPlaces.Remove(workPlace);
            await _unitOfWork.CompleteAsync();
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
            _unitOfWork.WorkPlaces.Update(workPlace);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<GetWorkPlaceDTO>(workPlace);
        }
    }
}
