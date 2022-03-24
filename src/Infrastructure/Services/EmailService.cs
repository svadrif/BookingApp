using Application.DTOs.BookingDTO;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Sender;
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

                if (appUser != null)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var mailer = scope.ServiceProvider.GetRequiredService<IFluentEmail>();
                        var email = mailer
                        .To(appUser.Email, appUser.FirstName)
                        .Subject("BookingApp")
                        .Body("<b>Test Mail</b><br>using <b>HTML</b>");

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

        public Task SendHtmlEmailAsync(AddBookingDTO newBooking)
        {
            try
            {
                var appUser = await _unitOfWork.AppUsers.GetByIdAsync(newBooking.UserId);

                if (appUser != null)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var mailer = scope.ServiceProvider.GetRequiredService<IFluentEmail>();
                        var email = mailer
                        .To(appUser.Email, appUser.FirstName)
                        .Subject("BookingApp")
                        .Body("<b>Test Mail</b><br>using <b>HTML</b>");

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
