using Monitoring.Posgresql.Infrastructure.Models.Access;
using System.Text.Json.Serialization;
using Monitoring.Posgresql.Infrastructure.Models.TelegramBot;

namespace Monitoring.Posgresql.Infrastructure.Models.Bindings;

public class TelegramToUserToActionDbModel
{
    /// <summary>
    /// Идентификатор записи
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public int TelegramBotUserId { get; set; }

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
    public TelegramBotUserDbModel TelegramBotUserDbModel { get; set; }
}