using Application.DTOs.WorkPlaceDTO;
using Domain.Entities;
using AutoMapper;

namespace Application.Profiles
{
    public class WorkPlaceProfile : Profile
    {
        public WorkPlaceProfile()
        {
            CreateMap<WorkPlace, GetWorkPlaceDTO>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.MapId, src => src.MapFrom(x => x.MapId))
                .ForMember(dest => dest.Number, src => src.MapFrom(x => x.Number))
                .ForMember(dest => dest.Type, src => src.MapFrom(x => x.Type))
                .ForMember(dest => dest.NextToWindow, src => src.MapFrom(x => x.NextToWindow))
                .ForMember(dest => dest.HasPC, src => src.MapFrom(x => x.HasPC))
                .ForMember(dest => dest.HasMonitor, src => src.MapFrom(x => x.HasMonitor))
                .ForMember(dest => dest.HasKeyboard, src => src.MapFrom(x => x.HasKeyboard))
                .ForMember(dest => dest.HasMouse, src => src.MapFrom(x => x.HasMouse))
                .ForMember(dest => dest.HasHeadset, src => src.MapFrom(x => x.HasHeadset));


            CreateMap<AddWorkPlaceDTO, WorkPlace>()
                .ForMember(dest => dest.MapId, src => src.MapFrom(x => x.MapId))
                .ForMember(dest => dest.Number, src => src.MapFrom(x => x.Number))
                .ForMember(dest => dest.Type, src => src.MapFrom(x => x.Type))
                .ForMember(dest => dest.NextToWindow, src => src.MapFrom(x => x.NextToWindow))
                .ForMember(dest => dest.HasPC, src => src.MapFrom(x => x.HasPC))
                .ForMember(dest => dest.HasMonitor, src => src.MapFrom(x => x.HasMonitor))
                .ForMember(dest => dest.HasKeyboard, src => src.MapFrom(x => x.HasKeyboard))
                .ForMember(dest => dest.HasMouse, src => src.MapFrom(x => x.HasMouse))
                .ForMember(dest => dest.HasHeadset, src => src.MapFrom(x => x.HasHeadset));

            CreateMap<UpdateWorkPlaceDTO, WorkPlace>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.MapId, src => src.MapFrom(x => x.MapId))
                .ForMember(dest => dest.Number, src => src.MapFrom(x => x.Number))
                .ForMember(dest => dest.Type, src => src.MapFrom(x => x.Type))
                .ForMember(dest => dest.NextToWindow, src => src.MapFrom(x => x.NextToWindow))
                .ForMember(dest => dest.HasPC, src => src.MapFrom(x => x.HasPC))
                .ForMember(dest => dest.HasMonitor, src => src.MapFrom(x => x.HasMonitor))
                .ForMember(dest => dest.HasKeyboard, src => src.MapFrom(x => x.HasKeyboard))
                .ForMember(dest => dest.HasMouse, src => src.MapFrom(x => x.HasMouse))
                .ForMember(dest => dest.HasHeadset, src => src.MapFrom(x => x.HasHeadset))
                .ForMember(dest => dest.IsBlocked, src => src.MapFrom(x => x.IsBlocked));
        }
    }
}
