using Application.DTOs.BookingDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }
        public async Task<Booking> AddAsync(AddBookingDTO bookingDTO)
        {
            Booking booking = _mapper.Map<Booking>(bookingDTO);
            booking.Id = Guid.NewGuid();
            await _bookingRepository.AddAsync(booking);
            return booking;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _bookingRepository.GetAllAsync();
        }

        public async Task<Booking> GetByIdAsync(Guid Id)
        {
            return await _bookingRepository.GetByIdAsync(Id);
        }

        public async Task<bool> RemoveAsync(Booking booking)
        {
            if (await _bookingRepository.GetByIdAsync(booking.Id) == null)
                return false;

            await _bookingRepository.RemoveAsync(booking);
            return true;
        }

        public async Task<IEnumerable<Booking>> SearchByUserIdAsync(Guid UserId)
        {
            if (UserId == Guid.Empty)
                return null; 

            return await _bookingRepository.SearchByUserIdAsync(c => c.UserId.Contains(UserId));
        }

        public async Task<Booking> UpdateAsync(Booking booking)
        {
            if (_bookingRepository.GetByIdAsync(booking.Id) == null)
                return null;

            await _bookingRepository.UpdateAsync(booking);
            return booking;
        }
    }
}
