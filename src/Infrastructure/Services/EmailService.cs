using Application.DTOs.BookingDTO;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILoggerManager _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceProvider _serviceProvider;

        public EmailService(ILoggerManager logger, IUnitOfWork unitOfWork, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _serviceProvider = serviceProvider;
        }

        public async Task SendBaseEmailAsync(AddBookingDTO newBooking)
        {
            try
            {
                var appUser = await _unitOfWork.AppUsers.GetByIdAsync(newBooking.UserId);
                var parkingPlace = await _unitOfWork.ParkingPlaces.GetByIdAsync(newBooking.ParkingPlaceId.Value);
                var workPlace = await _unitOfWork.WorkPlaces.GetByIdAsync(newBooking.WorkPlaceId);
                var map = await _unitOfWork.Maps.GetByIdAsync(workPlace.MapId);
                var office = await _unitOfWork.Offices.GetByIdAsync(map.OfficeId);

                StringBuilder template = new();
                template.AppendLine($"Dear {appUser.FirstName} , your booking register on {workPlace.Number} ");
                template.AppendLine($"Booking summary:\n" +
                        $"Office Address: {office.Country}, {office.City}, {office.Address}\n" +
                          $"Floor: {map.Floor}\n" +
                          $"Workplace number: {workPlace.Number}\n" +
                          $"Booking date: {newBooking.BookingStart.Year}.{newBooking.BookingStart.Month}.{newBooking.BookingStart.Day}");

                if (newBooking.BookingStart != newBooking.BookingEnd)
                {
                    template.AppendLine($" - {newBooking.BookingEnd.Year}.{newBooking.BookingEnd.Month}.{newBooking.BookingEnd.Day}\n");
                }
                else
                {
                    template.AppendLine("\n");
                }
                if (newBooking.IsRecurring)
                {
                    template.AppendLine($"Frequency: {newBooking.Frequancy}\n");
                }
                if (parkingPlace != null)
                {
                    template.AppendLine($"Parking place number {parkingPlace.Number}");
                }
                template.AppendLine("- The Team 4");

                if (appUser != null)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var mailer = scope.ServiceProvider.GetRequiredService<IFluentEmail>();
                        var email = mailer
                        .To(appUser.Email, appUser.FirstName)
                        .Subject("BookingApp")
                        .Body(template.ToString());

                        _logger.LogWarn($"Sending email to {appUser.Email} from {mailer.SetFrom} with subject {mailer.Subject}.");
                        await email.SendAsync();
                    }
                }     
            }
            catch (Exception ex)
            {
                _logger.LogError($"BookingApp EmailService: Unhandled Exception {ex} for {nameof(SendBaseEmailAsync)}");
            }
        }
    }
    
}
