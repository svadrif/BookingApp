using Application.DTOs.BookingDTO;
using Application.Interfaces.IServices;
using Domain.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Commands;

namespace TelegramBot.Handlers
{
    public class CallbackQueryHandler
    {
        public static async Task HandleAsync(
            CallbackQuery callback,
            ITelegramBotClient botClient,
            IAppUserService userService,
            IStateService stateService,
            IBookingHistoryService historyService,
            IOfficeService officeService,
            IMapService mapService,
            IWorkPlaceService workPlaceService,
            IBookingService bookingService,
            IParkingPlaceService parkingPlaceService)
        {
            if (callback.Data.Equals("."))
            {
                await botClient.AnswerCallbackQueryAsync(callback.Id, "Unavailable button");
                return;
            }

            var user = await userService.GetByTelegramIdAsync(callback.From.Id);
            var state = await stateService.GetByUserIdAsync(user.Id);
            var history = await historyService.GetByUserIdAsync(user.Id);
            UserState backState;
            string backCommand;
            AddBookingDTO booking;

            if (callback.Data.Length > 5 && callback.Data.Substring(0, 5).Equals("back:"))
            {
                state.StateNumber = (UserState)Enum.Parse(typeof(UserState), callback.Data.Split(":")[1]);

                var data = callback.Data.Split(":")[2];
                for (var counter = 3; counter < callback.Data.Split(":").Count(); counter++)
                {
                    data += $":{callback.Data.Split(":")[counter]}";
                }
                callback.Data = data;
            }

            switch (state.StateNumber)
            {
                case UserState.NotAuthorized:
                    #region NotAuthorized
                    await StartCommand.ExecuteAsync(callback, botClient);

                    state.StateNumber = UserState.SelectingAction;
                    await stateService.UpdateAsync(state);
                    return;
                #endregion

                case UserState.SelectingAction:
                    #region SelectingAction
                    switch (callback.Data)
                    {
                        case "New Booking":
                            await SendCountriesCommand.ExecuteAsync(callback, botClient, officeService);

                            state.LastCommand = callback.Data;
                            state.StateNumber = UserState.SelectingCountry;
                            await stateService.UpdateAsync(state);
                            return;

                        case "My Bookings":
                            await SendBookingsCommand.ExecuteAsync(callback, botClient, bookingService, workPlaceService, mapService, officeService, user.Id);

                            state.LastCommand = callback.Data;
                            state.StateNumber = UserState.ReviewingMyBookings;
                            await stateService.UpdateAsync(state);
                            return;
                    }
                    return;
                #endregion

                    // New Bookimg
                case UserState.SelectingCountry:
                    #region SelectingCountry
                    await SendCitiesCommand.ExecuteAsync(callback, botClient, officeService);

                    state.LastCommand = callback.Data;
                    state.StateNumber = UserState.SelectingCity;
                    await stateService.UpdateAsync(state);

                    history.Country = callback.Data;
                    await historyService.UpdateAsync(history);
                    return;
                #endregion

                case UserState.SelectingCity:
                    #region SelectingCity
                    await SendOfficesCommand.ExecuteAsync(callback, botClient, officeService, history.Country);

                    state.LastCommand = callback.Data;
                    state.StateNumber = UserState.SelectingOffice;
                    await stateService.UpdateAsync(state);

                    history.City = callback.Data;
                    await historyService.UpdateAsync(history);
                    return;
                #endregion

                case UserState.SelectingOffice:
                    #region SelectingOffice
                    await SelectBookingTypeCommand.ExecuteAsync(callback, botClient, history.City);

                    state.LastCommand = callback.Data;
                    state.StateNumber = UserState.SelectingBookingType;
                    await stateService.UpdateAsync(state);

                    history.OfficeId = Guid.Parse(callback.Data);
                    await historyService.UpdateAsync(history);
                    return;
                #endregion

                case UserState.SelectingBookingType:
                    #region SelectingBookingType
                    switch (callback.Data)
                    {
                        case "One-day":
                            await SendDateCommand.ExecuteAsync(callback, botClient, DateTime.Now.Date.AddDays(1), 0, "one-day", UserState.SelectingOffice, history.OfficeId.ToString());

                            state.LastCommand = callback.Data;
                            state.StateNumber = UserState.SelectingBookingDate;
                            await stateService.UpdateAsync(state);
                            return;

                        case "Continuous":
                            await SendDateCommand.ExecuteAsync(callback, botClient, DateTime.Now.Date.AddDays(1), 0, "start", UserState.SelectingOffice, history.OfficeId.ToString());

                            state.LastCommand = callback.Data;
                            state.StateNumber = UserState.SelectingBookingStartDate;
                            await stateService.UpdateAsync(state);
                            return;
                    }
                    return;
                #endregion

                case UserState.SelectingBookingDate:
                    #region SelectingBookingDate
                    if (callback.Data.Length > 6 && callback.Data.Substring(0, 6).Equals("month:"))
                    {
                        var skipMonths = int.Parse(callback.Data.Split(":")[1]);
                        if (skipMonths < 0 || skipMonths > 3)
                        {
                            await botClient.AnswerCallbackQueryAsync(callback.Id, "Unavailable button");
                            return;
                        }
                        await SendDateCommand.ExecuteAsync(callback, botClient, DateTime.Now.Date.AddDays(1), skipMonths, "one-day", UserState.SelectingOffice, history.OfficeId.ToString());
                        return;
                    }

                    await SelectParkingPlaceCommand.ExecuteAsync(callback, botClient, UserState.SelectingBookingType, "One-day");

                    state.LastCommand = callback.Data;
                    state.StateNumber = UserState.SelectingParkingPlace;
                    await stateService.UpdateAsync(state);

                    history.BookingStart = DateTime.Parse(callback.Data);
                    history.BookingEnd = DateTime.Parse(callback.Data);
                    history.IsRecurring = false;
                    history.Frequancy = string.Empty;
                    await historyService.UpdateAsync(history);
                    return;
                #endregion

                case UserState.SelectingBookingStartDate:
                    #region SelectingBookingStartDate
                    if (callback.Data.Length > 6 && callback.Data.Substring(0, 6).Equals("month:"))
                    {
                        var skipMonths = int.Parse(callback.Data.Split(":")[1]);
                        if (skipMonths < 0 || skipMonths > 3)
                        {
                            await botClient.AnswerCallbackQueryAsync(callback.Id, "Unavailable button");
                            return;
                        }
                        await SendDateCommand.ExecuteAsync(callback, botClient, DateTime.Now.Date.AddDays(1), skipMonths, "start", UserState.SelectingOffice, history.OfficeId.ToString());
                        return;
                    }
                    await SendDateCommand.ExecuteAsync(callback, botClient, DateTime.Parse(callback.Data).AddDays(1), 0, "end", UserState.SelectingBookingType, "Continuous");

                    state.LastCommand = callback.Data;
                    state.StateNumber = UserState.SelectingBookingEndDate;
                    await stateService.UpdateAsync(state);

                    history.BookingStart = DateTime.Parse(callback.Data);
                    await historyService.UpdateAsync(history);
                    return;
                #endregion

                case UserState.SelectingBookingEndDate:
                    #region SelectingBookingEndDate
                    if (callback.Data.Length > 6 && callback.Data.Substring(0, 6).Equals("month:"))
                    {
                        var skipMonths = int.Parse(callback.Data.Split(":")[1]) - (history.BookingStart.Value.Month - DateTime.Now.Month);
                        var monthDifference = DateTime.Now.AddMonths(3).Month - history.BookingStart.Value.Month;
                        if (skipMonths < 0 || skipMonths > (monthDifference < 0 ? monthDifference + 12 : monthDifference))
                        {
                            await botClient.AnswerCallbackQueryAsync(callback.Id, "Unavailable button");
                            return;
                        }
                        await SendDateCommand.ExecuteAsync(callback, botClient, history.BookingStart.Value.DateTime.AddDays(1), skipMonths, "end", UserState.SelectingBookingType, "Continuous");
                        return;
                    }

                    await SelectParkingPlaceCommand.ExecuteAsync(callback, botClient, UserState.SelectingBookingStartDate, history.BookingStart.ToString());

                    state.LastCommand = callback.Data;
                    state.StateNumber = UserState.SelectingParkingPlace;
                    await stateService.UpdateAsync(state);

                    history.BookingEnd = DateTime.Parse(callback.Data);
                    history.IsRecurring = false;
                    history.Frequancy = string.Empty;
                    await historyService.UpdateAsync(history);
                    return;
                #endregion

                case UserState.SelectingParkingPlace:
                    #region SelectingParkingPlace
                    backState = history.BookingStart == history.BookingEnd ? UserState.SelectingBookingDate : UserState.SelectingBookingEndDate;
                    switch (callback.Data)
                    {
                        case "Yes":
                            await botClient.AnswerCallbackQueryAsync(callback.Id, "Unavailable button");
                            return;

                        case "No":
                            await SelectSpecificWorkPlaceCommand.ExecuteAsync(callback, botClient, backState, history.BookingEnd.ToString());

                            state.StateNumber = UserState.SelectingSpecificWorkPlace;
                            state.LastCommand = callback.Data;
                            await stateService.UpdateAsync(state);

                            history.ParkingPlaceId = null;
                            await historyService.UpdateAsync(history);
                            return;
                    }
                    return;
                #endregion

                case UserState.SelectingSpecificWorkPlace:
                    #region SelectingSpecificWorkPlace
                    switch (callback.Data)
                    {
                        case "Yes":
                            await SelectExactFloorCommand.ExecuteAsync(callback, botClient, history.ParkingPlaceId == null ? "No" : "Yes");

                            state.StateNumber = UserState.SpecifyingWorkPlaceSelectingExactMap;
                            state.LastCommand = callback.Data;
                            await stateService.UpdateAsync(state);
                            return;

                        case "No":
                            await botClient.AnswerCallbackQueryAsync(callback.Id, "Unavailable button");
                            return;
                    }
                    return;
                #endregion

                case UserState.SpecifyingWorkPlaceSelectingExactMap:
                    #region SpecifyingWorkPlaceSelectingExactMap
                    switch (callback.Data)
                    {
                        case "Yes":
                            await SendFloorsCommand.ExecuteAsync(callback, botClient, mapService, history.OfficeId.Value);

                            state.StateNumber = UserState.SpecifyingWorkPlaceSelectingMapFloor;
                            state.LastCommand = callback.Data;
                            await stateService.UpdateAsync(state);
                            return;

                        case "No":
                            await botClient.AnswerCallbackQueryAsync(callback.Id, "Unavailable button");
                            return;
                    }
                    return;
                #endregion

                case UserState.SpecifyingWorkPlaceSelectingMapFloor:
                    #region SpecifyingWorkPlaceSelectingMapFloor
                    await SelectExactWorkPlaceCommand.ExecuteAsync(callback, botClient, UserState.SpecifyingWorkPlaceSelectingExactMap, "Yes");

                    state.StateNumber = UserState.SpecifyingWorkPlaceSelectingExactWorkPlace;
                    state.LastCommand = callback.Data;
                    await stateService.UpdateAsync(state);

                    history.HasConfRoom = null;
                    history.HasKitchen = null;
                    history.MapId = Guid.Parse(callback.Data);
                    history.Floor = mapService.GetByIdAsync(history.MapId.Value).Result.Floor;
                    await historyService.UpdateAsync(history);
                    return;
                #endregion

                case UserState.SpecifyingWorkPlaceSelectingExactWorkPlace:
                    #region SpecifyingWorkPlaceSelectingExactWorkPlace

                    backState = history.HasConfRoom == null ? UserState.SpecifyingWorkPlaceSelectingMapFloor : UserState.SpecifyingWorkPlaceSelectingMapAttributes;
                    backCommand = history.HasConfRoom == null ? history.MapId.Value.ToString() : "Confirm";
                    switch (callback.Data)
                    {
                        case "Yes":
                            await SendWorkPlacesCommand.ExecuteAsync(callback, botClient, workPlaceService, bookingService, history.MapId.Value,
                                                                     history.BookingStart.Value.DateTime, history.BookingEnd.Value.DateTime, backState, backCommand);

                            state.StateNumber = UserState.SpecifyingWorkPlaceSelectingWorkPlace;
                            state.LastCommand = callback.Data;
                            await stateService.UpdateAsync(state);
                            return;

                        case "No":
                            await botClient.AnswerCallbackQueryAsync(callback.Id, "Unavailable button");
                            return;
                    }
                    return;
                #endregion

                case UserState.SpecifyingWorkPlaceSelectingWorkPlace:
                    #region SpecifyingWorkPlaceSelectingWorkPlace
                    history.WorkPlaceId = Guid.Parse(callback.Data);

                    await SendBookingSummaryCommand.ExecuteAsync(callback, botClient, history, officeService, workPlaceService, parkingPlaceService,
                                                                 UserState.SpecifyingWorkPlaceSelectingExactWorkPlace, "Yes");

                    state.StateNumber = UserState.FinishingBooking;
                    state.LastCommand = callback.Data;
                    await stateService.UpdateAsync(state);

                    await historyService.UpdateAsync(history);
                    return;
                #endregion

                case UserState.FinishingBooking:
                    #region FinishingBooking
                    booking = new AddBookingDTO
                    {
                        UserId = history.UserId,
                        WorkPlaceId = history.WorkPlaceId.Value,
                        ParkingPlaceId = history.ParkingPlaceId,
                        BookingStart = history.BookingStart.Value,
                        BookingEnd = history.BookingEnd.Value,
                        IsRecurring = history.IsRecurring.Value,
                        Frequancy = history.Frequancy
                    };

                    switch (callback.Data)
                    {
                        case "Confirm":
                            await StartCommand.ExecuteAsync(callback, botClient);

                            await bookingService.AddAsync(booking);

                            state.StateNumber = UserState.SelectingAction;
                            state.LastCommand = callback.Data;
                            await stateService.UpdateAsync(state);
                            break;

                        case "Cancel":
                            await StartCommand.ExecuteAsync(callback, botClient);

                            state.StateNumber = UserState.SelectingAction;
                            state.LastCommand = callback.Data;
                            await stateService.UpdateAsync(state);
                            break;
                    }

                    history.Country = string.Empty;
                    history.City = string.Empty;
                    history.OfficeId = null;
                    history.Floor = null;
                    history.HasKitchen = null;
                    history.HasConfRoom = null;
                    history.MapId = null;
                    history.IsNextToWindow = null;
                    history.HasPC = null;
                    history.HasMonitor = null;
                    history.HasKeyboard = null;
                    history.HasMouse = null;
                    history.HasHeadset = null;
                    history.WorkPlaceId = null;
                    history.BookingStart = null;
                    history.BookingEnd = null;
                    history.IsRecurring = null;
                    history.Frequancy = string.Empty;
                    history.ParkingPlaceId = null;
                    await historyService.UpdateAsync(history);
                    return;
                #endregion

                    // My Bookings
                case UserState.ReviewingMyBookings:
                    #region ReviewingMyBookings
                    await SendBookingInfoCommand.ExecuteAsync(callback, botClient, bookingService, workPlaceService, mapService, officeService, parkingPlaceService, Guid.Parse(callback.Data));

                    state.LastCommand = callback.Data;
                    state.StateNumber = UserState.ReviewingBookingInfo;
                    await stateService.UpdateAsync(state);
                    return;
                #endregion

                case UserState.ReviewingBookingInfo:
                    #region ReviewingBookingInfo
                    switch(callback.Data.Split(":")[0])
                    {
                        case "Delete":
                            await StartCommand.ExecuteAsync(callback, botClient);

                            await bookingService.RemoveAsync(Guid.Parse(callback.Data.Split(":")[1]));

                            state.LastCommand = callback.Data;
                            state.StateNumber = UserState.SelectingAction;
                            await stateService.UpdateAsync(state);
                            return;

                        case "Edit":
                            await botClient.AnswerCallbackQueryAsync(callback.Id, "Unavailable button");
                            return;
                    }
                    return;
                    #endregion
            }
        }
    }
}
