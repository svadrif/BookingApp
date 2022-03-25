using Application.DTOs.BookingDTO;
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
        Task SendBaseEmailAsync(AddBookingDTO newBooking);
    }
}
