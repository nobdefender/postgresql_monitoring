namespace Monitoring.Postgresql.Models.Auth;

/// <summary>
/// Модель добавления/обновления пользователя
/// </summary>
public class UpsertWebUserDTO
{
    /// <summary>
    /// email адрес
    /// </summary>
    public string EmailAddress { get; set; }
 
    /// <summary>
    /// Username
    /// </summary>
    public string Username { get; set; }

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