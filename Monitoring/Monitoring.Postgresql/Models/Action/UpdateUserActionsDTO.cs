namespace Monitoring.Postgresql.Models.Action;

public class UpdateUserActionsDTO
{
    public int TelegramBotUserId { get; set; }
    
    public IEnumerable<int> Ids { get; set; }
}