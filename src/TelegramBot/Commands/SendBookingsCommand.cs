using Application.Interfaces.IServices;
using Application.Pagination;
using Domain.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Helpers;

namespace TelegramBot.Commands
{
    public static class SendBookingsCommand
    {
        public static async Task ExecuteAsync(CallbackQuery callback, ITelegramBotClient botClient, IBookingService bookingService,
                                              IWorkPlaceService workPlaceService, IMapService mapService, IOfficeService officeService, Guid userId)
        {
            var bookings = await bookingService.SearchByUserIdAsync(userId, new PagedQueryBase() { SortOn = "BookingStart"});
            var buttons = new List<InlineKeyboardButton>();
            string text;

            foreach (var booking in bookings)
            {
                if(!booking.IsActive)
                {
                    continue;
                }

                text = officeService.GetByIdAsync(mapService.GetByIdAsync(workPlaceService.GetByIdAsync(booking.WorkPlaceId).Result.MapId).Result.OfficeId).Result.Address;
                text += $" : {booking.BookingStart.Year}.{booking.BookingStart.Month}.{booking.BookingStart.Day}";
                if(booking.BookingStart != booking.BookingEnd)
                {
                    text += $" - {booking.BookingEnd.Year}.{booking.BookingEnd.Month}.{booking.BookingEnd.Day}";
                }
                buttons.Add(InlineKeyboardButton.WithCallbackData(text, booking.Id.ToString()));
            }
            var backButton = Tuple.Create(UserState.NotAuthorized, "/start");

            var inlineKeyboard = KeyboardBuilder.BuildInLineKeyboard(buttons, 1, backButton);
            await botClient.EditMessageTextAsync(chatId: callback.From.Id,
                                                 messageId: callback.Message.MessageId,
                                                 text: "My Bookings:",
                                                 replyMarkup: inlineKeyboard);
        }
    }
}
