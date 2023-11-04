namespace Monitoring.Postgresql.Models
{
    public class ZabbixRequestModel
    {
        public string Message { get; set; }
        public UserActionRequestModel[] userActionModels { get; set; }
    }
}
