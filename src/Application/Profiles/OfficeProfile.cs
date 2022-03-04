using Application.DTOs.OfficeDTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class OfficeProfile : Profile
    {
        public OfficeProfile()
        {
            CreateMap<Office, GetOfficeDTO>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Country, src => src.MapFrom(x => x.Country))
                .ForMember(dest => dest.City, src => src.MapFrom(x => x.City))
                .ForMember(dest => dest.Address, src => src.MapFrom(x => x.Address))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name));

            CreateMap<AddOfficeDTO, Office>()
                .ForMember(dest => dest.Country, src => src.MapFrom(x => x.Country))
                .ForMember(dest => dest.City, src => src.MapFrom(x => x.City))
                .ForMember(dest => dest.Address, src => src.MapFrom(x => x.Address))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name));

            CreateMap<UpdateOfficeDTO, Office>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Country, src => src.MapFrom(x => x.Country))
                .ForMember(dest => dest.City, src => src.MapFrom(x => x.City))
                .ForMember(dest => dest.Address, src => src.MapFrom(x => x.Address))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name));
        }
    }
}
