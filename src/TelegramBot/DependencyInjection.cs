using Microsoft.Extensions.DependencyInjection;
using TelegramBot.Services;

namespace TelegramBot;

public static class DependencyInjection
{
    public static void AddTelegramBot(this IServiceCollection services)
    {
        services.AddHostedService<ConfigureWebhook>();
        services.AddScoped<HandleUpdateService>();
    }
}