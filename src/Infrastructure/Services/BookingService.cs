using Application.DTOs.BookingDTO;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Pagination;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(AddBookingDTO bookingDTO)
        {
            Booking booking = _mapper.Map<Booking>(bookingDTO);
            await _unitOfWork.Bookings.AddAsync(booking);
            await _unitOfWork.CompleteAsync();
            return booking.Id;
        }

        public async Task<PagedList<GetBookingDTO>> GetPagedAsync(PagedQueryBase query)
        {
            var bookings = await _unitOfWork.Bookings.GetPagedAsync(query);
            var mapBookings = _mapper.Map<List<GetBookingDTO>>(bookings);
            var bookingsDTO = new PagedList<GetBookingDTO>(mapBookings, bookings.TotalCount, bookings.CurrentPage, bookings.PageSize);
            return bookingsDTO;
        }

        public async Task<GetBookingDTO> GetByIdAsync(Guid Id)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(Id);
            return _mapper.Map<GetBookingDTO>(booking);
        }

        public async Task<bool> RemoveAsync(Guid Id)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(Id);
            if (booking == null)
                return false;

            _unitOfWork.Bookings.Remove(booking);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<PagedList<GetBookingDTO>> SearchByUserIdAsync(Guid userId, PagedQueryBase query)
        {
            var bookings = await _unitOfWork.Bookings.GetPagedByUserIdAsync(userId, query);
            var bookingMaps = _mapper.Map<List<GetBookingDTO>>(bookings);
            var bookingsDTO = new PagedList<GetBookingDTO>(bookingMaps, bookings.TotalCount, bookings.CurrentPage, bookings.PageSize);
            return bookingsDTO;
        }

        public async Task<PagedList<GetBookingDTO>> SearchByWorkPlaceIdAsync(Guid workPlaceId, PagedQueryBase query)
        {
            var bookings = await _unitOfWork.Bookings.GetPagedByWorkPlaceIdAsync(workPlaceId, query);
            var bookingMaps = _mapper.Map<List<GetBookingDTO>>(bookings);
            var bookingsDTO = new PagedList<GetBookingDTO>(bookingMaps, bookings.TotalCount, bookings.CurrentPage, bookings.PageSize);
            return bookingsDTO;
        }

        public async Task<GetBookingDTO> UpdateAsync(UpdateBookingDTO bookingDTO)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingDTO.Id);
            if (booking == null)
                return null;

            _mapper.Map(bookingDTO, booking);
            _unitOfWork.Bookings.Update(booking);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<GetBookingDTO>(booking);
        }
    }
}
