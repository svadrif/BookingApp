using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class VacationProfile : Profile
    {
        public VacationProfile()
        {
            CreateMap<Vacation, GetVacationDTO>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.UserId))
                .ForMember(dest => dest.VacationStart, src => src.MapFrom(x => x.VacationStart))
                .ForMember(dest => dest.VacationEnd, src => src.MapFrom(x => x.VacationEnd)); 

            CreateMap<AddVacationDTO, Vacation>()
                .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.UserId))
                .ForMember(dest => dest.VacationStart, src => src.MapFrom(x => x.VacationStart))
                .ForMember(dest => dest.VacationEnd, src => src.MapFrom(x => x.VacationEnd));

            CreateMap<UpdateVacationDTO, Vacation>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.UserId))
                .ForMember(dest => dest.VacationStart, src => src.MapFrom(x => x.VacationStart))
                .ForMember(dest => dest.VacationEnd, src => src.MapFrom(x => x.VacationEnd));

        }
    }
}
