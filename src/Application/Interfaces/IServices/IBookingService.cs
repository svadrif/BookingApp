using Application.DTOs.BookingDTO;
using Application.Pagination;

namespace Application.Interfaces.IServices
{
    public interface IBookingService
    {
        Task<PagedList<GetBookingDTO>> GetPagedAsync(PagedQueryBase query);
        Task<GetBookingDTO> GetByIdAsync(Guid Id);
        Task<Guid> AddAsync(AddBookingDTO bookingDTO);
        Task<GetBookingDTO> UpdateAsync(UpdateBookingDTO bookingDTO);
        Task<bool> RemoveAsync(Guid Id);
        Task<IEnumerable<GetBookingDTO>> SearchByUserIdAsync(Guid UserId);
    }
}
