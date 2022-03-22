using Domain.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Helpers;

namespace TelegramBot.Commands
{
    public static class SelectBookingTypeCommand
    {
        static readonly List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>
        {
            InlineKeyboardButton.WithCallbackData("One-day", "Dne-day"),
            InlineKeyboardButton.WithCallbackData("Continuous", "Continuous")
        };

        public static async Task ExecuteAsync(CallbackQuery callback, ITelegramBotClient botClient, string backCommand)
        {
            var backButton = Tuple.Create(UserState.SelectingCity, backCommand);

            var inlineKeyboard = KeyboardBuilder.BuildInLineKeyboard(buttons, 2, backButton);
            await botClient.EditMessageTextAsync(chatId: callback.From.Id,
                                                 messageId: callback.Message.MessageId,
                                                 text: "Select booking type:",
                                                 replyMarkup: inlineKeyboard);
        }
    }
}
