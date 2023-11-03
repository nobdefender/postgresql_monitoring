namespace Monitoring.Postgresql.Models.Auth;

/// <summary>
/// Данные для авторизации
/// </summary>
public class UserLoginRequest
{
    /// <summary>
    /// UserName
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; }
}