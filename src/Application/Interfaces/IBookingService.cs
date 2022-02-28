using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBookingService : IDisposable
    {
        Task<IEnumerable<Booking>> GetAll();
        Task<Booking> GetById(Guid Id);
        Task<Booking> Add(Booking booking);
        Task<Booking> Update(Booking booking);
        Task<bool> Remove(Booking booking);
        Task<IEnumerable<Booking>> Search(Guid UserId);
    }
}
