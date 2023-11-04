namespace Monitoring.Posgresql.Infrastructure.Models.Auth;

public class UserDbModel
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Username
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Пароль
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// email адрес
    /// </summary>
    public string EmailAddress { get; set; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Роль
    /// </summary>
    public string Role { get; set; }

    /// <summary>
    /// Refresh токен
    /// </summary>
    public string? RefreshToken { get; set; }

    /// <summary>
    /// Id чата телеграм
    /// </summary>
    public long TelegramChatId { get; set; }

    /// <summary>
    /// Время действия Refresh токена
    /// </summary>
    public DateTime? RefreshTokenExpiryTime { get; set; }
}