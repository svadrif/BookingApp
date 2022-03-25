using Application.DTOs.BookingDTO;
using Application.Interfaces.IServices;
using Application.Pagination;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Validations;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Helpers;

namespace TelegramBot.Commands
{
    public static class SendWorkPlacesCommand
    {
        public static async Task ExecuteAsync(CallbackQuery callback, ITelegramBotClient botClient, IWorkPlaceService workPlaceService, 
                                              IBookingService bookingService, Guid mapId, DateTime bookingStart, DateTime bookingEnd, 
                                              UserState backState, string backCommand)
        {
            var workPlaces = await workPlaceService.SearchByMapIdAsync(mapId, new PagedQueryBase() { SortOn = "Number" });
            var buttons = new List<InlineKeyboardButton>();

            PagedList<GetBookingDTO> bookings;
            bool isValid;
            int page;

            string buttonText;
            foreach (var workPlace in workPlaces)
            {
                page = 1;
                isValid = true;
                do
                {
                    bookings = await bookingService.SearchByWorkPlaceIdAsync(workPlace.Id, new PagedQueryBase() { CurrentPage = page });
                    foreach (var booking in bookings)
                    {
                        isValid = BookingValidation.ValidateBookingDate(new Booking { BookingStart = bookingStart, BookingEnd = bookingEnd },
                                                      booking.BookingStart, booking.BookingEnd);
                        if (!isValid)
                        {
                            break;
                        }
                    }
                    page++;
                } while (bookings.CurrentPage < bookings.TotalPages && isValid);

                if (!isValid)
                {
                    continue;
                }

                buttonText = $"{workPlace.Number}";

                if (workPlace.IsNextToWindow)
                {
                    buttonText += "🪟";
                }
                if (workPlace.HasPC)
                {
                    buttonText += "💻";
                }
                if (workPlace.HasMonitor)
                {
                    buttonText += "🖥";
                }
                if (workPlace.HasKeyboard)
                {
                    buttonText += "⌨️";
                }
                if (workPlace.HasMouse)
                {
                    buttonText += "🖱";
                }
                if (workPlace.HasHeadset)
                {
                    buttonText += "🎧";
                }

                buttons.Add(InlineKeyboardButton.WithCallbackData(buttonText, $"{workPlace.Id.ToString()}"));
            }

            //var backButton = Tuple.Create(backState, backCommand);
            var backButton = Tuple.Create(UserState.SelectingSpecificWorkPlace, "Yes");

            var inlineKeyboard = KeyboardBuilder.BuildInLineKeyboard(buttons, 2, backButton);
            await botClient.EditMessageTextAsync(chatId: callback.From.Id,
                                                 messageId: callback.Message.MessageId,
                                                 text: "Select workplace:\n" +
                                                       "🪟 - next to window\t💻 - has PC\n" +
                                                       "🖥 - has monitor\t⌨️ - has keyboard\n" +
                                                       "🖱 - has mouse\t🎧 - has headset",
                                                 replyMarkup: inlineKeyboard);
        }
    }
}
