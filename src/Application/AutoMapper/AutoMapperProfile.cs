using Application.DTOs.OfficeDTO;
using AutoMapper;
using Domain.Entities;

namespace Application.AutoMapper;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Office, GetOfficeDTO>();
        CreateMap<Office, AddOfficeDTO>();

    }
}