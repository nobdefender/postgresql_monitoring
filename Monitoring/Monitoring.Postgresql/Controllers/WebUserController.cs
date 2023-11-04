using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monitoring.Postgresql.Models.Auth;
using Monitoring.Postgresql.Providers.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Monitoring.Postgresql.Controllers;

public class WebUserController : UserBaseController
{
    private readonly IWebUserProvider _webUserProvider;

    public WebUserController(
        ILogger<WebUserController> logger,
        IHttpContextAccessor httpContextAccessor,
        IWebUserProvider webUserProvider)
        : base(httpContextAccessor, logger)
    {
        _webUserProvider = webUserProvider;
    }

    /// <summary>
    /// Авторизация пользователя
    /// </summary>
    /// <param name="userLoginRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost(nameof(Login))]
    [SwaggerResponse(200, Type = typeof(string))]
    public async Task<IResult> Login([FromBody] UserLoginRequest userLoginRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _webUserProvider.WebUserLoginAsync(userLoginRequest, cancellationToken);
            return user;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка получения данных пользователя");
            return Results.NotFound("Пользователь не найден");
        }
    }

    /// <summary>
    /// Обновление Access токена
    /// </summary>
    /// <param name="tokens"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost(nameof(Refresh))]
    [SwaggerResponse(200, Type = typeof(TokenApiModel))]
    public async Task<IResult> Refresh([FromBody] TokenApiModel tokens, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _webUserProvider.WebUserRefreshAsync(tokens, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка обновления AccessToken");
            return Results.NotFound("Ошибка обновления токена");
        }
    }

    /// <summary>
    /// Получить всех пользователей
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Список пользователей</returns>
    [AllowAnonymous]
    [HttpGet(nameof(AllUsers))]
    public async Task<IResult> AllUsers(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _webUserProvider.GetAllWebUsersAsync(cancellationToken);
            return Results.Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка получения комментариев");
            return Results.NotFound("Комментарии не получены");
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("GetUserData")]
    [SwaggerResponse(200, Type = typeof(WebUserDTO))]
    public async Task<IResult> GetUserData([FromQuery] string token,
        CancellationToken cancellationToken)
    {
        try
        {
            var userId = _webUserProvider.GetWebUserIdByJwtToken(token);
            if (userId == null)
            {
                return Results.NotFound("Пользователь не найден");
            }

            var user = await _webUserProvider.GetWebUserById(userId, cancellationToken);
            return Results.Ok(user);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка получения данных пользователя");
            return Results.NotFound("Пользователь не найден");
        }
    }

    /// <summary>
    /// Создать/обновить пользователя
    /// </summary>
    /// <param name="model">Инфомация о пользователе</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Список пользователей</returns>
    [Authorize(Roles = "Reviewer")]
    [HttpPost(nameof(Upsert))]
    public async Task<IResult> Upsert([FromBody] UpsertWebUserDTO model,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _webUserProvider.UpsertWebUserAsync(model, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка создания/обнолвнеия пользователя");
            return Results.NotFound("Пользователь не создан/обновлён");
        }
    }

    /// <summary>
    /// Установить пароль
    /// </summary>
    /// <param name="model">Данные для установки пароля</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Список пользователей</returns>
    [Authorize(Roles = "Reviewer")]
    [HttpPost(nameof(SetPassword))]
    public async Task<IResult> SetPassword([FromBody] SetPasswordDTO model,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _webUserProvider.SetWebUserPasswordAsync(model, cancellationToken);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка установки пароля");
            return Results.NotFound("Пароль не установлен");
        }
    }
}