namespace Monitoring.Postgresql.Models.Auth
{
    public class UserWithTokenDTO: TokenApiModel
    {
        public WebUserDTO WebUser { get; set; }
    }
}
