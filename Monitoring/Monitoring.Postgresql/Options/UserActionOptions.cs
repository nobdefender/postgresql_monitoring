namespace Monitoring.Postgresql.Options
{
    public class UserActionOptions
    {
        public UserActionSection UserAction { get; set; }
    }

    public class UserActionSection
    {
        public string MongoConnectionString { get; set; }
    }
}
