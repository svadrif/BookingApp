using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TelegramBot.Services;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TelegramBotController : ControllerBase
{
    private readonly ILoggerManager _logger;
    public TelegramBotController(ILoggerManager logger)
    {
        _logger = logger;
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromServices] HandleUpdateService handleUpdateService,
                                          [FromBody] Update update)
    {
        await handleUpdateService.Handle(update);
        _logger.LogInfo($"called method {nameof(Post)}");
        return Ok();
    }
}
