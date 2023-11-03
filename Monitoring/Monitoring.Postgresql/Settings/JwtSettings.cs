namespace Monitoring.Postgresql.Settings;

public class JwtSettings
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int AccessTokenMinutesExpire { get; set; }
    public int RefreshTokenDaysExpire { get; set; }
}