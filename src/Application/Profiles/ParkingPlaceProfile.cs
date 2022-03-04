using Application.DTOs.ParkingPlaceDTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class ParkingPlaceProfile : Profile
    {
        public ParkingPlaceProfile()
        {
            CreateMap<ParkingPlace, GetParkingPlaceDTO>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Number, src => src.MapFrom(x => x.Number))
                .ForMember(dest => dest.OfficeId, src => src.MapFrom(x => x.OfficeId));

            CreateMap<AddParkingPlaceDTO, ParkingPlace>()
                .ForMember(dest => dest.Number, src => src.MapFrom(x => x.Number))
                .ForMember(dest => dest.OfficeId, src => src.MapFrom(x => x.OfficeId));

            CreateMap<UpdateParkingPlaceDTO, ParkingPlace>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Number, src => src.MapFrom(x => x.Number))
                .ForMember(dest => dest.OfficeId, src => src.MapFrom(x => x.OfficeId));
        }
    }
}
