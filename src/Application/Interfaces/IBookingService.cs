using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBookingService 
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking> GetByIdAsync(Guid Id);
        Task<Booking> AddAsync(Booking booking);
        Task<Booking> UpdateAsync(Booking booking);
        Task<bool> RemoveAsync(Booking booking);
        Task<IEnumerable<Booking>> SearchAsync(Guid UserId);
    }
}
