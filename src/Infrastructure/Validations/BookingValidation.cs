using Domain.Entities;

namespace Infrastructure.Validations;

public static class BookingValidation
{
    public static bool Validate(Booking booking)
    {
        if (DateTimeOffset.Compare(booking.BookingStart.AddSeconds(50),DateTimeOffset.UtcNow) < 0)
        {
            return false;
        }
        if (DateTimeOffset.Compare(booking.BookingEnd.Date,DateTimeOffset.UtcNow.AddMonths(3).Date) > 0)
        {
            return false;       
        }
        if (DateTimeOffset.Compare(booking.BookingEnd.Date, booking.BookingStart.Date) < 0)
        {
            return false;
        }
        return true;
    }

    public static bool ValidateBookingDate(Booking booking,DateTimeOffset startDb,DateTimeOffset endDb)
    {
        
        if (DateTimeOffset.Compare(booking.BookingStart.Date, startDb.Date) == 0)
        {
            return false;
        }
        
        if(DateTimeOffset.Compare(booking.BookingEnd.Date,endDb.Date) == 0)
        {
            return false;
        }
        return true;
    }

    public static bool ValidateOnVacation(Booking booking,Vacation vacation)
    {
        var vacationEnd = vacation.VacationEnd??default;
        var vacationStart = vacation.VacationStart??default;
        if (DateTimeOffset.Compare(booking.BookingStart.Date, vacationStart.Date) > 0)
        {
            return false;
        }
        if(DateTimeOffset.Compare(booking.BookingStart.Date,vacationStart.Date) < 0 && (DateTimeOffset.Compare(booking.BookingEnd.Date, vacationStart.Date) > 0 ))
        {
            return false;
        }
        if(DateTimeOffset.Compare(booking.BookingStart.Date, vacationStart.Date) > 0 && (DateTimeOffset.Compare(booking.BookingStart.Date, vacationEnd.Date) < 0))
        {
            return false;
        }
        return true;
    }
}