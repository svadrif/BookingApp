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
        public static async Task ExecuteAsync(Message message, ITelegramBotClient botClient, IAppUserService userService, IStateService stateService)
        {
            var buttons = new List<InlineKeyboardButton>{
                InlineKeyboardButton.WithCallbackData("New booking", "New Booking"),
                InlineKeyboardButton.WithCallbackData("My bookings", "My Bookings")
            };

            var user = await userService.GetByTelegramIdAsync(message.From.Id);
            var state = await stateService.GetByUserIdAsync(user.Id);

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
            await stateService.UpdateAsync(state);
        }
    }
}
