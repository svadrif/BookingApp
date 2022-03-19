using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Helpers;

namespace TelegramBot.Commands
{
    public static class StartCommand
    {
        public static async Task ExecuteAsync(Message message, ITelegramBotClient botClient)
        {
            var buttons = new List<InlineKeyboardButton>{
                InlineKeyboardButton.WithCallbackData("New booking", "New Booking"),
                InlineKeyboardButton.WithCallbackData("My bookings", "My Bookings")
            };

            var inlineKeyboard = KeyboardBuilder.BuildInLineKeyboard(buttons, 2);

            await botClient.SendTextMessageAsync(chatId: message.From.Id,
                                                 text: "Select action:",
                                                 replyMarkup: inlineKeyboard);
        }
    }
}
