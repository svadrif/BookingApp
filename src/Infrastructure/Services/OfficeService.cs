using Application.DTOs.OfficeDTO;
using Application.Interfaces;
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
            return office.Id;
        }

        public async Task<IEnumerable<GetOfficeDTO>> GetAllAsync()
        {
            var offices = await _unitOfWork.Offices.GetAllAsync();
            return _mapper.Map<IEnumerable<GetOfficeDTO>>(offices);
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

            await _unitOfWork.Offices.RemoveAsync(office);
            return true;
        }    

        public async Task<GetOfficeDTO> UpdateAsync(UpdateOfficeDTO officeDTO)
        {
            var office = await _unitOfWork.Offices.UpdateAsync(officeDTO.Id);
            if (office == null)
                return null;

            _mapper.Map(officeDTO, office);
            await _unitOfWork.Offices.UpdateAsync(office);
            return _mapper.Map<GetOfficeDTO>(office);
        }
    }
}
