using Application.Interfaces.IServices;
using Application.Pagination;
using Domain.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Helpers;

namespace TelegramBot.Commands
{
    public static class SendFloorsCommand
    {
        public static async Task ExecuteAsync(CallbackQuery callback, ITelegramBotClient botClient, IMapService mapService, Guid officeId)
        {
            var maps = await mapService.SearchByOfficeIdAsync(officeId, new PagedQueryBase() { SortOn = "Floor" });
            var buttons = new List<InlineKeyboardButton>();
            string buttonText;
            foreach (var map in maps)
            {
                buttonText = $"Floor {map.Floor}";
                if (map.HasKitchen)
                {
                    buttonText += "🍽";
                }
                if (map.HasConfRoom)
                {
                    buttonText += "💬";
                }
                buttons.Add(InlineKeyboardButton.WithCallbackData(buttonText, $"{map.Id}"));
            }

            var backButton = Tuple.Create(UserState.SelectingSpecificWorkPlace, "Yes");

            var inlineKeyboard = KeyboardBuilder.BuildInLineKeyboard(buttons, 2, backButton);
            await botClient.EditMessageTextAsync(chatId: callback.From.Id,
                                                 messageId: callback.Message.MessageId,
                                                 text: "Select floor:/n🍽 - has kitchen/t💬 - has conference room",
                                                 replyMarkup: inlineKeyboard);
        }
    }
}
