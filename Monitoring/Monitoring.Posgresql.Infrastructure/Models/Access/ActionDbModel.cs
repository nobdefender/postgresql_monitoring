using Monitoring.Posgresql.Infrastructure.Models.Bindings;
using System.Text.Json.Serialization;

namespace Monitoring.Posgresql.Infrastructure.Models.Access;

public class ActionDbModel
{
    public ActionDbModel()
    {
        UserToActionDbModels = new List<TelegramToUserToActionDbModel>();
    }

    /// <summary>
    /// Идентификатор записи
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Имя действия
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Описание действия
    /// </summary>
    public string Description { get; set; }
    
    [JsonIgnore]
    public List<TelegramToUserToActionDbModel>? UserToActionDbModels { get; set; }
}