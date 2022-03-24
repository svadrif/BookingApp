using Application.DTOs.BookingDTO;
using Application.Interfaces;
using Application.Sender;
using FluentEmail.Core;
using FluentEmail.Smtp;
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

        public EmailService(ILoggerManager logger)
        {
            _logger = logger;
        }

        public async Task SendAsync(AddBookingDTO newBooking, IFluentEmail mailer)
        {

            Email.DefaultSender = emailSender;

            var email = await Email
                .From(request.FromMail)
                .To(request.GetAppUserDTO.Email, request.GetAppUserDTO.UserName)
                .Subject(request.Subject)
                .Body(request.Body);        

            foreach (string to in request.ToMail)
            {
                email.To.Add(new MailAddress(to));
            }

            //TODO:EmailService if there was error, try at least three times. 
            try
            {
                await emailSender.SendAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"BookingApp EmailService: Unhandled Exception {ex} for Request {request}");
            }

            _logger.LogWarn($"Sending email to {request.ToMail} from {request.FromMail} with subject {request.Subject}.");

        }
    }
}
