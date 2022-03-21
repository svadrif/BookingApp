using Application.Interfaces.IServices;
using Application.Pagination;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Helpers;

namespace TelegramBot.Commands
{
    public static class SendCountriesCommand
    {
        public static async Task ExecuteAsync(CallbackQuery callback, ITelegramBotClient botClient, IOfficeService officeService)
        {
            var countries = await officeService.GetCountriesAsync(new PagedQueryBase());
            var buttons = new List<InlineKeyboardButton>();
            foreach (var country in countries)
            {
                buttons.Add(InlineKeyboardButton.WithCallbackData(country, country));
            }

            var inlineKeyboard = KeyboardBuilder.BuildInLineKeyboard(buttons, 2);
            await botClient.EditMessageTextAsync(chatId: callback.From.Id,
                                                 messageId: callback.Message.MessageId,
                                                 text: "Select country:",
                                                 replyMarkup: inlineKeyboard);
        }
    }
}
