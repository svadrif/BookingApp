using Application.Interfaces.IServices;
using Application.Pagination;
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

            var inlineKeyboard = KeyboardBuilder.BuildInLineKeyboard(buttons, 2);
            await botClient.EditMessageTextAsync(chatId: callback.From.Id,
                                                 messageId: callback.Message.MessageId,
                                                 text: "Select country:",
                                                 replyMarkup: inlineKeyboard);
        }
    }
}
