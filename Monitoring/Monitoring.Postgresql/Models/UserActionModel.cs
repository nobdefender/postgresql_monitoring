namespace Monitoring.Postgresql.Models
{
    public class UserActionRequestModel
    {
        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string[] ActionParams { get; set; }
    }
}
