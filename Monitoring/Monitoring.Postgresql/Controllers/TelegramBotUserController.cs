using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Monitoring.Postgresql.Models.Auth;
using Monitoring.Postgresql.Providers.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Monitoring.Postgresql.Controllers;

public class TelegramBotUserController : BaseController
{
    private readonly ITelegramBotUserProvider _telegramBotUserProvider;

    public TelegramBotUserController(ILogger<TelegramBotUserController> logger,
        ITelegramBotUserProvider telegramBotUserProvider) : base(logger)
    {
        _telegramBotUserProvider = telegramBotUserProvider;
    }

    /// <summary>
    /// Получение списка всех пользователей телеграмм бота
    /// </summary>
    /// <returns></returns>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost(nameof(GetAllTelegramBotUsers))]
    [SwaggerResponse(200, Type = typeof(string))]
    public async Task<IResult> GetAllTelegramBotUsers(CancellationToken cancellationToken)
    {
        try
        {
            var telegramBotUsers = await _telegramBotUserProvider.GetAllTelegramBotUsersAsync(cancellationToken);
            return Results.Ok(telegramBotUsers);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка получения пользователей телеграмм бота");
            return Results.NotFound("Пользователи не найдены");
        }
    }
}