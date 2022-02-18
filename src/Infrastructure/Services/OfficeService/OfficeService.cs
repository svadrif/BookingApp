using Application.Common;
using Application.DTOs.OfficeDTO;
using AutoMapper;
using Infrastructure.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.OfficeService
{
    public class OfficeService : IOfficeService
    {

        private readonly ApplicationDbContext _dataContext;
        private readonly IMapper _mapper;

        public OfficeService(ApplicationDbContext dataContext,IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetOfficeDTO>>>  AddOffice(AddOfficeDTO officeDto)
        {
            Office newOffice = new Office
            {
                Id = Guid.NewGuid(),
                Country = officeDto.Country,
                City = officeDto.City,
                Address = officeDto.Address,
                Name = officeDto.Name,
            };

            var serviceResponse = new ServiceResponse<List<GetOfficeDTO>>();
            _dataContext.Offices.Add(newOffice);
            await _dataContext.SaveChangesAsync();
            var list = await _dataContext.Offices.ToListAsync();
            serviceResponse.Data = list.Select(c=>_mapper.Map<GetOfficeDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetOfficeDTO>>>  GetAllOffices()
        {
            var serviceResponse = new ServiceResponse<List<GetOfficeDTO>>();
            var officeList = await _dataContext.Offices.ToListAsync();
            serviceResponse.Data = officeList.Select(c=>_mapper.Map<GetOfficeDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetOfficeDTO>>>  UpdateOffice(AddOfficeDTO office1,Guid id)
        {
            var serviceResponse = new ServiceResponse<List<GetOfficeDTO>>();

            var office = await _dataContext.Offices.FindAsync(id);
            if (office == null)
            {
                serviceResponse.Data = (await _dataContext.Offices.ToListAsync()).Select(c=>_mapper.Map<GetOfficeDTO>(c)).ToList();
                return serviceResponse;
            }
                
            office.Country = office1.Country;
            office.City = office1.City;
            office.Address = office1.Address;
            office.Name = office1.Name;
            await _dataContext.SaveChangesAsync();
            var officeList = await _dataContext.Offices.ToListAsync();
            serviceResponse.Data = officeList.Select(c => _mapper.Map<GetOfficeDTO>(c)).ToList();
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetOfficeDTO>>  GetById(Guid id)
        {
            var office = await _dataContext.Offices.FindAsync(id);
            var serviceResponse = new ServiceResponse<GetOfficeDTO>();
            serviceResponse.Data = _mapper.Map<GetOfficeDTO>(office); 
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetOfficeDTO>>>  DeleteOffice(Guid id)
        {
            var serviceResponse = new ServiceResponse<List<GetOfficeDTO>>();
            var office = await _dataContext.Offices.FindAsync(id);
            _dataContext.Offices.Remove(office);
            await _dataContext.SaveChangesAsync();
            var officeList = await _dataContext.Offices.ToListAsync();
            serviceResponse.Data = officeList.Select(c => _mapper.Map<GetOfficeDTO>(c)).ToList();
            return serviceResponse;

        }
    }
}
