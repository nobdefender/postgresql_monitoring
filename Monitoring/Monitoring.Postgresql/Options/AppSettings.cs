namespace Monitoring.Postgresql.Options
{
    public class AppSettingsOptions
    {
        public AppSettings AppSettings { get; set; }
    }

    public class AppSettings
    {
        public string TelegramBotToken { get; set; }
    }
}
