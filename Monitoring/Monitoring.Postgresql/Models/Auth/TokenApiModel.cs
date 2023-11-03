namespace Monitoring.Postgresql.Models.Auth;
    
/// <summary>
/// Модель Access и Refresh токенов
/// </summary>

public class TokenApiModel
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}