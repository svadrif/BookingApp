using Application.DTOs.VacationDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class VacationService : IVacationService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VacationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(AddVacationDTO vacationDTO)
        {
            Vacation vacation = _mapper.Map<Vacation>(vacationDTO);
            await _unitOfWork.Vacations.AddAsync(vacation);
            return vacation.Id;
        }

        public async Task<IEnumerable<GetVacationDTO>> GetAllAsync()
        {
            var vacations = await _unitOfWork.Vacations.GetAllAsync();
            return await _mapper.Map<IEnumerable<GetVacationDTO>>(vacations);
        }

        public async Task<GetVacationDTO> GetByIdAsync(Guid Id)
        {
            var vacation = await _unitOfWork.Vacations.GetByIdAsync(Id);
            return await _mapper.Map<GetVacationDTO>(vacation);
        }

        public async Task<bool> RemoveAsync(Guid Id)
        {
            var vacation = await _unitOfWork.Vacations.GetByIdAsync(Id);
            if (vacation == null)
                return false;
          
            await _unitOfWork.Vacations.RemoveAsync(vacation);
            return true;
        }

        public async Task<GetVacationDTO> UpdateAsync(UpdateVacationDTO vacationDTO)
        {
            var vacation = await _unitOfWork.Vacations.GetByIdAsync(vacationDTO.Id);
            if (vacation == null)
                return null;

            _mapper.Map(vacationDTO, vacation);
            await _unitOfWork.Vacations.UpdateAsync(vacation);
            return await _mapper.Map<GetVacationDTO>(vacation);
        }

        public async Task<IEnumerable<GetVacationDTO>> SearchByUserIdAsync(Guid UserId)
        {
            var vacations = await _unitOfWork.Vacations.SearchAsync(c => c.UserId.Contains(UserId));
            if (vacations == null)
                return null;

            return await _mapper.Map<IEnumerable<GetVacationDTO>>(vacations);
        }
    }
}
