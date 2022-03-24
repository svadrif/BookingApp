using Application.DTOs.VacationDTO;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Pagination;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Validations;
using Application.Interfaces;

namespace Infrastructure.Services {
        public class VacationService: IVacationService {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ILoggerManager _logger;

            public VacationService(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager logger) {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task < Guid > AddAsync(AddVacationDTO vacationDTO) {
                try {
                    Vacation vacation = _mapper.Map < Vacation > (vacationDTO);
                    if (VacationValidation.Validate(vacation)) {
                        await _unitOfWork.Vacations.AddAsync(vacation);
                        await _unitOfWork.CompleteAsync();
                        return vacation.Id;

                    } else {
                        throw Exception e;
                    }
                } catch (Exception ex) {
                    _logger.LogWarn($"Non correct values in the {nameof(AddAsync)} action {ex}");
                    return Guid.Empty;
                }
            }

            public async Task < PagedList < GetVacationDTO >> GetPagedAsync(PagedQueryBase query) {
                try {
                    var vacations = await _unitOfWork.Vacations.GetPagedAsync(query);
                    var mapVacations = _mapper.Map < List < GetVacationDTO >> (vacations);
                    var vacationsDTO = new PagedList < GetVacationDTO > (mapVacations, vacations.TotalCount, vacations.CurrentPage, vacations.PageSize);
                    return vacationsDTO;
                } catch (Exception ex) {
                    _logger.LogWarn($"Non correct values in the {nameof(GetPagedAsync)} action {ex}");
                    return null;
                }
            }

            public async Task < GetVacationDTO > GetByIdAsync(Guid Id) {
                try {
                    var vacation = await _unitOfWork.Vacations.GetByIdAsync(Id);
                    return _mapper.Map < GetVacationDTO > (vacation);
                } catch (Exception ex) {
                    _logger.LogWarn($"Non correct values in the {nameof(GetByIdAsync)} action {ex}");
                    return null;
                }
            }

            public async Task < bool > RemoveAsync(Guid Id) {
                try {
                    var vacation = await _unitOfWork.Vacations.GetByIdAsync(Id);
                    if (vacation == null)
                        return false;

                    _unitOfWork.Vacations.Remove(vacation);
                    await _unitOfWork.CompleteAsync();
                    return true;
                } catch (Exception ex) {
                    _logger.LogWarn($"Non correct values in the {nameof(RemoveAsync)} action {ex}");
                    return false;
                }
            }

            public async Task < GetVacationDTO > UpdateAsync(UpdateVacationDTO vacationDTO) {
                try {
                    var vacation = await _unitOfWork.Vacations.GetByIdAsync(vacationDTO.Id);
                    if (vacation == null)
                        return null;

                    _mapper.Map(vacationDTO, vacation);
                    if (VacationValidation.Validate(vacation)) {
                        _unitOfWork.Vacations.Update(vacation);
                        await _unitOfWork.CompleteAsync();
                        return _mapper.Map < GetVacationDTO > (vacation);

                    } else {
                        throw Exception e;
                    } catch (Exception ex) {
                        _logger.LogWarn($"Non correct values in the {nameof(UpdateAsync)} action {ex}");
                        return null;
                    }
                }

                public async Task < IEnumerable < GetVacationDTO >> SearchByUserIdAsync(Guid UserId) {
                    try {
                        var vacations = _unitOfWork.Vacations.Search(c => c.UserId.Equals(UserId), false);
                        if (vacations == null)
                            return null;

                        return _mapper.Map < IEnumerable < GetVacationDTO >> (vacations);
                    } catch (Exception ex) {
                        _logger.LogWarn($"Non correct values in the {nameof(SearchByUserIdAsync)} action {ex}");
                        return null;
                    }
                }
            }
        }