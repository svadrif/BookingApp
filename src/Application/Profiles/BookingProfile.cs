using Application.DTOs.BookingDTO;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, GetBookingDTO>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.BookingStart, src => src.MapFrom(x => x.BookingStart))
                .ForMember(dest => dest.BookingEnd, src => src.MapFrom(x => x.BookingEnd))
                .ForMember(dest => dest.IsRecurring, src => src.MapFrom(x => x.IsRecurring))
                .ForMember(dest => dest.Frequancy, src => src.MapFrom(x => x.Frequancy))
                .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.UserId))
                .ForMember(dest => dest.ParkingPlaceId, src => src.MapFrom(x => x.ParkingPlaceId))
                .ForMember(dest => dest.WorkPlaceId, src => src.MapFrom(x => x.WorkPlaceId));  
            
            CreateMap<AddBookingDTO, Booking>()
                .ForMember(dest => dest.BookingStart, src => src.MapFrom(x => x.BookingStart))
                .ForMember(dest => dest.BookingEnd, src => src.MapFrom(x => x.BookingEnd))
                .ForMember(dest => dest.IsRecurring, src => src.MapFrom(x => x.IsRecurring))
                .ForMember(dest => dest.Frequancy, src => src.MapFrom(x => x.Frequancy))
                .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.UserId))
                .ForMember(dest => dest.ParkingPlaceId, src => src.MapFrom(x => x.ParkingPlaceId))
                .ForMember(dest => dest.WorkPlaceId, src => src.MapFrom(x => x.WorkPlaceId));

            CreateMap<UpdateBookingDTO, Booking>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.BookingStart, src => src.MapFrom(x => x.BookingStart))
                .ForMember(dest => dest.BookingEnd, src => src.MapFrom(x => x.BookingEnd))
                .ForMember(dest => dest.IsRecurring, src => src.MapFrom(x => x.IsRecurring))
                .ForMember(dest => dest.Frequancy, src => src.MapFrom(x => x.Frequancy))
                .ForMember(dest => dest.IsActive, src => src.MapFrom(x => x.IsActive))
                .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.UserId))
                .ForMember(dest => dest.ParkingPlaceId, src => src.MapFrom(x => x.ParkingPlaceId))
                .ForMember(dest => dest.WorkPlaceId, src => src.MapFrom(x => x.WorkPlaceId));

        }
    }
}
