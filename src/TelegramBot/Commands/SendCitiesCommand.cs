using Application.Interfaces.IServices;
using Application.Pagination;
using Domain.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Helpers;

namespace TelegramBot.Commands
{
    public static class SendCitiesCommand
    {
        public static async Task ExecuteAsync(CallbackQuery callback, ITelegramBotClient botClient, IOfficeService officeService)
        {
            var cities = await officeService.GetCitiesAsync(callback.Data, new PagedQueryBase());
            var buttons = new List<InlineKeyboardButton>();
            foreach (var city in cities)
            {
                buttons.Add(InlineKeyboardButton.WithCallbackData(city, city));
            }
            var backButton = Tuple.Create(UserState.SelectingAction, "New Booking");

            var inlineKeyboard = KeyboardBuilder.BuildInLineKeyboard(buttons, 2, backButton);
            await botClient.EditMessageTextAsync(chatId: callback.From.Id,
                                                 messageId: callback.Message.MessageId,
                                                 text: "Select city:",
                                                 replyMarkup: inlineKeyboard);
        }
    }
}
