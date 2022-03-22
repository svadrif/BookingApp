using Application.Interfaces.IServices;
using Application.Pagination;
using Domain.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Helpers;

namespace TelegramBot.Commands
{
    public static class SendOfficesCommand
    {
        public static async Task ExecuteAsync(CallbackQuery callback, ITelegramBotClient botClient, IOfficeService officeService, string backCommand)
        {
            var offices = await officeService.GetPagedByCityAsync(callback.Data, new PagedQueryBase());
            var buttons = new List<InlineKeyboardButton>();
            foreach (var office in offices)
            {
                buttons.Add(InlineKeyboardButton.WithCallbackData(office.Address, office.Id.ToString()));
            }
            var backButton = Tuple.Create(UserState.SelectingCountry, backCommand);

            var inlineKeyboard = KeyboardBuilder.BuildInLineKeyboard(buttons, 2, backButton);
            await botClient.EditMessageTextAsync(chatId: callback.From.Id,
                                                 messageId: callback.Message.MessageId,
                                                 text: "Select office:",
                                                 replyMarkup: inlineKeyboard);
        }
    }
}
