using Domain.Entities;

namespace Infrastructure.Validations;

public class BookingValidation
{
    public static bool Validate(Booking booking,DateTimeOffset startDb,DateTimeOffset endDb)
    {
        if (DateTimeOffset.Compare(booking.BookingStart,DateTimeOffset.UtcNow) > 0)
        {
            return false;
        }
        //booking should be less than 3 months
        if (DateTimeOffset.Compare(booking.BookingEnd,DateTimeOffset.UtcNow.AddMonths(3)) > 0)
        {
            return false;       
        }
        //already exists
        if (DateTimeOffset.Compare(booking.BookingStart, startDb) == 0)
        {
            return false;
        }
        if(DateTimeOffset.Compare(booking.BookingEnd,endDb) == 0)
        {
            return false;
        }
        if (DateTimeOffset.Compare(booking.BookingEnd, booking.BookingStart) > 0)
        {
            return false;
        }
        
        return true;
    }
}