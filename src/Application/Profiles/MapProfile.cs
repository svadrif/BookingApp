using Application.DTOs.MapDTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Map, GetMapDTO>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Floor, src => src.MapFrom(x => x.Floor))
                .ForMember(dest => dest.HasKitchen, src => src.MapFrom(x => x.HasKitchen))
                .ForMember(dest => dest.HasConfRoom, src => src.MapFrom(x => x.HasConfRoom))
                .ForMember(dest => dest.OfficeId, src => src.MapFrom(x => x.OfficeId));

            CreateMap<AddMapDTO, Map>()
                .ForMember(dest => dest.Floor, src => src.MapFrom(x => x.Floor))
                .ForMember(dest => dest.HasKitchen, src => src.MapFrom(x => x.HasKitchen))
                .ForMember(dest => dest.HasConfRoom, src => src.MapFrom(x => x.HasConfRoom))
                .ForMember(dest => dest.OfficeId, src => src.MapFrom(x => x.OfficeId));

            CreateMap<UpdateMapDTO, Map>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Floor, src => src.MapFrom(x => x.Floor))
                .ForMember(dest => dest.HasKitchen, src => src.MapFrom(x => x.HasKitchen))
                .ForMember(dest => dest.HasConfRoom, src => src.MapFrom(x => x.HasConfRoom))
                .ForMember(dest => dest.OfficeId, src => src.MapFrom(x => x.OfficeId));
        }
    }
}
