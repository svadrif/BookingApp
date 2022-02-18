using Application.DTOs.MapDTO;
using Domain.Entities;

namespace Infrastructure.Services.Map;

public class MapService: IMapService
{
    public Task<ServiceResponse<List<GetMapDTO>>> GetAllMaps()
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<List<GetMapDTO>>> UpdateMap(AddMapDTO map, Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<GetMapDTO>> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<List<GetMapDTO>>> AddMap(AddMapDTO map)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<List<GetMapDTO>>> DeleteMap(Guid id)
    {
        throw new NotImplementedException();
    }
}