using Application.DTOs.AppUserDTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser, GetAppUserDTO>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.TelegramId, src => src.MapFrom(x => x.TelegramId))
                .ForMember(dest => dest.UserName, src => src.MapFrom(x => x.UserName))
                .ForMember(dest => dest.FirstName, src => src.MapFrom(x => x.FirstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(x => x.LastName))
                .ForMember(dest => dest.TelephoneNumber, src => src.MapFrom(x => x.TelephoneNumber))
                .ForMember(dest => dest.Email, src => src.MapFrom(x => x.Email))
                .ForMember(dest => dest.Role, src => src.MapFrom(x => x.Role))
                .ForMember(dest => dest.EmploymentStart, src => src.MapFrom(x => x.EmploymentStart))
                .ForMember(dest => dest.EmploymentEnd, src => src.MapFrom(x => x.EmploymentEnd))
                .ForMember(dest => dest.PrefferdWorkPlaceId, src => src.MapFrom(x => x.PrefferdWorkPlaceId));

            CreateMap<AddAppUserDTO, AppUser>()
                .ForMember(dest => dest.TelegramId, src => src.MapFrom(x => x.TelegramId))
                .ForMember(dest => dest.UserName, src => src.MapFrom(x => x.UserName))
                .ForMember(dest => dest.FirstName, src => src.MapFrom(x => x.FirstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(x => x.LastName))
                .ForMember(dest => dest.TelephoneNumber, src => src.MapFrom(x => x.TelephoneNumber))
                .ForMember(dest => dest.Email, src => src.MapFrom(x => x.Email))
                .ForMember(dest => dest.Role, src => src.MapFrom(x => x.Role))
                .ForMember(dest => dest.EmploymentStart, src => src.MapFrom(x => x.EmploymentStart))
                .ForMember(dest => dest.EmploymentEnd, src => src.MapFrom(x => x.EmploymentEnd))
                .ForMember(dest => dest.PrefferdWorkPlaceId, src => src.MapFrom(x => x.PrefferdWorkPlaceId));

            CreateMap<UpdateAppUserDTO, AppUser>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.TelegramId, src => src.MapFrom(x => x.TelegramId))
                .ForMember(dest => dest.UserName, src => src.MapFrom(x => x.UserName))
                .ForMember(dest => dest.FirstName, src => src.MapFrom(x => x.FirstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(x => x.LastName))
                .ForMember(dest => dest.TelephoneNumber, src => src.MapFrom(x => x.TelephoneNumber))
                .ForMember(dest => dest.Email, src => src.MapFrom(x => x.Email))
                .ForMember(dest => dest.Role, src => src.MapFrom(x => x.Role))
                .ForMember(dest => dest.EmploymentStart, src => src.MapFrom(x => x.EmploymentStart))
                .ForMember(dest => dest.EmploymentEnd, src => src.MapFrom(x => x.EmploymentEnd))
                .ForMember(dest => dest.PrefferdWorkPlaceId, src => src.MapFrom(x => x.PrefferdWorkPlaceId))
                .ForMember(dest => dest.IsDeleted, src => src.MapFrom(x => x.IsDeleted));
        }
    }
}
