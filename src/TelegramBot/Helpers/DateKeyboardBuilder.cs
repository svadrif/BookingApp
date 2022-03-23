using Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Helpers
{
    public static class DateKeyboardBuilder
    {
        public static InlineKeyboardMarkup BuildKeyboard(DateTime startDate, int dayCount, Tuple<UserState, string>? backButton = null)
        {
            int currentColumn = 0;
            var rows = new List<InlineKeyboardButton[]>();
            var columns = new List<InlineKeyboardButton>();

            rows.Add(new[]
            {
                InlineKeyboardButton.WithCallbackData("Mo", "."),
                InlineKeyboardButton.WithCallbackData("Tu", "."),
                InlineKeyboardButton.WithCallbackData("We", "."),
                InlineKeyboardButton.WithCallbackData("Th", "."),
                InlineKeyboardButton.WithCallbackData("Fr", "."),
                InlineKeyboardButton.WithCallbackData("Sa", "."),
                InlineKeyboardButton.WithCallbackData("Su", ".")
            });

            for (int dayOfWeek = 0; dayOfWeek < ((int)startDate.DayOfWeek + 6) % 7; dayOfWeek++)
            {
                Console.WriteLine($"skipDay {dayOfWeek} of {(int)startDate.DayOfWeek}");
                columns.Add(InlineKeyboardButton.WithCallbackData(" ", "."));
                currentColumn++;
            }

            for (int currentDay = 0; currentDay < dayCount; currentDay++)
            {
                Console.WriteLine($"addDay {startDate.AddDays(currentDay).Day}, which is {(int)startDate.AddDays(currentDay).DayOfWeek}");
                columns.Add(InlineKeyboardButton.WithCallbackData($"{startDate.AddDays(currentDay).Day}", $"{startDate.AddDays(currentDay)}"));
                currentColumn++;

                if (currentColumn < 7)
                {
                    continue;
                }

                rows.Add(columns.ToArray());
                columns = new List<InlineKeyboardButton>();
                currentColumn = 0;
            }

            if (currentColumn > 0)
            {
                for (int dayOfWeek = ((int)startDate.AddDays(dayCount).DayOfWeek + 6) % 7; dayOfWeek < 7; dayOfWeek++)
                {
                    Console.WriteLine($"skipDay {dayOfWeek} after {(int)startDate.AddDays(dayCount).DayOfWeek}");
                    columns.Add(InlineKeyboardButton.WithCallbackData(" ", "."));
                    currentColumn++;
                }
                rows.Add(columns.ToArray());
            }

            rows.Add(new[]
            {
                InlineKeyboardButton.WithCallbackData("<<", $"month:{startDate.Month - DateTime.Now.Month - 1}"),
                InlineKeyboardButton.WithCallbackData(">>", $"month:{startDate.Month - DateTime.Now.Month + 1}"),
            });

            if (backButton != null)
            {
                rows.Add(new[] { InlineKeyboardButton.WithCallbackData("back", $"back:{backButton.Item1}:{backButton.Item2}") });
            }

            var keyboard = new InlineKeyboardMarkup(rows);
            return keyboard;
        }
    }
}
