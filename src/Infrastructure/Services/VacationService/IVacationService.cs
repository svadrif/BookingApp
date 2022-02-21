using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common;
using Application.Dtos.Vacation;

namespace Infrastructure.Services.VacationService
{
    public interface IVacationService
    {
        Task<ServiceResponse<List<GetVacationDTO>>> GetAllVacations();
        Task<ServiceResponse<GetVacationDTO>> GetVacationById(Guid Id);
        Task<ServiceResponse<List<GetVacationDTO>>> AddVacation(AddVacationDTO newVacation);
        Task<ServiceResponse<GetVacationDTO>> UpdateVacation(UpdateVacationDTO updatedVacation);
        Task<ServiceResponse<List<GetVacationDTO>>> DeleteVacation(Guid Id);

    }
}
