using Application.Interfaces.IServices;
using Domain.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Commands;

namespace TelegramBot.Handlers
{
    public class CallbackQueryHandler
    {
        public static async Task HandleAsync(
            CallbackQuery callback,
            ITelegramBotClient botClient,
            IAppUserService userService,
            IStateService stateService,
            IBookingHistoryService historyService,
            IOfficeService officeService)
        {
            var user = await userService.GetByTelegramIdAsync(callback.From.Id);
            var state = await stateService.GetByUserIdAsync(user.Id);
            var history = await historyService.GetByUserIdAsync(user.Id);
            switch (state.StateNumber)
            {
                case UserState.SelectingAction:
                    switch (callback.Data)
                    {
                        case "New Booking":
                            await SendCountriesCommand.ExecuteAsync(callback, botClient, officeService);

                            state.LastCommand = callback.Data;
                            state.StateNumber = UserState.SelectingCountry;
                            await stateService.UpdateAsync(state);
                            return;

                        case "My  Bookings":

                            return;
                    }
                    return;

                case UserState.SelectingCountry:
                    await SendCitiesCommand.ExecuteAsync(callback, botClient, officeService);

                    state.LastCommand = callback.Data;
                    state.StateNumber = UserState.SelectingCity;
                    await stateService.UpdateAsync(state);

                    history.Country = callback.Data;
                    await historyService.UpdateAsync(history);
                    return;
            }
        }
    }
}
