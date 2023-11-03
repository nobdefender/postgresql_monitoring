using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monitoring.Postgresql.Models.Auth;
using Monitoring.Postgresql.Providers.Implementations;
using Monitoring.Postgresql.Providers.Interfaces;

namespace Monitoring.Postgresql.Controllers;

public class UserController : BaseController
{
    private readonly IUserProvider _userProvider;

    public UserController(
        ILogger<UserController> logger,
        IHttpContextAccessor httpContextAccessor,
        IUserProvider userProvider)
        : base(httpContextAccessor, logger)
    {
        _userProvider = userProvider;
    }

    /// <summary>
    /// Авторизация пользователя
    /// </summary>
    /// <param name="userLoginRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost(nameof(Login))]
    // [SwaggerResponse(200, Type = typeof(string))]
    public async Task<IResult> Login([FromBody] UserLoginRequest userLoginRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userProvider.LoginAsync(userLoginRequest, cancellationToken);
            return user;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка получения данных пользователя");
            return Results.NotFound("Пользователь не найден");
        }
    }
    
    // /// <summary>
    // /// Обновление Access токена
    // /// </summary>
    // /// <param name="tokens"></param>
    // /// <param name="cancellationToken"></param>
    // /// <returns></returns>
    // [AllowAnonymous]
    // [HttpPost(nameof(Refresh))]
    // // [SwaggerResponse(200, Type = typeof(TokenApiModel))]
    // public async Task<IResult> Refresh([FromBody] TokenApiModel tokens, CancellationToken cancellationToken)
    // {
    //     try
    //     {
    //         var result = await _userProvider.RefreshAsync(tokens, cancellationToken);
    //         return result;
    //     }
    //     catch (Exception e)
    //     {
    //         _logger.LogError(e, "Ошибка обновления AccessToken");
    //         return Results.NotFound("Ошибка обновления токена");
    //     }
    // }
    //
    // /// <summary>
    // /// Получить всех пользователей
    // /// </summary>
    // /// <param name="cancellationToken"></param>
    // /// <returns>Список пользователей</returns>
    // [AllowAnonymous]
    // [HttpGet(nameof(AllUsers))]
    // public async Task<IResult> AllUsers(CancellationToken cancellationToken)
    // {
    //     try
    //     {
    //         var result = await _userProvider.GetAllUsersAsync(cancellationToken);
    //         return Results.Ok(result);
    //     }
    //     catch (Exception e)
    //     {
    //         _logger.LogError(e, "Ошибка получения комментариев");
    //         return Results.NotFound("Комментарии не получены");
    //     }
    // }
    //
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    // [HttpGet("GetUserData")]
    // [SwaggerResponse(200, Type = typeof(UserDTO))]
    // public async Task<IResult> GetUserData([FromQuery] string token,
    //     CancellationToken cancellationToken)
    // {
    //     try
    //     {
    //         var userId = _userProvider.GetUserIdByJwtToken(token);
    //         if (userId == null)
    //         {
    //             return Results.NotFound("Пользователь не найден");
    //         }
    //
    //         var user = await _userProvider.GetById(userId, cancellationToken);
    //         return Results.Ok(user);
    //     }
    //     catch (Exception e)
    //     {
    //         _logger.LogError(e, "Ошибка получения данных пользователя");
    //         return Results.NotFound("Пользователь не найден");
    //     }
    // }
    //
    // /// <summary>
    // /// Создать/обновить пользователя
    // /// </summary>
    // /// <param name="model">Инфомация о пользователе</param>
    // /// <param name="cancellationToken"></param>
    // /// <returns>Список пользователей</returns>
    // [Authorize(Roles = "Reviewer")]
    // [HttpPost(nameof(Upsert))]
    // public async Task<IResult> Upsert([FromBody] UpsertUserDTO model,
    //     CancellationToken cancellationToken)
    // {
    //     try
    //     {
    //         var result = await _userProvider.UpsertUserAsync(model, cancellationToken);
    //         return result;
    //     }
    //     catch (Exception e)
    //     {
    //         _logger.LogError(e, "Ошибка создания/обнолвнеия пользователя");
    //         return Results.NotFound("Пользователь не создан/обновлён");
    //     }
    // }
    //
    // /// <summary>
    // /// Установить пароль
    // /// </summary>
    // /// <param name="model">Данные для установки пароля</param>
    // /// <param name="cancellationToken"></param>
    // /// <returns>Список пользователей</returns>
    // [Authorize(Roles = "Reviewer")]
    // [HttpPost(nameof(SetPassword))]
    // public async Task<IResult> SetPassword([FromBody] SetPasswordDTO model,
    //     CancellationToken cancellationToken)
    // {
    //     try
    //     {
    //         var result = await _userProvider.SetPasswordAsync(model, cancellationToken);
    //         return result;
    //     }
    //     catch (Exception e)
    //     {
    //         _logger.LogError(e, "Ошибка установки пароля");
    //         return Results.NotFound("Пароль не установлен");
    //     }
    // }
}