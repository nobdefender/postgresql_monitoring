using Microsoft.AspNetCore.Mvc;
using Monitoring.Postgresql.Providers.Implementations;
using Monitoring.Postgresql.Providers.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Monitoring.Postgresql.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[ApiController]
[Route("/")]
public class BotController : ControllerBase
{
    private readonly TelegramBotClient _telegramBotClient;
    private readonly IUserActionProvider _userActionProvider;
    private readonly ITelegramBotUserProvider _telegramBotUserProvider;

    private readonly string _startMessage = "Отслеживание состояния Postgresql началось. По наступлению ключевых событий вам придет сообщение об ошибке, а также методы его решения.";

    public BotController(TelegramBotClient telegramBotClient, IUserActionProvider userActionProvider, ITelegramBotUserProvider telegramBotUserProvider)
    {
        _telegramBotClient = telegramBotClient;
        _userActionProvider = userActionProvider;
        _telegramBotUserProvider = telegramBotUserProvider;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Update update, CancellationToken cancellationToken)
    {
        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            if (update.Message?.Text == "/start")
            {
                await _telegramBotUserProvider.Save(update.Message.Chat.Id, cancellationToken);

                await _telegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, _startMessage);
                return Ok();
            }

            if (new[] { "/get_chat_id", "/getid" }.Contains(update.Message?.Text))
            {
                await _telegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, $"{update.Message.Chat.Id}");
                return Ok();
            }

            if (update.Message?.Text == "/end")
            {
                await _telegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, "end");
                return Ok();
            }
        }

        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
        {
            if (update.CallbackQuery?.Data?.StartsWith("UserAction") ?? false)
            {
                await _userActionProvider.SetSelect(update.CallbackQuery.Data, cancellationToken);

                return Ok();
            }
        }

        return Ok();
    }

    [HttpGet]
    public string Get()
    {
        return "Telegram bot was started";
    }
}
