using Application.DTOs.MapDTO;
using Domain.Entities;

namespace Infrastructure.Services.Map;

public interface IMapService
{
    Task<ServiceResponse<List<GetMapDTO>>> GetAllMaps();
    Task<ServiceResponse<List<GetMapDTO>>> UpdateMap(AddMapDTO map,Guid id);
    Task<ServiceResponse<GetMapDTO>> GetById(Guid id);
    Task<ServiceResponse<List<GetMapDTO>>>  AddMap(AddMapDTO map);
    Task<ServiceResponse<List<GetMapDTO>>> DeleteMap(Guid id);
}