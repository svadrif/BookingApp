using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Commands;

namespace TelegramBot.Handlers
{
    public static class MessageHandler
    {
        public static async Task HandleAsync(Message message, ITelegramBotClient botClient)
        {
            switch (message.Text)
            {
                case "/start":
                    await StartCommand.ExecuteAsync(message, botClient);
                    return;
                default:
                    await botClient.DeleteMessageAsync(message.From.Id, message.MessageId);
                    return;
            }
        }
    }
}
