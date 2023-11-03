namespace Monitoring.Postgresql.Models.Auth;

/// <summary>
/// Модель пользователя сервиса
/// </summary>
public class UserDTO
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
}