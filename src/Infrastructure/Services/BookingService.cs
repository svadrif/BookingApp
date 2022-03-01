using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IAppUserService _appUserService;
        private readonly IWorkPlaceService _workPlaceService;
        private readonly IParkingPlaceService _parkingPlaceService;
        public BookingService(IBookingRepository bookingRepository, IAppUserService appUserService, IWorkPlaceService workPlaceService, IParkingPlaceService parkingPlaceService)
        {
            _bookingRepository = bookingRepository;
            _appUserService = appUserService;
            _workPlaceService = workPlaceService;
            _parkingPlaceService = parkingPlaceService;
        }
        public async Task<Booking> AddAsync(Booking booking)
        {
            if (_bookingRepository.SearchAsync(b => b.Id == booking.Id).Result.Any())
                return null;

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
            var appUsers = await _appUserService.SearchAsync(booking.UserId);
            if (appUsers.Any()) return false;

            var workPlaces = await _workPlaceService.SearchAsync(booking.WorkPlaceId);
            if (workPlaces.Any()) return false;

            var parkingPlaces = await _parkingPlaceService.SearchAsync(booking.ParkingPlaceId);
            if (parkingPlaces.Any()) return false;

            await _bookingRepository.Remove(booking);
            return true;
        }

        public async Task<IEnumerable<Booking>> SearchAsync(Guid UserId)
        {
            return await _bookingRepository.SearchAsync(c => c.UserId.Contains(UserId));
        }

        public async Task<Booking> UpdateAsync(Booking booking)
        {
            if (_bookingRepository.SearchAsync(b => b.UserId == booking.UserId &&
                                               b.WorkPlaceId == booking.WorkPlaceId &&
                                               b.ParkingPlaceId == booking.ParkingPlaceId &&
                                               b.Id != booking.Id).Result.Any())
                return null;

            await _bookingRepository.UpdateAsync(booking);
            return booking;
        }
    }
}
