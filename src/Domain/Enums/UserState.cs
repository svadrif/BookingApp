using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public class UserState
    {
        SelectingAction,

        // Booking creating process states
        SelectingCountry,
        SelectingCity,
        SelectingOffice,
        SelectingBookingType,
        SelectingBookingDate,
        SelectingFloor,
        SelectingWorkPlace,
        SelectingParkingPlace,

        // Booking setting states
        SettingShortTermBooking,
        SettingLongTermBooking,
        SettingRecurringBooking,

    
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
