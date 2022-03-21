using Application.Interfaces.IServices;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Handlers;

namespace TelegramBot.Services;

public class HandleUpdateService
{
    private readonly ITelegramBotClient _botClient;
    private readonly IAppUserService _userService;
    private readonly IStateService _stateService;
    private readonly IBookingHistoryService _historyService;
    private readonly IOfficeService _officeService;

    public HandleUpdateService(
        ITelegramBotClient botClient,
        IAppUserService userService,
        IStateService stateService,
        IBookingHistoryService historyService,
        IOfficeService officeService)
    {
        _botClient = botClient;
        _userService = userService;
        _stateService = stateService;
        _historyService = historyService;
        _officeService = officeService;
    }

    public async Task Handle(Update update)
    {
        if (update.Message != null)
        {
            var user = await _userService.GetByTelegramIdAsync(update.Message.From.Id);
            if (user == null)
            {
                await _botClient.SendTextMessageAsync(update.Message.From.Id, "You are not authorized.");
                return;
            }
        }

        switch (update.Type)
        {
            case UpdateType.Message:
                await MessageHandler.HandleAsync(update.Message, _botClient, _userService, _stateService);
                return;

            case UpdateType.CallbackQuery:
                await CallbackQueryHandler.HandleAsync(
                        update.CallbackQuery,
                        _botClient,
                        _userService,
                        _stateService,
                        _historyService,
                        _officeService);
                return;
        }
    }
}

