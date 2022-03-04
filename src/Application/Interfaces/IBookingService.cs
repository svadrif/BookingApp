using Application.DTOs.BookingDTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBookingService 
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking> GetByIdAsync(Guid Id);
        Task<Booking> AddAsync(AddBookingDTO bookingDTO);
        Task<Booking> UpdateAsync(Booking booking);
        Task<bool> RemoveAsync(Booking booking);
        Task<IEnumerable<Booking>> SearchByUserIdAsync(Guid UserId);
    }
}
