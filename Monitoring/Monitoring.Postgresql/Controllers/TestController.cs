﻿using Microsoft.AspNetCore.Mvc;
using Monitoring.Postgresql.Providers.Implementations;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Monitoring.Postgresql.Controllers
{
    [ApiController]
    [Route("/")]
    public class TestController : ControllerBase
    {
        private readonly InlineKeyboardMarkup buttons = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Статистика", "сommand_stat"),
                }
            });

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            var tgClient = TelegramBotProvider.GetBot();

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                if (update.Message?.Text == "/start")
                {

                    await tgClient.SendTextMessageAsync(update.Message.From.Id, "comands", replyMarkup: buttons);


                    return Ok();
                }
            }

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                if (update.CallbackQuery?.Data == "сommand_stat")
                {
                    await tgClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "stat");

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
