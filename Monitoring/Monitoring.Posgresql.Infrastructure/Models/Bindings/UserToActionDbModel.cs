namespace Monitoring.Posgresql.Infrastructure.Models.Bindings;

public class UserToActionDbModel
{
    /// <summary>
    /// Идентификатор записи
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Идентификатор действия
    /// </summary>
    public string ActiondId { get; set; }

    /// <summary>
    /// Удалена ли запись
    /// </summary>
    public bool? IsDeleted { get; set; }
}