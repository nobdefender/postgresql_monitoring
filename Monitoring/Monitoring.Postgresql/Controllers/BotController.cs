using Microsoft.AspNetCore.Mvc;
using Monitoring.Postgresql.Providers.Implementations;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Monitoring.Postgresql.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    [Route("/")]
    public class BotController : ControllerBase
    {
        private readonly TelegramBotClient _telegramBotClient;
        private readonly IUserActionProvider _userActionProvider;

        public BotController(TelegramBotClient telegramBotClient, IUserActionProvider userActionProvider)
        {
            _telegramBotClient = telegramBotClient;
            _userActionProvider = userActionProvider;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update, CancellationToken cancellationToken)
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                if (update.Message?.Text == "/start")
                {
                    await _telegramBotClient.SendTextMessageAsync(update.Message.From.Id, "start");
                    return Ok();
                }

                if (new[] { "/get_chat_id", "/getid" }.Contains(update.Message?.Text))
                {
                    await _telegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, $"{update.Message.Chat.Id}");
                    return Ok();
                }

                if (update.Message?.Text == "/end")
                {
                    await _telegramBotClient.SendTextMessageAsync(update.Message.From.Id, "end");
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

            return BadRequest();
        }

        [HttpGet]
        public string Get()
        {
            return "Telegram bot was started";
        }
    }

}
