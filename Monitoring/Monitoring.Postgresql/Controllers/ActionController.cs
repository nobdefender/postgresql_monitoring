using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monitoring.Postgresql.Models.Action;
using Monitoring.Postgresql.Providers.Interfaces;

namespace Monitoring.Postgresql.Controllers;

public class ActionController : BaseController
{
    private readonly IActionProvider _actionProvider;

    public ActionController(ILogger<UserController> logger, IActionProvider actionProvider) : base(logger)
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
    /// Получить все доступные действия пользователя
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Список доступных действий</returns>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet(nameof(UserActions))]
    public async Task<IResult> UserActions([FromQuery] int? userId, CancellationToken cancellationToken)
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
            return Results.NotFound("Список действий не получен");
        }
    }

    /// <summary>
    /// Обновить все доступные действия пользователя
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Список доступных действий</returns>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut(nameof(UpdateUserActions))]
    public async Task<IResult> UpdateUserActions([FromBody] UpdateUserActionsDTO? dto,
        CancellationToken cancellationToken)
    {
        try
        {
            if (dto == null)
            {
                return Results.BadRequest("Неправильные параметры запроса");
            }

            await _actionProvider.UpdateUserActionsAsync(dto, cancellationToken);
            return Results.Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка получения списка действий");
            return Results.NotFound("Список действий не получен");
        }
    }
}