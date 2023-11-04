namespace Monitoring.Postgresql.Models.Action;

public class ActionDTO
{
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
}