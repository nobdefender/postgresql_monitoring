using Monitoring.Posgresql.Infrastructure.Models.Access;
using Monitoring.Posgresql.Infrastructure.Models.Auth;
using System.Text.Json.Serialization;

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
    public int ActiondId { get; set; }

    /// <summary>
    /// Удалена ли запись
    /// </summary>
    public bool? IsDeleted { get; set; }


    [JsonIgnore]
    public ActionDbModel ActionDbModel { get; set; }

    [JsonIgnore]
    public UserDbModel UserDbModel { get; set; }
}