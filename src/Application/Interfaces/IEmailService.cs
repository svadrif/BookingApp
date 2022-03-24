using Application.DTOs.BookingDTO;
using Application.Sender;
using FluentEmail.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(AddBookingDTO newBooking, IFluentEmail mailer);
    }
}
