using Application.DTOs.OfficeDTO;
using Domain.Entities;
using Infrastructure.Services.OfficeService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private readonly IOfficeService _officeService;

        public OfficeController(IOfficeService officeService)
        {
            _officeService = officeService;
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetOfficeDTO>>>> Get()
        {
            return Ok(await _officeService.GetAllOffices());
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ServiceResponse<List<GetOfficeDTO>>>> Get(Guid id)
        {
            return Ok(await _officeService.GetById(id));
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetOfficeDTO>>>> Post(AddOfficeDTO office2Add)
        {
            return Ok(await _officeService.AddOffice(office2Add));
        }
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ServiceResponse<GetOfficeDTO>>> Put(AddOfficeDTO request, Guid id)
        {
            return Ok(await _officeService.UpdateOffice(request, id));
        }
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ServiceResponse<GetOfficeDTO>>> Delete(Guid id)
        {
            return Ok(await _officeService.DeleteOffice(id));
        }
        
    }
}
