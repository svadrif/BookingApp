using Domain.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Helpers;

namespace TelegramBot.Commands
{
    public static class SelectExactWorkPlaceCommand
    {
        public static async Task ExecuteAsync(CallbackQuery callback, ITelegramBotClient botClient, UserState backState, string backCommand)
        {
            var buttons = new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithCallbackData("Yes", "Yes"),
                InlineKeyboardButton.WithCallbackData("No", "No")
            };

            var backButton = Tuple.Create(backState, backCommand);

            var inlineKeyboard = KeyboardBuilder.BuildInLineKeyboard(buttons, 2, backButton);
            await botClient.EditMessageTextAsync(chatId: callback.From.Id,
                                                 messageId: callback.Message.MessageId,
                                                 text: "Would you like to choose the exact workplace?",
                                                 replyMarkup: inlineKeyboard);
        }
    }
}
