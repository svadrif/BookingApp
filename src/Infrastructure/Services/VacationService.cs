using Application.DTOs.VacationDTO;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Pagination;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Validations;

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
            if (VacationValidation.Validate(vacation))
            {
                await _unitOfWork.Vacations.AddAsync(vacation);
                await _unitOfWork.CompleteAsync();
            }
            return vacation.Id;
        }

        public async Task<PagedList<GetVacationDTO>> GetPagedAsync(PagedQueryBase query)
        {
            var vacations = await _unitOfWork.Vacations.GetPagedAsync(query);
            var mapVacations = _mapper.Map<List<GetVacationDTO>>(vacations);
            var vacationsDTO = new PagedList<GetVacationDTO>(mapVacations, vacations.TotalCount, vacations.CurrentPage, vacations.PageSize);
            return vacationsDTO;
        }

        public async Task<GetVacationDTO> GetByIdAsync(Guid Id)
        {
            var vacation = await _unitOfWork.Vacations.GetByIdAsync(Id);
            return _mapper.Map<GetVacationDTO>(vacation);
        }

        public async Task<bool> RemoveAsync(Guid Id)
        {
            var vacation = await _unitOfWork.Vacations.GetByIdAsync(Id);
            if (vacation == null)
                return false;

            _unitOfWork.Vacations.Remove(vacation);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<GetVacationDTO> UpdateAsync(UpdateVacationDTO vacationDTO)
        {
            var vacation = await _unitOfWork.Vacations.GetByIdAsync(vacationDTO.Id);
            if (vacation == null)
                return null;

            _mapper.Map(vacationDTO, vacation);
            if (VacationValidation.Validate(vacation))
            {
                _unitOfWork.Vacations.Update(vacation);
                await _unitOfWork.CompleteAsync();
            }
            return _mapper.Map<GetVacationDTO>(vacation);
        }

        public async Task<IEnumerable<GetVacationDTO>> SearchByUserIdAsync(Guid UserId)
        {
            var vacations = _unitOfWork.Vacations.Search(c => c.UserId.Equals(UserId), false);
            if (vacations == null)
                return null;

            return _mapper.Map<IEnumerable<GetVacationDTO>>(vacations);
        }
    }
}
