using Application.DTOs.OfficeDTO;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Pagination;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Validations;
using Application.Interfaces;

namespace Infrastructure.Services {
    public class OfficeService: IOfficeService {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        public OfficeService(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager logger) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task < Guid > AddAsync(AddOfficeDTO officeDTO) {
            try {
                Office office = _mapper.Map < Office > (officeDTO);
                if (OfficeValidation.Validate(office)) {
                    await _unitOfWork.Offices.AddAsync(office);
                    await _unitOfWork.CompleteAsync();
                    return office.Id;

                } else {
                    throw new Exception();
                }
            } catch (Exception ex) {
                _logger.LogWarn($"Non correct values in the {nameof(AddAsync)} action {ex}");
                return Guid.Empty;
            }
        }

        public async Task < PagedList < GetOfficeDTO >> GetPagedAsync(PagedQueryBase query) {
            try {
                var offices = await _unitOfWork.Offices.GetPagedAsync(query);
                var mapOffices = _mapper.Map < List < GetOfficeDTO >> (offices);
                var officesDTO = new PagedList < GetOfficeDTO > (mapOffices, offices.TotalCount, offices.CurrentPage, offices.PageSize);
                return officesDTO;
            } catch (Exception ex) {
                _logger.LogWarn($"Non correct values in the {nameof(GetPagedAsync)} action {ex}");
                return null;
            }
        }

        public async Task < PagedList < GetOfficeDTO >> GetPagedByCityAsync(string city, PagedQueryBase query) {
            var offices = await _unitOfWork.Offices.GetPagedByCityAsync(city, query);
            var mapOffices = _mapper.Map < List < GetOfficeDTO >> (offices);
            var officesDTO = new PagedList < GetOfficeDTO > (mapOffices, offices.TotalCount, offices.CurrentPage, offices.PageSize);
            return officesDTO;
        }

        public async Task < GetOfficeDTO > GetByIdAsync(Guid Id) {
            try {
                var office = await _unitOfWork.Offices.GetByIdAsync(Id);
                return _mapper.Map < GetOfficeDTO > (office);
            } catch (Exception ex) {
                _logger.LogWarn($"Non correct values in the {nameof(GetByIdAsync)} action {ex}");
                return null;
            }
        }

        public async Task < bool > RemoveAsync(Guid Id) {
            try {
                var office = await _unitOfWork.Offices.GetByIdAsync(Id);
                if (office == null)
                    return false;

                _unitOfWork.Offices.Remove(office);
                await _unitOfWork.CompleteAsync();
                return true;
            } catch (Exception ex) {
                _logger.LogWarn($"Non correct values in the {nameof(RemoveAsync)} action {ex}");
                return false;
            }
        }

        public async Task < GetOfficeDTO > UpdateAsync(UpdateOfficeDTO officeDTO) {
            try {
                var office = await _unitOfWork.Offices.GetByIdAsync(officeDTO.Id);
                if (office == null)
                    return null;

                _mapper.Map(officeDTO, office);
                if (OfficeValidation.Validate(office)) {
                    _unitOfWork.Offices.Update(office);
                    await _unitOfWork.CompleteAsync();
                    return _mapper.Map < GetOfficeDTO > (office);

                } else {
                    throw new Exception();
                }
            } catch (Exception ex) {
                _logger.LogWarn($"Non correct values in the {nameof(UpdateAsync)} action {ex}");
                return null;
            }
        }

        public async Task < PagedList < string >> GetCountriesAsync(PagedQueryBase query) {
            try {
                return await _unitOfWork.Offices.GetPagedCountriesAsync(query);
            } catch (Exception ex) {
                _logger.LogWarn($"Non correct values in the {nameof(GetCountriesAsync)} action {ex}");
                return null;
            }
        }

        public async Task < PagedList < string >> GetCitiesAsync(string country, PagedQueryBase query) {
            try {
                return await _unitOfWork.Offices.GetPagedCitiesByCountryAsync(country, query);
            } catch (Exception ex) {
                _logger.LogWarn($"Non correct values in the {nameof(GetCitiesAsync)} action {ex}");
                return null;
            }
        }
    }
}