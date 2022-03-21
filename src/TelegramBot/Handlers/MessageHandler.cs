using Application.Interfaces.IServices;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Commands;

namespace TelegramBot.Handlers
{
    public static class MessageHandler
    {
        public static async Task HandleAsync(Message message, ITelegramBotClient botClient, IAppUserService userService)
        {
            switch (message.Text)
            {
                case "/start":
                    await StartCommand.ExecuteAsync(message, botClient, userService);
                    return;
                default:
                    await botClient.DeleteMessageAsync(message.From.Id, message.MessageId);
                    return;
            }
        }
    }
}
