using Application.Common;
using Application.DTOs.OfficeDTO;

namespace Infrastructure.Services.OfficeService
{
    public interface IOfficeService
    {
        Task<ServiceResponse<List<GetOfficeDTO>>> GetAllOffices();
        Task<ServiceResponse<List<GetOfficeDTO>>> UpdateOffice(AddOfficeDTO office,Guid id);
        Task<ServiceResponse<GetOfficeDTO>> GetById(Guid id);
        Task<ServiceResponse<List<GetOfficeDTO>>>  AddOffice(AddOfficeDTO office2Add);
        Task<ServiceResponse<List<GetOfficeDTO>>> DeleteOffice(Guid id);

    }
}
