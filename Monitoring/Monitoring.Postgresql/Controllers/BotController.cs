using Microsoft.AspNetCore.Mvc;
using Monitoring.Posgresql.Infrastructure.Models.TelegramBot;
using Monitoring.Postgresql.Providers.Implementations;
using Monitoring.Postgresql.Providers.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

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
                var startKeyboard = new ReplyKeyboardMarkup
                (
                    new[]
                    {
                        new[] {
                            new KeyboardButton("Id чата"),
                        }
                    }
                )
                {
                    ResizeKeyboard = true,
                };

                var telegramBotUserDbModel = new TelegramBotUserDbModel
                { 
                    TelegramChatId = update.Message.Chat.Id,
                    FirstName = update.Message.Chat.FirstName,
                    LastName = update.Message.Chat.LastName,
                    UserName = update.Message.Chat.Username
                };

                await _telegramBotUserProvider.Save(telegramBotUserDbModel, cancellationToken);

                await _telegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, _startMessage, replyMarkup: startKeyboard);
                return Ok();
            }

            if (new[] { "/get_chat_id", "/getid", "Id чата" }.Contains(update.Message?.Text))
            {
                await _telegramBotClient.SendTextMessageAsync(update.Message.Chat.Id, $"{update.Message.Chat.Id}");
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
