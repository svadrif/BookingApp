using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Helpers
{
    public static class KeyboardBuilder
    {
        public static InlineKeyboardMarkup BuildInLineKeyboard(List<InlineKeyboardButton> buttons, int totalColumns)
        {
            int currentColumn = 0;
            var rows = new List<InlineKeyboardButton[]>();
            var columns = new List<InlineKeyboardButton>();

            foreach (var button in buttons)
            {
                currentColumn++;
                columns.Add(button);

                if (currentColumn >= totalColumns)
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

            var keyboard = new InlineKeyboardMarkup(rows);
            return keyboard;
        }
    }
}
