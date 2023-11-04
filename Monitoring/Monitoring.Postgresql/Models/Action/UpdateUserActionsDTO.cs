namespace Monitoring.Postgresql.Models.Action;

public class UpdateUserActionsDTO
{
    public int UserId { get; set; }
    
    public IEnumerable<string> ActionIds { get; set; }
}