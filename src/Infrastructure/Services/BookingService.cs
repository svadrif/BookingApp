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
        public async Task<Booking> Add(Booking booking)
        {
            if (_bookingRepository.Search(b => b.Id == booking.Id).Result.Any())
                return null;

            await _bookingRepository.Add(booking);
            return booking;
        }

        public void Dispose()
        {
            _bookingRepository?.Dispose();
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            return await _bookingRepository.GetAll();
        }

        public async Task<Booking> GetById(Guid Id)
        {
            return await _bookingRepository.GetById(Id);
        }

        public async Task<bool> Remove(Booking booking)
        {
            var appUsers = await _appUserService.Search(booking.UserId);
            if (appUsers.Any()) return false;

            var workPlaces = await _workPlaceService.Search(booking.WorkPlaceId);
            if (workPlaces.Any()) return false;

            var parkingPlaces = await _parkingPlaceService.Search(booking.ParkingPlaceId);
            if (parkingPlaces.Any()) return false;

            await _bookingRepository.Remove(booking);
            return true;
        }

        public async Task<IEnumerable<Booking>> Search(Guid UserId)
        {
            return await _bookingRepository.Search(c => c.UserId.Contains(UserId));
        }

        public async Task<Booking> Update(Booking booking)
        {
            if (_bookingRepository.Search(b => b.UserId == booking.UserId &&
                                               b.WorkPlaceId == booking.WorkPlaceId &&
                                               b.ParkingPlaceId == booking.ParkingPlaceId &&
                                               b.Id != booking.Id).Result.Any())
                return null;

            await _bookingRepository.Update(booking);
            return booking;
        }
    }
}
