using Application.DTOs.BookingDTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBookingService 
    {
        Task<IEnumerable<GetBookingDTO>> GetAllAsync();
        Task<GetBookingDTO> GetByIdAsync(Guid Id);
        Task<Guid> AddAsync(AddBookingDTO bookingDTO);
        Task<GetBookingDTO> UpdateAsync(UpdateBookingDTO bookingDTO);
        Task<bool> RemoveAsync(Guid Id);
        Task<IEnumerable<GetBookingDTO>> SearchByUserIdAsync(Guid UserId);
    }
}
