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

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Ник пользователя в телеграмме
    /// </summary>
    public string? UserName { get; set; }

    [JsonIgnore]
    public List<TelegramToUserToActionDbModel> UserToActionDbModels { get; set; }
}