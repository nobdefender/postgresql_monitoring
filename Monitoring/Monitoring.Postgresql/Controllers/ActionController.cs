using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monitoring.Posgresql.Infrastructure.Models.Access;
using Monitoring.Postgresql.Models.Action;
using Monitoring.Postgresql.Providers.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;

namespace Monitoring.Postgresql.Controllers;

public class ActionController : BaseController
{
    private readonly IActionProvider _actionProvider;

    public ActionController(ILogger<WebUserController> logger, IActionProvider actionProvider) : base(logger)
    {
        _actionProvider = actionProvider;
    }

    /// <summary>
    /// Получить все доступные действия
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Список доступных действий</returns>
    [AllowAnonymous]
    [HttpGet(nameof(AllActions))]
    public async Task<IResult> AllActions(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _actionProvider.GetAllActionsAsync(cancellationToken);
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка получения списка действий");
            return Results.NotFound("Список действий не получен");
        }
    }

    /// <summary>
    /// Получить действие по id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Действие</returns>
    [AllowAnonymous]
    [HttpGet(nameof(GetActionById))]
    public async Task<IResult> GetActionById([FromQuery] int? id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _actionProvider.GetActionByIdAsync(id, cancellationToken);
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка получения действия по id");
            return Results.NotFound("Действие по id не получено");
        }
    }

    /// <summary>
    /// Создания действия
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost(nameof(CreateAction))]
    public async Task<IResult> CreateAction([FromBody] ActionDTO dto, CancellationToken cancellationToken)
    {
        try
        {
            await _actionProvider.CreateActionAsync(dto, cancellationToken);
            return Results.Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка создания действия");
            return Results.NotFound("Действие не создано");
        }
    }
    
    /// <summary>
    /// Обновление действия
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPut(nameof(UpdateAction))]
    public async Task<IResult> UpdateAction([FromBody] ActionDbModel model, CancellationToken cancellationToken)
    {
        try
        {
            await _actionProvider.UpdateActionAsync(model, cancellationToken);
            return Results.Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка обновления действия");
            return Results.NotFound("Действие не обновлено");
        }
    }

    /// <summary>
    /// Удаление действия
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpDelete(nameof(DeleteAction))]
    public async Task<IResult> DeleteAction([FromQuery] int id, CancellationToken cancellationToken)
    {
        try
        {
            await _actionProvider.DeleteActionAsync(id, cancellationToken);
            return Results.Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка удаления действия");
            return Results.NotFound("Действие не удалено");
        }
    }

    /// <summary>
    /// Получить все доступные действия пользователя
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Список доступных действий</returns>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet(nameof(TelegramBotUserActions))]
    public async Task<IResult> TelegramBotUserActions([FromQuery] int? userId, CancellationToken cancellationToken)
    {
        try
        {
            if (userId == null)
            {
                return Results.NotFound("Пользователь не найден");
            }

            var result = await _actionProvider.GetUserActionsAsync(userId.Value, cancellationToken);
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка получения списка действий");
            return Results.NotFound("Список действий пользователя не получен");
        }
    }

    /// <summary>
    /// Обновить все доступные действия пользователя
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Список доступных действий</returns>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut(nameof(UpdateTelegramBotUserActions))]
    public async Task<IResult> UpdateTelegramBotUserActions([FromBody] UpdateUserActionsDTO? dto,
        CancellationToken cancellationToken)
    {
        try
        {
            if (dto == null)
            {
                return Results.BadRequest("Неправильные параметры запроса");
            }

            await _actionProvider.UpdateTelegramBotUserActionsAsync(dto, cancellationToken);
            return Results.Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка получения списка действий");
            return Results.NotFound("Список разрешенных действий не обновлен");
        }
    }
}