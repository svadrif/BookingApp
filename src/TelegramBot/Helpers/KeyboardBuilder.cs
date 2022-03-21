using Domain.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Helpers
{
    public static class KeyboardBuilder
    {
        public static InlineKeyboardMarkup BuildInLineKeyboard(List<InlineKeyboardButton> buttons, int totalColumns, Tuple<UserState, string>? backButton = null)
        {
            int currentColumn = 0;
            var rows = new List<InlineKeyboardButton[]>();
            var columns = new List<InlineKeyboardButton>();

            foreach (var button in buttons)
            {
                columns.Add(button);
                currentColumn++;

                if (currentColumn < totalColumns)
                {
                    continue;
                }

                rows.Add(columns.ToArray());
                columns = new List<InlineKeyboardButton>();
                currentColumn = 0;
            }

            if (currentColumn > 0)
            {
                rows.Add(columns.ToArray());
            }

            if(backButton != null)
            {
                rows.Add(new[] { InlineKeyboardButton.WithCallbackData("back", $"back:{backButton.Item1}:{backButton.Item2}") });
            }

            var keyboard = new InlineKeyboardMarkup(rows);
            return keyboard;
        }
    }
}
