namespace Monitoring.Postgresql.Models.Action;

public class ActionDTO
{
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
}