﻿using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Services;

public class HandleUpdateService
{
    private readonly ITelegramBotClient _botClient;

    public HandleUpdateService(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task Handle(Update update)
    {

    }
}
