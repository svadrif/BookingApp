using Domain.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Helpers;

namespace TelegramBot.Commands
{
    public static class SendDateCommand
    {
        public static async Task ExecuteAsync(CallbackQuery callback, ITelegramBotClient botClient,
                                              DateTime startDate, int skipMonths, string type,
                                              UserState backState, string backCommand)
        {
            var daysInMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);

            for (int monthCounter = 0; monthCounter < skipMonths; monthCounter++)
            {
                startDate = startDate.AddDays(daysInMonth - (int)startDate.Day + 1);
                daysInMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
            }
            if(startDate.Month == DateTime.Now.AddMonths(3).Month)
            {
                daysInMonth = DateTime.Now.AddMonths(3).Day;
            }

            var dayCount = daysInMonth - (int)startDate.Day + 1;

            var backButton = Tuple.Create(backState, backCommand);

            var text = "Select ";
            if (type.Equals("start"))
            {
                text += "start ";
            }
            else if (type.Equals("end"))
            {
                text += "end ";
            }
            text += $"date:\nYear: {startDate.Year}\nMonth: {startDate.Month}";

            var inlineKeyboard = DateKeyboardBuilder.BuildKeyboard(startDate, dayCount, backButton);
            await botClient.EditMessageTextAsync(chatId: callback.From.Id,
                                                 messageId: callback.Message.MessageId,
                                                 text: text,
                                                 replyMarkup: inlineKeyboard);
        }
    }
}
