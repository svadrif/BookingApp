using Application.Interfaces.IServices;
using Domain.Entities;
using Domain.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Helpers;

namespace TelegramBot.Commands
{
    public static class SendBookingSummaryCommand
    {
        public static async Task ExecuteAsync(CallbackQuery callback, ITelegramBotClient botClient, BookingHistory history, IOfficeService officeService, 
                                              IWorkPlaceService workPlaceService, IParkingPlaceService parkingPlaceService,
                                              UserState backState, string backCommand)
        {
            var buttons = new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithCallbackData("Confirm", "Confirm"),
                InlineKeyboardButton.WithCallbackData("Cancel", "Cancel")
            };

            var backButton = Tuple.Create(backState, backCommand);

            var text = $"Booking summary:\n" +
                       $"Office Address: {history.Country}, {history.City}, {officeService.GetByIdAsync(history.OfficeId.Value).Result.Address}\n" +
                       $"Floor: {history.Floor}\n" +
                       $"Workplace number: {workPlaceService.GetByIdAsync(history.WorkPlaceId.Value).Result.Number}\n";

            DateTimeOffset date;
            if (history.BookingStart == history.BookingEnd)
            {
                date = history.BookingStart.Value;
                text += $"Booking date: {date.Year}.{date.Month}.{date.Day}\n";
            }
            else
            {
                date = history.BookingStart.Value;
                text += $"Booking date: {date.Year}.{date.Month}.{date.Day}";

                date = history.BookingEnd.Value;
                text += $" - {date.Year}.{date.Month}.{date.Day}\n";
            }
            if (history.IsRecurring.Value)
            {
                text += $"Frequency: {history.Frequancy}\n";
            }
            if (history.ParkingPlaceId != null)
            {
                text += $"Parking place number {parkingPlaceService.GetByIdAsync(history.ParkingPlaceId.Value).Result.Number}";
            }

            var inlineKeyboard = KeyboardBuilder.BuildInLineKeyboard(buttons, 2, backButton);
            await botClient.EditMessageTextAsync(chatId: callback.From.Id,
                                                 messageId: callback.Message.MessageId,
                                                 text: text,
                                                 replyMarkup: inlineKeyboard);
        }
    }
}
