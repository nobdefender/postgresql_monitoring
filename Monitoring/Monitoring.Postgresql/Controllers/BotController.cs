﻿using Microsoft.AspNetCore.Mvc;
using Monitoring.Postgresql.Providers.Implementations;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Monitoring.Postgresql.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    [Route("/")]
    public class BotController : ControllerBase
    {
        private readonly TelegramBotClient _telegramBotClient;
        private readonly UserActionProvider _userActionProvider;

        public BotController(TelegramBotClient telegramBotClient, UserActionProvider userActionProvider)
        {
            _telegramBotClient = telegramBotClient;
            _userActionProvider = userActionProvider;
        }

        private readonly InlineKeyboardMarkup buttons = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Статистика", "сommand_stat"),
                }
            });

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update, CancellationToken cancellationToken)
        {
            var tgClient = _telegramBotClient;

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                if (update.Message?.Text == "/start")
                {
                    await tgClient.SendTextMessageAsync(update.Message.From.Id, "comands", replyMarkup: buttons);
                    return Ok();
                }

                if (update.Message?.Text == "/get_chat_id" || update.Message?.Text == "/getid")
                {
                    await tgClient.SendTextMessageAsync(update.Message.Chat.Id, $"{update.Message.Chat.Id}");
                    return Ok();
                }
            }

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                if (update.CallbackQuery?.Data?.StartsWith("UserAction") ?? false)
                {
                    await _userActionProvider.Select(update.CallbackQuery.Data, cancellationToken);

                    return Ok();
                }

                if (update.CallbackQuery?.Data == "сommand_stat")
                {
                    await tgClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "stat");

                    return Ok();
                }
            }

            return Ok();
            //return BadRequest();
        }

        [HttpGet]
        public string Get()
        {
            return "Telegram bot was started";
        }
    }

}
