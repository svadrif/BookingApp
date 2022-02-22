using Application.TelegramBot;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddHostedService<ConfigureWebhook>();
        services.AddScoped<HandleUpdateService>();
    }
}