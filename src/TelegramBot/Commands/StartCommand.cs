using Application.Interfaces.IServices;
using Domain.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Helpers;

namespace TelegramBot.Commands
{
    public static class StartCommand
    {
        public static async Task ExecuteAsync(Message message, ITelegramBotClient botClient, IAppUserService userService)
        {
            var buttons = new List<InlineKeyboardButton>{
                InlineKeyboardButton.WithCallbackData("New booking", "New Booking"),
                InlineKeyboardButton.WithCallbackData("My bookings", "My Bookings")
            };

            var state = await userService.GetStateByTelegramIdAsync(message.From.Id);

            if (state.StateNumber != UserState.NotAuthorized)
            {
                await botClient.DeleteMessageAsync(message.From.Id, state.MessageId);
            }

            var inlineKeyboard = KeyboardBuilder.BuildInLineKeyboard(buttons, 2);
            var responce = await botClient.SendTextMessageAsync(chatId: message.From.Id,
                                                 text: "Select action:",
                                                 replyMarkup: inlineKeyboard);

            state.StateNumber = UserState.SelectingAction;
            state.LastCommand = "/start";
            state.MessageId = responce.MessageId;
            await userService.UpdateUserStateAsync(state);
        }
    }
}
