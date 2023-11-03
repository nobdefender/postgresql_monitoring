namespace Monitoring.Postgresql.Models.Auth;

/// <summary>
/// Модель установки пароля
/// </summary>
public class SetPasswordDTO
{
    /// <summary>
    /// email адрес
    /// </summary>
    public string EmailAddress { get; set; }

    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; }
}