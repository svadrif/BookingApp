using Application.DTOs.BookingDTO;
using Application.Interfaces;
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

        public async Task<IEnumerable<GetBookingDTO>> GetAllAsync()
        {
            var bookings = await _unitOfWork.Bookings.GetAllAsync();
            return _mapper.Map<IEnumerable<GetBookingDTO>>(bookings);
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

        public async Task<IEnumerable<GetBookingDTO>> SearchByUserIdAsync(Guid UserId)
        {
            var bookings = await _unitOfWork.Bookings.Search(c => c.UserId.Contains(UserId));
            if (bookings = null)
                return null;

            return _mapper.Map<IEnumerable<GetBookingDTO>>(bookings);
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
