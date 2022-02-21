using Application.Common;
using Application.Dtos.Vacation;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.VacationService
{
    public class VacaionService : IVacationService
    {
        static Guid guid = Guid.NewGuid();
        private static List<Vacation> vacations = new List<Vacation>();

        private readonly IMapper _mapper;

        public VacaionService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetVacationDTO>>> AddVacation(AddVacationDTO newVacation)
        {
            var serviceResponse = new ServiceResponse<List<GetVacationDTO>>();
            Vacation vacation = _mapper.Map<Vacation>(newVacation);
            vacation.Id = Guid.NewGuid();
            vacations.Add(vacation);
            serviceResponse.Data = vacations.Select(c => _mapper.Map<GetVacationDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetVacationDTO>>> GetAllVacations()
        {
            var serviceResponse = new ServiceResponse<List<GetVacationDTO>>();
            serviceResponse.Data = vacations.Select(c => _mapper.Map<GetVacationDTO>(c)).ToList(); 
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetVacationDTO>> GetVacationById(Guid id)
        {
            var serviceResponse = new ServiceResponse<GetVacationDTO>();
            serviceResponse.Data = _mapper.Map<GetVacationDTO>(vacations.FirstOrDefault(c => c.Id == id));
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetVacationDTO>> UpdateVacation(UpdateVacationDTO updatedVacation)
        {
            var serviceResponse = new ServiceResponse<GetVacationDTO>();
            try
            {
                Vacation vacation = vacations.FirstOrDefault(c => c.Id == updatedVacation.Id);
                
                vacation.VacationStart = updatedVacation.VacationStart;
                vacation.VacationEnd = updatedVacation.VacationEnd;

                serviceResponse.Data = _mapper.Map<GetVacationDTO>(vacation);
            }
            catch (Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetVacationDTO>>> DeleteVacation(Guid id)
        {
            var serviceResponse = new ServiceResponse<List<GetVacationDTO>>();        
            try
            {
                Vacation vacation = vacations.First(c => c.Id == id);
                vacations.Remove(vacation);
                serviceResponse.Data = vacations.Select(c => _mapper.Map<GetVacationDTO>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
