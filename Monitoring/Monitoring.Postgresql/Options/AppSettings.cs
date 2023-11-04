namespace Monitoring.Postgresql.Options
{
    public class AppSettingsOptions
    {
        public AppSettingsSection AppSettings { get; set; }
    }

    public class AppSettingsSection
    {
        public string TelegramBotToken { get; set; }
    }
}
