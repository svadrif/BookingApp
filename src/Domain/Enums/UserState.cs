using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public enum UserState
    {
        NotAuthorized,
        SelectingAction,

        // Booking creating process states
        SelectingCountry,
        SelectingCity,
        SelectingOffice,
        SelectingBookingType,
        SelectingBookingRecurringType,
        SelectingFloor,
        SelectingWorkPlace,
        SelectingParkingPlace,

        // Booking setting date states
            // One-day
        SelectingBookingDate,
            // Continuous
        SelectingBookingStartDate,
        SelectingBookingEndDate,
            // Recurring
        SelectingRecurringDays,


        ReviewingMyBookings,

        // Booking editing process states
        EditingBookingType,
        EditingBookingDate,
        EditingWorkPlace,
        EditingFloor,
        EditingOffice,
        EditingParkingPlace,
    }
}
