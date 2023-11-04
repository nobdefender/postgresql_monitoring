using System.Text.Json.Serialization;
using Monitoring.Posgresql.Infrastructure.Models.Bindings;

namespace Monitoring.Posgresql.Infrastructure.Models.TelegramBot;

public class TelegramBotUserDbModel
{
    public TelegramBotUserDbModel()
    {
        UserToActionDbModels = new List<TelegramToUserToActionDbModel>();
    }

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Id чата телеграм
    /// </summary>
    public long TelegramChatId { get; set; }

    [JsonIgnore]
    public List<TelegramToUserToActionDbModel> UserToActionDbModels { get; set; }
}