using Application.TelegramBot;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TelegramBotController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromServices] HandleUpdateService handleUpdateService,
                                          [FromBody] Update update)
    {
        await handleUpdateService.Handle(update);

        return Ok();
    }
}
