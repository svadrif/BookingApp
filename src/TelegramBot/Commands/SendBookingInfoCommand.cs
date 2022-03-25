using Application.Interfaces.IServices;
using Domain.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Helpers;

namespace TelegramBot.Commands
{
    public static class SendBookingInfoCommand
    {
        public static async Task ExecuteAsync(CallbackQuery callback, ITelegramBotClient botClient, IBookingService bookingService,
                                              IWorkPlaceService workPlaceService, IMapService mapService, IOfficeService officeService,
                                              IParkingPlaceService parkingPlaceService, Guid bookingId)
        {
            var buttons = new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithCallbackData("Delete", $"Delete:{bookingId}"),
                InlineKeyboardButton.WithCallbackData("Edit", $"Edit:{bookingId}")
            };

            var booking = await bookingService.GetByIdAsync(bookingId);
            var workPlace = await workPlaceService.GetByIdAsync(booking.WorkPlaceId);
            var map = await mapService.GetByIdAsync(workPlace.MapId);
            var office = await officeService.GetByIdAsync(map.OfficeId);

            var text = $"Booking summary:\n" +
                       $"Office Address: {office.Country}, {office.City}, {office.Address}\n" +
                       $"Floor: {map.Floor}\n" +
                       $"Workplace number: {workPlace.Number}\n" +
                       $"Booking date: {booking.BookingStart.Year}.{booking.BookingStart.Month}.{booking.BookingStart.Day}";

            if (booking.BookingStart != booking.BookingEnd)
            {
                text += $" - {booking.BookingEnd.Year}.{booking.BookingEnd.Month}.{booking.BookingEnd.Day}\n";
            }
            else
            {
                text += "\n";
            }
            if (booking.IsRecurring)
            {
                text += $"Frequency: {booking.Frequancy}\n";
            }
            if (booking.ParkingPlaceId != null)
            {
                text += $"Parking place number {parkingPlaceService.GetByIdAsync(booking.ParkingPlaceId.Value).Result.Number}";
            }

            var backButton = Tuple.Create(UserState.SelectingAction, "My Bookings");

            var inlineKeyboard = KeyboardBuilder.BuildInLineKeyboard(buttons, 2, backButton);
            await botClient.EditMessageTextAsync(chatId: callback.From.Id,
                                                 messageId: callback.Message.MessageId,
                                                 text: text,
                                                 replyMarkup: inlineKeyboard);
        }
    }
}
