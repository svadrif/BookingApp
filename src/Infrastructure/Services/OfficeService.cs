using Application.DTOs.OfficeDTO;
using Application.Interfaces.IServices;
using Application.Interfaces.IRepositories;
using Application.Pagination;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OfficeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(AddOfficeDTO officeDTO)
        {
            Office office = _mapper.Map<Office>(officeDTO);
            await _unitOfWork.Offices.AddAsync(office);
            await _unitOfWork.CompleteAsync();
            return office.Id;
        }

        public async Task<PagedList<GetOfficeDTO>> GetPagedAsync(PagedQueryBase query)
        {
            var offices = await _unitOfWork.Offices.GetPagedAsync(query);
            var officesDTO = new PagedList<GetOfficeDTO>(_mapper.Map<List<GetOfficeDTO>>(offices), offices.TotalCount, offices.CurrentPage, offices.PageSize);
            return officesDTO;
        }

        public async Task<GetOfficeDTO> GetByIdAsync(Guid Id)
        {
            var office = await _unitOfWork.Offices.GetByIdAsync(Id);
            return _mapper.Map<GetOfficeDTO>(office);
        }

        public async Task<bool> RemoveAsync(Guid Id)
        {
            var office = await _unitOfWork.Offices.GetByIdAsync(Id);
            if (office == null)
                return false;

            _unitOfWork.Offices.Remove(office);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<GetOfficeDTO> UpdateAsync(UpdateOfficeDTO officeDTO)
        {
            var office = await _unitOfWork.Offices.GetByIdAsync(officeDTO.Id);
            if (office == null)
                return null;

            _mapper.Map(officeDTO, office);
            _unitOfWork.Offices.Update(office);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<GetOfficeDTO>(office);
        }
    }
}
