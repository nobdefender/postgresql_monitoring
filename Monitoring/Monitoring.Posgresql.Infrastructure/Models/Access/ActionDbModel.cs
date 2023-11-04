using Monitoring.Posgresql.Infrastructure.Models.Bindings;
using System.Text.Json.Serialization;

namespace Monitoring.Posgresql.Infrastructure.Models.Access;

public class ActionDbModel
{
    public ActionDbModel()
    {
        UserToActionDbModels = new List<UserToActionDbModel>();
    }

    /// <summary>
    /// Идентификатор записи
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор действия из Zabbix
    /// </summary>
    public string Actionid { get; set; }
    
    /// <summary>
    /// Имя действия
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// EventSource
    /// </summary>
    public string Eventsource { get; set; }
    
    /// <summary>
    /// Статус
    /// </summary>
    public string Status { get; set; }
    
    /// <summary>
    /// Период
    /// </summary>
    public string Esc_period { get; set; }

    /// <summary>
    /// Пауза
    /// </summary>
    public string Pause_suppressed { get; set; }
    
    /// <summary>
    /// Notify_if_canceled
    /// </summary>
    public string Notify_if_canceled { get; set; }

    /// <summary>
    /// Notify_if_canceled
    /// </summary>
    public string Pause_symptoms { get; set; }

    [JsonIgnore]
    public List<UserToActionDbModel>? UserToActionDbModels { get; set; }
}